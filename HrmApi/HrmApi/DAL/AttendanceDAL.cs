using HrmApi.Models;
using HrmApi.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static HrmApi.DAL.SQLHelper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace HrmApi.DAL
{
    public class AttendanceDAL
    {
        private string _ConnString; //variable for connection string
        public AttendanceDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Attendance

        public AttendanceReturn MarkLocalAttendance(AttendanceModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            AttendanceReturn RObj = new AttendanceReturn(); // For result body object declaration

            int retstatus; string retmessage; string todayChkInTime; string todayChkOutTime;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";
                todayChkInTime = "";
                todayChkOutTime = "";

                // Array for parameters
                var parm = new SqlParameter[6];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("emp_id", SqlDbType.VarChar, 50) { Value = getModel.emp_id };
                parm[2] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                parm[3] = new SqlParameter("client_code", SqlDbType.VarChar, 50) { Value = getModel.client_code };
                parm[4] = new SqlParameter("company", SqlDbType.VarChar, 255) { Value = getModel.company };
                parm[5] = new SqlParameter("want", SqlDbType.Int) { Value = getModel.want };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_att_proc]", parm);
                foreach (DataRow row in dsuser.Rows)
                {
                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        todayChkInTime = Common.CheckStringNull(row["today_check_in_time"]);
                        todayChkOutTime = Common.CheckStringNull(row["today_check_out_time"]);
                    }
                }


                RObj.Status = retstatus; // Success status
                RObj.Message = retmessage;
                RObj.today_check_in_time = todayChkInTime;
                RObj.today_check_out_time = todayChkOutTime;
                return RObj;
            }
            // If an error occurs during API execution
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }
        }

        public async Task<AttendanceReturn> MarkAttendance(AttendanceModel getModel)
        {
            AttendanceReturn RObj = new AttendanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new AttendanceModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company,
                want = getModel.want,
            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/attendance";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";


                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<AttApiResponse>(jsonResponse);

                        List<AttendanceModel> RetLst = new List<AttendanceModel>();

                        foreach (var item in apiResponse.Items)
                        {
                           
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                      
                     
                    }
                    else
                    {
                        retstatus = 70;
                        retmessage = "API returned an error: " + response.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                retstatus = 70;
                retmessage = "No Data Found. Error: " + ex.Message;
            }

            RObj.Status = retstatus;
            RObj.Message = retmessage;

            return RObj;
        }


        public AttendanceReturn GetAttendance(AttendanceModel getModel) // get modal from user in getModel
        {
            AttendanceReturn RObj = new AttendanceReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Att_Proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strAttDate = ""; // for string date
                var strCheckInDate = ""; // for string date
                var strCheckOutDate = ""; // for string date
                var strCheckInTime = ""; // for string date
                var strCheckOutTime = ""; // for string date

                // for single object declaration
                var RetObj = new AttendanceModel();
                // for list object declaration
                List<AttendanceModel> RetLst = new List<AttendanceModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new AttendanceModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping

                        RetObj.att_id = Common.CheckIntegerNull(row["att_id"]);
                        RetObj.att_date = Common.CheckDateTimeNull(row["att_date"]);
                        if (!string.IsNullOrEmpty(row["att_date"].ToString()) && row["att_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["att_date"]);
                            strAttDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strAttDate = "00-000-0000";
                        }
                        RetObj.str_att_date = strAttDate;
                        RetObj.check_in_time = Common.CheckDateTimeNull(row["check_in_time"]);
                        if (!string.IsNullOrEmpty(row["check_in_time"].ToString()) && row["check_in_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_in_time"]);
                            strCheckInDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCheckInDate = "00-000-0000";
                        }
                        RetObj.str_check_in_Date = strCheckInDate;
                        if (!string.IsNullOrEmpty(row["check_in_time"].ToString()) && row["check_in_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_in_time"]);
                            strCheckInTime = ChequeOldDate.ToString("HH:mm:ss");
                        }
                        else
                        {
                            strCheckInTime = "00:00:00";
                        }
                        RetObj.str_check_in_time = strCheckInTime;
                        RetObj.check_out_time = Common.CheckDateTimeNull(row["check_out_time"]);
                        if (!string.IsNullOrEmpty(row["check_out_time"].ToString()) && row["check_out_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_out_time"]);
                            strCheckOutDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCheckOutDate = "00-000-0000";
                        }
                        RetObj.str_check_out_Date = strCheckOutDate;
                        if (!string.IsNullOrEmpty(row["check_out_time"].ToString()) && row["check_out_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_out_time"]);
                            strCheckOutTime = ChequeOldDate.ToString("HH:mm:ss");
                        }
                        else
                        {
                            strCheckOutTime = "00:00:00";
                        }
                        RetObj.str_check_out_time = strCheckOutTime;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id_to_show = Common.CheckStringNull(row["emp_id_to_show"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.emp_designation = Common.CheckStringNull(row["emp_designation"]);
                        RetObj.shift_type = Common.CheckStringNull(row["shift_type"]);
                        RetObj.working_hours = Common.CheckStringNull(row["working_hours"]);
                        RetObj.att_status = Common.CheckStringNull(row["att_status"]);
                        if (RetObj.check_in_time != null && RetObj.check_out_time != null)
                        {
                            // Ensure both check-in and check-out times are for today's date
                            DateTime today = DateTime.Today;

                            DateTime checkInDateTime = RetObj.check_in_time.Value;
                            DateTime checkOutDateTime = RetObj.check_out_time.Value;

                            // Check if the check-in and check-out times are on today's date
                            if (checkInDateTime.Date == today && checkOutDateTime.Date == today)
                            {
                                TimeSpan totalHours = checkOutDateTime - checkInDateTime;

                                // Format the total time in HH:mm
                                RetObj.total_hours_work = string.Format("{0:00}:{1:00}:{2:00}", (int)totalHours.TotalHours, totalHours.Minutes, totalHours.Seconds);

                            }
                            else
                            {
                                // If not today's date, default to "00:00"
                                RetObj.total_hours_work = "00:00";
                            }
                        }
                        else
                        {
                            // If either time is null, set total hours to "00:00"
                            RetObj.total_hours_work = "00:00";
                        }






                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.AttendanceDataList = RetLst;
                return RObj; //finaly return the object
            }
            // if api calling error
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }

        }


        public AttendanceReturn GetDashboardAttendance(AttendanceModel getModel) // get modal from user in getModel
        {
            AttendanceReturn RObj = new AttendanceReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 7 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Att_Proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strAttDate = ""; // for string date
                var strCheckInDate = ""; // for string date
                var strCheckOutDate = ""; // for string date
                var strCheckInTime = ""; // for string date
                var strCheckOutTime = ""; // for string date

                // for single object declaration
                var RetObj = new AttendanceModel();
                // for list object declaration
                List<AttendanceModel> RetLst = new List<AttendanceModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new AttendanceModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping

                        RetObj.att_id = Common.CheckIntegerNull(row["att_id"]);
                        RetObj.att_date = Common.CheckDateTimeNull(row["att_date"]);
                        if (!string.IsNullOrEmpty(row["att_date"].ToString()) && row["att_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["att_date"]);
                            strAttDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strAttDate = "00-000-0000";
                        }
                        RetObj.str_att_date = strAttDate;
                        RetObj.check_in_time = Common.CheckDateTimeNull(row["check_in_time"]);
                        if (!string.IsNullOrEmpty(row["check_in_time"].ToString()) && row["check_in_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_in_time"]);
                            strCheckInDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCheckInDate = "00-000-0000";
                        }
                        RetObj.str_check_in_Date = strCheckInDate;
                        if (!string.IsNullOrEmpty(row["check_in_time"].ToString()) && row["check_in_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_in_time"]);
                            strCheckInTime = ChequeOldDate.ToString("HH:mm:ss");
                        }
                        else
                        {
                            strCheckInTime = "00:00:00";
                        }
                        RetObj.str_check_in_time = strCheckInTime;
                        RetObj.check_out_time = Common.CheckDateTimeNull(row["check_out_time"]);
                        if (!string.IsNullOrEmpty(row["check_out_time"].ToString()) && row["check_out_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_out_time"]);
                            strCheckOutDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCheckOutDate = "00-000-0000";
                        }
                        RetObj.str_check_out_Date = strCheckOutDate;
                        if (!string.IsNullOrEmpty(row["check_out_time"].ToString()) && row["check_out_time"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["check_out_time"]);
                            strCheckOutTime = ChequeOldDate.ToString("HH:mm:ss");
                        }
                        else
                        {
                            strCheckOutTime = "00:00:00";
                        }
                        RetObj.str_check_out_time = strCheckOutTime;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id_to_show = Common.CheckStringNull(row["emp_id_to_show"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.emp_designation = Common.CheckStringNull(row["emp_designation"]);
                        RetObj.shift_type = Common.CheckStringNull(row["shift_type"]);
                        RetObj.working_hours = Common.CheckStringNull(row["working_hours"]);
                        RetObj.att_status = Common.CheckStringNull(row["att_status"]);
                        if (RetObj.check_in_time != null && RetObj.check_out_time != null)
                        {
                            // Ensure both check-in and check-out times are for today's date
                            DateTime today = DateTime.Today;

                            DateTime checkInDateTime = RetObj.check_in_time.Value;
                            DateTime checkOutDateTime = RetObj.check_out_time.Value;

                            // Check if the check-in and check-out times are on today's date
                            if (checkInDateTime.Date == today && checkOutDateTime.Date == today)
                            {
                                TimeSpan totalHours = checkOutDateTime - checkInDateTime;

                                // Format the total time in HH:mm
                                RetObj.total_hours_work = string.Format("{0:00}:{1:00}:{2:00}", (int)totalHours.TotalHours, totalHours.Minutes, totalHours.Seconds);

                            }
                            else
                            {
                                // If not today's date, default to "00:00"
                                RetObj.total_hours_work = "00:00";
                            }
                        }
                        else
                        {
                            // If either time is null, set total hours to "00:00"
                            RetObj.total_hours_work = "00:00";
                        }






                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.AttendanceDataList = RetLst;
                return RObj; //finaly return the object
            }
            // if api calling error
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }

        }


        public async Task<AttendanceReturn> GetLiveMonthAtt(AttendanceModel getModel)
        {
            AttendanceReturn RObj = new AttendanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new AttendanceModel
            {
                Operation = 2,
                emp_id = getModel.emp_id,
                company = getModel.company,
                month_year = getModel.month_year,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/attendance";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<AttApiResponse>(jsonResponse);

                        List<AttendanceModel> RetLst = new List<AttendanceModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            AttendanceModel RetObj = new AttendanceModel
                            {

                                att_date = item.att_date,
                                str_att_date = item.str_att_date,
                                dayy = item.dayy,
                                str_check_in_time = item.str_check_in_time,
                                str_check_out_time = item.str_check_out_time,
                                att_status = item.att_status,
                                working_hours = item.working_hours
                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.AttendanceDataList = RetLst;
                    }
                    else
                    {
                        retstatus = 70;
                        retmessage = "API returned an error: " + response.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                retstatus = 70;
                retmessage = "No Data Found. Error: " + ex.Message;
            }

            RObj.Status = retstatus;
            RObj.Message = retmessage;

            return RObj;
        }


        #endregion
    }
}