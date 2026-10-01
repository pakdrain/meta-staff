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
using System.IO;
using Google.Protobuf;

namespace HrmApi.DAL
{
    public class LeaveDAL
    {
        private string _ConnString; //variable for connection string
        public LeaveDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Leaves


        public async Task<LeaveReturn> AUDLeave(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            string taskId = "";
            int operation = getModel.Operation;
            var attendanceRequest = new LeaveModel
            {

                Operation = getModel.Operation,
                action = getModel.action,
                emp_leave_id = getModel.emp_leave_id,
                emp_id = getModel.emp_id,
                company = getModel.company,
                leave_id = getModel.leave_id,
                total_days = getModel.total_days,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                remark = getModel.remark,
                contact_no = getModel.contact_no,
                contact_adress = getModel.contact_adress,
                leave_duration = getModel.leave_duration,
                leave_doc = getModel.leave_doc,
                extension = getModel.extension,



            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leaves";


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

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            if (operation == 2)
                            {
                                taskId = item.new_leave_id;
                            }
                        }

                        if (retstatus == 2 && operation == 2)
                        {
                            // taskId = Common.CheckIntegerNull(row["task_id"]);

                            // Define the path where the image will be saved
                            string folderPath = HttpContext.Current.Server.MapPath("~/LeaveDocument/");
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            // Generate a unique file name
                            string fileName = taskId.ToString() + "." + getModel.extension;
                            string taskIdString = taskId.ToString();
                            string fullPath = Path.Combine(folderPath, fileName);
                            string[] existingFiles = Directory.GetFiles(folderPath, taskIdString + ".*");
                            //retmessage = fullPath + getModel.leave_doc;
                            // Check if file already exists, and delete it if so

                            //foreach (string file in existingFiles)
                            //{
                            //    File.Delete(file);  // Delete the existing file
                            //}

                            if (!string.IsNullOrEmpty(getModel.extension))
                            {
                                // Convert the Base64 string to a byte array
                                byte[] imageBytes = Convert.FromBase64String(getModel.leave_doc);

                                // Save the byte array to the specified path as a file
                                File.WriteAllBytes(fullPath, imageBytes);
                            }

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

        public async Task<LeaveReturn> GetLeaveById(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LeaveModel
            {
                Operation = 5,
                emp_id = getModel.emp_id,
                company = getModel.company,
                emp_leave_id = getModel.emp_leave_id
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leaves";


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


                            LeaveModel RetObj = new LeaveModel
                            {

                                emp_leave_id = item.emp_leave_id,
                                emp_id = item.emp_id,
                                leave_id = item.leave_id,
                                org_id = item.org_id,
                                leave_status = item.leave_status,
                                from_date = item.from_date,
                                to_date = item.to_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                total_days = item.total_days,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                remark = item.remark,
                                contact_adress = item.contact_adress,
                                contact_no = item.contact_no,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rejected_remark = item.rejected_remark,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                leave_type = item.leave_type,
                                leave_duration = item.leave_duration,
                                rejected_by = item.rejected_by,
                                leave_doc = item.leave_doc,
                                extension = item.extension,
                                company = item.company,

                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.LeaveDataList = RetLst;
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

        public async Task<LeaveReturn> GetMyLeaves(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LeaveModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leaves";


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


                            LeaveModel RetObj = new LeaveModel
                            {

                                emp_leave_id = item.emp_leave_id,
                                emp_id = item.emp_id,
                                leave_id = item.leave_id,
                                org_id = item.org_id,
                                leave_status = item.leave_status,
                                from_date = item.from_date,
                                to_date = item.to_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                total_days = item.total_days,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                remark = item.remark,
                                contact_adress = item.contact_adress,
                                contact_no = item.contact_no,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rejected_remark = item.rejected_remark,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                leave_type = item.leave_type,
                                leave_duration = item.leave_duration,
                                rejected_by = item.rejected_by


                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.LeaveDataList = RetLst;
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

        public async Task<LeaveReturn> GetHODLeaves(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LeaveModel
            {
                Operation = 7,
                emp_id = getModel.emp_id,
                company = getModel.company,
                org_id = getModel.org_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leaves";


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


                            LeaveModel RetObj = new LeaveModel
                            {

                                emp_leave_id = item.emp_leave_id,
                                emp_id = item.emp_id,
                                leave_id = item.leave_id,
                                org_id = item.org_id,
                                leave_status = item.leave_status,
                                from_date = item.from_date,
                                to_date = item.to_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                total_days = item.total_days,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                remark = item.remark,
                                contact_adress = item.contact_adress,
                                contact_no = item.contact_no,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rejected_remark = item.rejected_remark,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                leave_type = item.leave_type,
                                leave_duration = item.leave_duration,
                                rejected_by = item.rejected_by

                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.LeaveDataList = RetLst;
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

        public async Task<LeaveReturn> GetHRLeavess(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LeaveModel
            {
                Operation = 8,
                emp_id = getModel.emp_id,
                company = getModel.company,
                org_id = getModel.org_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leaves";


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


                            LeaveModel RetObj = new LeaveModel
                            {

                                emp_leave_id = item.emp_leave_id,
                                emp_id = item.emp_id,
                                leave_id = item.leave_id,
                                org_id = item.org_id,
                                leave_status = item.leave_status,
                                from_date = item.from_date,
                                to_date = item.to_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                total_days = item.total_days,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                remark = item.remark,
                                contact_adress = item.contact_adress,
                                contact_no = item.contact_no,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rejected_remark = item.rejected_remark,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                leave_type = item.leave_type,
                                leave_duration = item.leave_duration,
                                rejected_by = item.rejected_by

                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.LeaveDataList = RetLst;
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

        public async Task<LeaveReturn> GetHRLeaves(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LeaveModel
            {
                Operation = 8,
                emp_id = getModel.emp_id,
                company = getModel.company,
                org_id = getModel.org_id,
                str_from_date = getModel.str_from_date,   
                str_to_date = getModel.str_to_date, 
            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leaves";

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5); // Increase timeout
                                                              // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

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
                            LeaveModel RetObj = new LeaveModel
                            {
                                emp_leave_id = item.emp_leave_id,
                                emp_id = item.emp_id,
                                leave_id = item.leave_id,
                                org_id = item.org_id,
                                leave_status = item.leave_status,
                                from_date = item.from_date,
                                to_date = item.to_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                total_days = item.total_days,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                remark = item.remark,
                                contact_adress = item.contact_adress,
                                contact_no = item.contact_no,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rejected_remark = item.rejected_remark,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                leave_type = item.leave_type,
                                leave_duration = item.leave_duration,
                                rejected_by = item.rejected_by
                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully!";
                        }

                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.LeaveDataList = RetLst;
                    }
                    else
                    {
                        retstatus = 70;
                        retmessage = "API returned an error: " + response.StatusCode.ToString();
                    }
                }
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                retstatus = 70;
                retmessage = "The request timed out. Please try again later.";
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



        public async Task<LeaveReturn> LeaveStatusChanger(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LeaveModel
            {
                Operation = 6,
                emp_leave_id = getModel.emp_leave_id,
                leave_status = getModel.leave_status,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                rejected_remark = getModel.rejected_remark,
                emp_id = getModel.emp_id,
                company = getModel.company,
                rejected_by = getModel.rejected_by

            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leaves";


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

        public async Task<LeaveReturn> GetSadwitchStatus(LeaveModel getModel)
        {
            LeaveReturn RObj = new LeaveReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            decimal noOfDays = '0';
            int operation = getModel.Operation;
            var attendanceRequest = new LeaveModel
            {

                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,


            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/leave-sandwitch";


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

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            noOfDays = item.total_days;
                        }


                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.no_of_leave_days = noOfDays;


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