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
using System.Text;
using System.Threading.Tasks;

namespace HrmApi.DAL
{
    public class DashboardDAL
    {
        private string _ConnString; //variable for connection string
        public DashboardDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Dashboard

        ///////////////// GET dashboard att //////////////////////////

        public DashAttReturn GetDashboardAtt(DashboardAttendance getModel) // get modal from user in getModel
        {
            DashAttReturn RObj = new DashAttReturn(); // for result body object declaration
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
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Dashboard_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";


                // for single object declaration
                var RetObj = new DashboardAttendance();
                // for list object declaration
                List<DashboardAttendance> RetLst = new List<DashboardAttendance>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new DashboardAttendance(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping

                        RetObj.present = Common.CheckIntegerNull(row["present"]);
                        RetObj.absent = Common.CheckIntegerNull(row["absent"]);
                        RetObj.short_leave = Common.CheckIntegerNull(row["short_leave"]);
                        RetObj.leave = Common.CheckIntegerNull(row["leave"]);
                        RetObj.gh_holiday = Common.CheckIntegerNull(row["gh_holiday"]);
                        //RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        //RetObj.company = Common.CheckStringNull(row["company"]);
                        //RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);





                    }

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                RObj.DashAttData = RetObj;
                // if want to show in list of object
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

        ///////////////// GET dashboard working hours //////////////////////////

        public DashHourReturn GetDashboardHours(DashWorkHoursModel getModel) // get modal from user in getModel 
        {
            DashHourReturn RObj = new DashHourReturn(); // for result body object declaration
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
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Dashboard_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
          

                // for single object declaration
                var RetObj = new DashWorkHoursModel();
                // for list object declaration
                List<DashWorkHoursModel> RetLst = new List<DashWorkHoursModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new DashWorkHoursModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping

                        RetObj.day_title = Common.CheckStringNull(row["day_title"]);
                        RetObj.day_hours = Common.CheckIntegerNull(row["day_hours"]);
                        //RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        //RetObj.company = Common.CheckStringNull(row["company"]);
                        //RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);





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
                RObj.DashHourDataList = RetLst;
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

        ///////////////// GET dashboard att //////////////////////////

        public DashLeaveBalReturn GetDashLeaveBal(DashLeaveBalModel getModel) // get modal from user in getModel
        {
            DashLeaveBalReturn RObj = new DashLeaveBalReturn(); // for result body object declaration
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
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Dashboard_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";


                // for single object declaration
                var RetObj = new DashLeaveBalModel();

                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new DashLeaveBalModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.leave_bal = Common.CheckIntegerNull(row["leave_bal"]);
                        //RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        //RetObj.company = Common.CheckStringNull(row["company"]);
                        //RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);





                    }

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                RObj.DashLeaveBalData = RetObj;
                // if want to show in list of object
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


        //public async Task<AttendanceReturn> GetLiveLastSevDayAtt(AttendanceModel getModel)
        //{
        //    AttendanceReturn RObj = new AttendanceReturn();
        //    int retstatus = 80;
        //    string retmessage = "Api is not Fetching!";
        //    var attendanceRequest = new AttendanceModel
        //    {
        //        Operation = 1,
        //        emp_id = getModel.emp_id,
        //    };


        //    try
        //    {
        //        string apiUrl = "http://192.168.2.3:8080/ords/setup/hrms/dashboardd";
        //        //for hr db
        //        if (getModel.company == "2423-0019" || getModel.company == "2423-0121" || getModel.company == "2423-0122" || getModel.company == "2423-0123" || getModel.company == "2423-0124" || getModel.company == "2423-0125" || getModel.company == "2423-0126")
        //        {
        //            apiUrl = "http://192.168.2.3:8080/ords/setup/hrms/dashboard";
        //        }

        //        using (HttpClient client = new HttpClient())
        //        {
        //            // Serialize the request model to JSON
        //            string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
        //            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        //            // Send POST request with the JSON body
        //            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                retstatus = 100;
        //                retmessage = "Data fetched successfully";

        //                string jsonResponse = await response.Content.ReadAsStringAsync();
        //                var apiResponse = JsonConvert.DeserializeObject<AttApiResponse>(jsonResponse);

        //                List<AttendanceModel> RetLst = new List<AttendanceModel>();

        //                foreach (var item in apiResponse.Items)
        //                {


        //                    AttendanceModel RetObj = new AttendanceModel
        //                    {

        //                        att_date = item.att_date,
        //                        str_att_date = item.str_att_date,
        //                        dayy = item.dayy,
        //                        str_check_in_time = item.str_check_in_time,
        //                        str_check_out_time = item.str_check_out_time,
        //                        att_status = item.att_status,
        //                        working_hours = item.working_hours
        //                    };

        //                    RetLst.Add(RetObj);
        //                    retstatus = 2;
        //                    retmessage = "Data fetched successfully !";
        //                }
        //                RObj.Status = retstatus;
        //                RObj.Message = retmessage;
        //                RObj.AttendanceDataList = RetLst;
        //            }
        //            else
        //            {
        //                retstatus = 70;
        //                retmessage = "API returned an error: " + response.StatusCode.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retstatus = 70;
        //        retmessage = "No Data Found. Error: " + ex.Message;
        //    }

        //    RObj.Status = retstatus;
        //    RObj.Message = retmessage;

        //    return RObj;
        //}

        public async Task<AttendanceReturn> GetLiveAttCount(AttendanceModel getModel)
        {
            AttendanceReturn RObj = new AttendanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int present = 0;
            int absent = 0;
            int late_in = 0;
            int early_out = 0;
            int awaiting = 0;
            var attendanceRequest = new AttendanceModel
            {
                Operation = 3,
                emp_id = getModel.emp_id,
                company = getModel.company,
                att_month = getModel.att_month
            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/dashboard";
                

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


                            /* AttendanceModel RetObj = new AttendanceModel
                             {

                                 present = item.present,
                                 absent = item.absent,
                                 late_in = item.late_in,
                                 early_out = item.early_out

                             };

                             RetLst.Add(RetObj);*/
                            present = item.present;
                            absent = item.absent;
                            late_in = item.late_in;
                            early_out = item.early_out;
                            awaiting = item.awaiting;
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.present = present;
                        RObj.absent = absent;
                        RObj.late_in = late_in;
                        RObj.early_out = early_out;
                        RObj.awaiting = awaiting;
                        // RObj.AttendanceDataList = RetLst;
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


        public async Task<LeaveReturn> GetLiveLeaveBal(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            decimal total_sick_leave = 0;
            decimal avail_sick_leave = 0;
            decimal total_annual_leave = 0;
            decimal avail_annual_leave = 0;
            decimal total_casual_leave = 0;
            decimal avail_casual_leave = 0;
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LeaveModel
            {
                Operation = 2,
                emp_id = getModel.emp_id,
                company = getModel.company,
            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/dashboard";
                

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
                        var apiResponse = JsonConvert.DeserializeObject<LeaveApiResponse>(jsonResponse);

                        List<LeaveModel> RetLst = new List<LeaveModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            total_sick_leave = item.total_sick_leave;
                            avail_sick_leave = item.avail_sick_leave;
                            total_annual_leave = item.total_annual_leave;
                            avail_annual_leave = item.avail_annual_leave;
                            total_casual_leave = item.total_casual_leave;
                            avail_casual_leave = item.avail_casual_leave;
                            /*LeaveModel RetObj = new LeaveModel
                            {

                                status = item.status
                            };

                            RetLst.Add(RetObj);*/

                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.total_sick_leave = total_sick_leave;
                        RObj.sick_leave_bal = avail_sick_leave;
                        RObj.total_anual_leave = total_annual_leave;
                        RObj.anual_leave_bal = avail_annual_leave;
                        RObj.total_casual_leave = total_casual_leave;
                        RObj.casual_leave_bal = avail_casual_leave;

                        //RObj.LeaveDataList = RetLst;
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


        public async Task<AttendanceReturn> GetLiveLastSevDayAtt(AttendanceModel getModel)
        {
            AttendanceReturn RObj = new AttendanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new AttendanceModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company
            };


            try
            {
                
                  string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/dashboard";
                

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