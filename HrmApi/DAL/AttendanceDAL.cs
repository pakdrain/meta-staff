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
        public async Task<AttendanceReturn> GetAttReport(AttendanceModel getModel)
        {
            AttendanceReturn RObj = new AttendanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new AttendanceModel
            {
                Operation = 3,
                emp_id = getModel.emp_id,
                company = getModel.company,
                dep_id = getModel.dep_id,
                month_year = getModel.month_year,
                str_start_date = getModel.str_start_date,
                str_end_date = getModel.str_end_date,
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

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                sr_id = item.sr_id,
                                att_id = item.att_id,
                                want = item.want,
                                att_date = item.att_date,
                                str_att_date = item.str_att_date,
                                check_in_time = item.check_in_time,
                                str_check_in_Date = item.str_check_in_Date,
                                str_check_in_time = item.str_check_in_time,
                                check_out_time = item.check_out_time,
                                str_check_out_Date = item.str_check_out_Date,
                                str_check_out_time = item.str_check_out_time,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                client_code = item.client_code,
                                company = item.company,
                                dep_id = item.dep_id,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_id = item.emp_id,
                                dayy = item.dayy,
                                emp_name = item.emp_name,
                                emp_designation = item.emp_designation,
                                shift_type = item.shift_type,
                                working_hours = item.working_hours,
                                total_hours_work = item.total_hours_work,
                                att_status = item.att_status,
                                month_year = item.month_year,
                                att_month = item.att_month,
                                emp_no = item.emp_no,
                                designation = item.designation,
                                department = item.department,
                                present = item.present,
                                absent = item.absent,
                                late_in = item.late_in,
                                early_out = item.early_out,
                                awaiting = item.awaiting

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
        public async Task<AttendanceReturn> GetHierarchyAtt(AttendanceModel getModel)
        {
            AttendanceReturn RObj = new AttendanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            AttendanceModel value = new AttendanceModel
            {
                Operation = 5,
                emp_id = getModel.emp_id,
                company = getModel.company,
                dep_id = getModel.dep_id,
                month_year = getModel.month_year,
                str_start_date = getModel.str_start_date,
                str_end_date = getModel.str_end_date
            };
            try
            {
                string requestUri = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/attendance";
                using (HttpClient client = new HttpClient()) { 
                string content = JsonConvert.SerializeObject(value);
                StringContent content2 = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await client.PostAsync(requestUri, content2);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    retstatus = 100;
                    retmessage = "Data fetched successfully";
                    AttApiResponse attApiResponse = JsonConvert.DeserializeObject<AttApiResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
                    List<AttendanceModel> list = new List<AttendanceModel>();
                    foreach (AttendanceModel item2 in attApiResponse.Items)
                    {
                        AttendanceModel item = new AttendanceModel
                        {
                            Operation = item2.Operation,
                            res_status = item2.res_status,
                            res_message = item2.res_message,
                            sr_id = item2.sr_id,
                            att_id = item2.att_id,
                            want = item2.want,
                            att_date = item2.att_date,
                            str_att_date = item2.str_att_date,
                            check_in_time = item2.check_in_time,
                            str_check_in_Date = item2.str_check_in_Date,
                            str_check_in_time = item2.str_check_in_time,
                            check_out_time = item2.check_out_time,
                            str_check_out_Date = item2.str_check_out_Date,
                            str_check_out_time = item2.str_check_out_time,
                            start_date = item2.start_date,
                            str_start_date = item2.str_start_date,
                            end_date = item2.end_date,
                            str_end_date = item2.str_end_date,
                            client_code = item2.client_code,
                            company = item2.company,
                            dep_id = item2.dep_id,
                            emp_id_to_show = item2.emp_id_to_show,
                            emp_id = item2.emp_id,
                            parent_emp_id = item2.parent_emp_id,
                            dayy = item2.dayy,
                            emp_name = item2.emp_name,
                            emp_designation = item2.emp_designation,
                            shift_type = item2.shift_type,
                            working_hours = item2.working_hours,
                            total_hours_work = item2.total_hours_work,
                            att_status = item2.att_status,
                            month_year = item2.month_year,
                            att_month = item2.att_month,
                            emp_no = item2.emp_no,
                            designation = item2.designation,
                            department = item2.department,
                            present = item2.present,
                            absent = item2.absent,
                            late_in = item2.late_in,
                            early_out = item2.early_out,
                            awaiting = item2.awaiting
                        };
                        list.Add(item);
                        retstatus = 2;
                        retmessage = "Data fetched successfully !";
                    }

                    RObj.Status = retstatus;
                    RObj.Message = retmessage;
                    RObj.AttendanceDataList = list;
                }
                else
                {
                    retstatus = 70;
                    retmessage = "API returned an error: " + httpResponseMessage.StatusCode;
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

        public async Task<AttendanceReturn> GetAttDesigWise(AttendanceModel getModel)
        {
            AttendanceReturn RObj = new AttendanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new AttendanceModel
            {
                Operation = 4,
                emp_id = getModel.emp_id,
                company = getModel.company,
                dep_id = getModel.dep_id,
                month_year = getModel.month_year,
                str_start_date = getModel.str_start_date,
                str_end_date = getModel.str_end_date,
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

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                sr_id = item.sr_id,
                                att_id = item.att_id,
                                want = item.want,
                                att_date = item.att_date,
                                str_att_date = item.str_att_date,
                                check_in_time = item.check_in_time,
                                str_check_in_Date = item.str_check_in_Date,
                                str_check_in_time = item.str_check_in_time,
                                check_out_time = item.check_out_time,
                                str_check_out_Date = item.str_check_out_Date,
                                str_check_out_time = item.str_check_out_time,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                client_code = item.client_code,
                                company = item.company,
                                dep_id = item.dep_id,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_id = item.emp_id,
                                dayy = item.dayy,
                                emp_name = item.emp_name,
                                emp_designation = item.emp_designation,
                                shift_type = item.shift_type,
                                working_hours = item.working_hours,
                                total_hours_work = item.total_hours_work,
                                att_status = item.att_status,
                                month_year = item.month_year,
                                att_month = item.att_month,
                                emp_no = item.emp_no,
                                designation = item.designation,
                                department = item.department,
                                present = item.present,
                                absent = item.absent,
                                late_in = item.late_in,
                                early_out = item.early_out,
                                awaiting = item.awaiting,
                                casual = item.casual,
                                annual = item.annual,
                                medical = item.medical

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