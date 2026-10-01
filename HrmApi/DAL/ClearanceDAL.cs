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
    public class ClearanceDAL
    {
        private string _ConnString; //variable for connection string
        public ClearanceDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Clearance


        public async Task<ClearanceReturn> AUDClearance(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int clearID = 0;
            int taskId = 0;
            int operation = getModel.Operation;
            var attendanceRequest = new ClearanceModel
            {
                Operation = getModel.Operation,
                clear_id = getModel.clear_id,
                clear_no = getModel.clear_no,
                company = getModel.company,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_date_of_res = getModel.str_date_of_res,
                dep_id = getModel.dep_id,
                dep_id_s = getModel.dep_id_s,
                emp_id = getModel.emp_id,
                last_working_day = getModel.last_working_day,
                str_last_working_day = getModel.str_last_working_day,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                clearance_emp = getModel.clearance_emp,
                hr_extension = getModel.hr_extension,
                hod_extension = getModel.hod_extension,
                dep_extension = getModel.dep_extension,
                ip_adrress = getModel.ip_adrress,
            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";

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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            clearID =  item.clear_id;
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2 && operation == 2)
                        {
                            taskId = clearID;

                            // Define the path where the image will be saved
                            string folderPath = HttpContext.Current.Server.MapPath("~/ClearanceHRDocument/");
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            // Generate a unique file name
                            string fileName = taskId.ToString() + "." + getModel.hr_extension;
                            string taskIdString = taskId.ToString();
                            string fullPath = Path.Combine(folderPath, fileName);
                            string[] existingFiles = Directory.GetFiles(folderPath, taskIdString + ".*");

                            // Check if file already exists, and delete it if so

                            foreach (string file in existingFiles)
                            {
                                File.Delete(file);  // Delete the existing file
                            }

                            if (!string.IsNullOrEmpty(getModel.hr_extension))
                            {
                                // Convert the Base64 string to a byte array
                                byte[] imageBytes = Convert.FromBase64String(getModel.hr_doc);

                                // Save the byte array to the specified path as a file
                                File.WriteAllBytes(fullPath, imageBytes);
                            }
                        }
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

        public async Task<ClearanceReturn> ClearanceStatusChanger(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";

            string clearStatus = getModel.clearance_status;
            int clearID = getModel.clear_id;
            int taskId = 0;
            var attendanceRequest = new ClearanceModel
            {
                Operation = 6,
                clear_id = getModel.clear_id,
                clearance_status = getModel.clearance_status,
                company = getModel.company,
                emp_id = getModel.emp_id,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                hod_extension = getModel.hod_extension,
                ip_adrress = getModel.ip_adrress,
            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";

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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                           
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2 && clearStatus == "HOD_INITIATE")
                        {
                            taskId = clearID;

                            // Define the path where the image will be saved
                            string folderPath = HttpContext.Current.Server.MapPath("~/ClearanceHODDocument/");
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            // Generate a unique file name
                            string fileName = taskId.ToString() + "." + getModel.hod_extension;
                            string taskIdString = taskId.ToString();
                            string fullPath = Path.Combine(folderPath, fileName);
                            string[] existingFiles = Directory.GetFiles(folderPath, taskIdString + ".*");

                            // Check if file already exists, and delete it if so

                            foreach (string file in existingFiles)
                            {
                                File.Delete(file);  // Delete the existing file
                            }

                            if (!string.IsNullOrEmpty(getModel.hod_extension))
                            {
                                // Convert the Base64 string to a byte array
                                byte[] imageBytes = Convert.FromBase64String(getModel.hod_doc);

                                // Save the byte array to the specified path as a file
                                File.WriteAllBytes(fullPath, imageBytes);
                            }
                        }
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

        public async Task<ClearanceReturn> GetClearance(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ClearanceModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";


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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ClearanceModel RetObj = new ClearanceModel
                            {

                                Operation = item.Operation,
                                clear_id = item.clear_id,
                                clear_no = item.clear_no,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                date_of_res = item.date_of_res,
                                str_date_of_res = item.str_date_of_res,
                                dep_id = item.dep_id,
                                dep_id_s = item.dep_id_s,
                                emp_id = item.emp_id,
                                last_working_day = item.last_working_day,
                                str_last_working_day = item.str_last_working_day,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                clearance_status = item.clearance_status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                dep_app_by = item.dep_app_by,
                                dep_app_date = item.dep_app_date,
                                str_dep_app_date = item.str_dep_app_date,
                                app_by = item.app_by,
                                app_date = item.app_date,
                                str_app_date = item.str_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                dep_clear_id = item.dep_clear_id,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                domain = item.domain,
                                remarks = item.remarks,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                department = item.department,
                                str_doj = item.str_doj,
                                str_dor = item.str_dor,
                                clearance_emp = item.clearance_emp,
                                dep_name = item.dep_name,
                                hr_extension = item.hr_extension,
                                hr_doc = item.hr_doc,   
                                hod_extension = item.hod_extension,
                                hod_doc = item.hod_doc,
                                dep_extension = item.dep_extension, 
                                dep_doc = item.dep_doc,
                                ip_adrress = item.ip_adrress,


                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.ClearanceDataList = RetLst;
                        }
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

        public async Task<ClearanceReturn> GetHodClearance(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ClearanceModel
            {
                Operation = 7,
                emp_id = getModel.emp_id,
                company = getModel.company,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";


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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ClearanceModel RetObj = new ClearanceModel
                            {

                                Operation = item.Operation,
                                clear_id = item.clear_id,
                                clear_no = item.clear_no,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                date_of_res = item.date_of_res,
                                str_date_of_res = item.str_date_of_res,
                                dep_id = item.dep_id,
                                dep_id_s = item.dep_id_s,
                                emp_id = item.emp_id,
                                last_working_day = item.last_working_day,
                                str_last_working_day = item.str_last_working_day,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                clearance_status = item.clearance_status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                dep_app_by = item.dep_app_by,
                                dep_app_date = item.dep_app_date,
                                str_dep_app_date = item.str_dep_app_date,
                                app_by = item.app_by,
                                app_date = item.app_date,
                                str_app_date = item.str_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                dep_clear_id = item.dep_clear_id,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                domain = item.domain,
                                remarks = item.remarks,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                department = item.department,
                                str_doj = item.str_doj,
                                str_dor = item.str_dor,
                                clearance_emp = item.clearance_emp,
                                dep_name = item.dep_name,
                                hr_extension = item.hr_extension,
                                hr_doc = item.hr_doc,
                                hod_extension = item.hod_extension,
                                hod_doc = item.hod_doc,
                                dep_extension = item.dep_extension,
                                dep_doc = item.dep_doc,
                                ip_adrress = item.ip_adrress,


                            };

                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.ClearanceDataList = RetLst;
                        }
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

        public async Task<ClearanceReturn> GetClearanceById(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ClearanceModel
            {
                Operation = 5,
                emp_id = getModel.emp_id,
                company = getModel.company,
                clear_id = getModel.clear_id
            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";


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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ClearanceModel RetObj = new ClearanceModel
                            {

                                Operation = item.Operation,
                                clear_id = item.clear_id,
                                clear_no = item.clear_no,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                date_of_res = item.date_of_res,
                                str_date_of_res = item.str_date_of_res,
                                dep_id = item.dep_id,
                                dep_id_s = item.dep_id_s,
                                emp_id = item.emp_id,
                                last_working_day = item.last_working_day,
                                str_last_working_day = item.str_last_working_day,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                clearance_status = item.clearance_status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                dep_app_by = item.dep_app_by,
                                dep_app_date = item.dep_app_date,
                                str_dep_app_date = item.str_dep_app_date,
                                app_by = item.app_by,
                                app_date = item.app_date,
                                str_app_date = item.str_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                dep_clear_id = item.dep_clear_id,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                domain = item.domain,
                                remarks = item.remarks,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                department = item.department,
                                str_doj = item.str_doj,
                                str_dor = item.str_dor,
                                clearance_emp = item.clearance_emp,
                                dep_name = item.dep_name,
                                hr_extension = item.hr_extension,
                                hr_doc = item.hr_doc,
                                hod_extension = item.hod_extension,
                                hod_doc = item.hod_doc,
                                dep_extension = item.dep_extension,
                                dep_doc = item.dep_doc,
                                ip_adrress = item.ip_adrress,



                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.ClearanceDataList = RetLst;
                        }
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

        ////////////////////////dep clearance/////////////////////////////////

        public async Task<ClearanceReturn> GetDepClearance(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ClearanceModel
            {
                Operation = 8,
                emp_id = getModel.emp_id,
                company = getModel.company,


            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";


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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ClearanceModel RetObj = new ClearanceModel
                            {

                                Operation = item.Operation,
                                clear_id = item.clear_id,
                                clear_no = item.clear_no,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                date_of_res = item.date_of_res,
                                str_date_of_res = item.str_date_of_res,
                                dep_id = item.dep_id,
                                dep_id_s = item.dep_id_s,
                                emp_id = item.emp_id,
                                last_working_day = item.last_working_day,
                                str_last_working_day = item.str_last_working_day,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                clearance_status = item.clearance_status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                dep_app_by = item.dep_app_by,
                                dep_app_date = item.dep_app_date,
                                str_dep_app_date = item.str_dep_app_date,
                                app_by = item.app_by,
                                app_date = item.app_date,
                                str_app_date = item.str_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                dep_clear_id = item.dep_clear_id,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                domain = item.domain,
                                remarks = item.remarks,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                department = item.department,
                                str_doj = item.str_doj,
                                str_dor = item.str_dor,
                                clearance_emp = item.clearance_emp,
                                dep_name = item.dep_name,
                                hr_extension = item.hr_extension,
                                hr_doc = item.hr_doc,
                                hod_extension = item.hod_extension,
                                hod_doc = item.hod_doc,
                                dep_extension = item.dep_extension,
                                dep_doc = item.dep_doc,
                                ip_adrress = item.ip_adrress,


                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.ClearanceDataList = RetLst;
                        }
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


        public async Task<ClearanceReturn> GetDepClearanceByClearId(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ClearanceModel
            {
                Operation = 11,
                emp_id = getModel.emp_id,
                company = getModel.company,
                clear_id = getModel.clear_id,


            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";


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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ClearanceModel RetObj = new ClearanceModel
                            {

                                Operation = item.Operation,
                                clear_id = item.clear_id,
                                clear_no = item.clear_no,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                date_of_res = item.date_of_res,
                                str_date_of_res = item.str_date_of_res,
                                dep_id = item.dep_id,
                                dep_id_s = item.dep_id_s,
                                emp_id = item.emp_id,
                                last_working_day = item.last_working_day,
                                str_last_working_day = item.str_last_working_day,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                clearance_status = item.clearance_status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                dep_app_by = item.dep_app_by,
                                dep_app_date = item.dep_app_date,
                                str_dep_app_date = item.str_dep_app_date,
                                app_by = item.app_by,
                                app_date = item.app_date,
                                str_app_date = item.str_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                dep_clear_id = item.dep_clear_id,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                domain = item.domain,
                                remarks = item.remarks,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                department = item.department,
                                str_doj = item.str_doj,
                                str_dor = item.str_dor,
                                clearance_emp = item.clearance_emp,
                                dep_name = item.dep_name,
                                hr_extension = item.hr_extension,
                                hr_doc = item.hr_doc,
                                hod_extension = item.hod_extension,
                                hod_doc = item.hod_doc,
                                dep_extension = item.dep_extension,
                                dep_doc = item.dep_doc,
                                ip_adrress = item.ip_adrress,


                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.ClearanceDataList = RetLst;
                        }
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


        public async Task<ClearanceReturn> GetDepClearanceById(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ClearanceModel
            {
                Operation = 10,
                emp_id = getModel.emp_id,
                company = getModel.company,
                dep_clear_id = getModel.dep_clear_id,


            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";


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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ClearanceModel RetObj = new ClearanceModel
                            {

                                Operation = item.Operation,
                                clear_id = item.clear_id,
                                clear_no = item.clear_no,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                date_of_res = item.date_of_res,
                                str_date_of_res = item.str_date_of_res,
                                dep_id = item.dep_id,
                                dep_id_s = item.dep_id_s,
                                emp_id = item.emp_id,
                                last_working_day = item.last_working_day,
                                str_last_working_day = item.str_last_working_day,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                clearance_status = item.clearance_status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                dep_app_by = item.dep_app_by,
                                dep_app_date = item.dep_app_date,
                                str_dep_app_date = item.str_dep_app_date,
                                app_by = item.app_by,
                                app_date = item.app_date,
                                str_app_date = item.str_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                dep_clear_id = item.dep_clear_id,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                domain = item.domain,
                                remarks = item.remarks,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                department = item.department,
                                str_doj = item.str_doj,
                                str_dor = item.str_dor,
                                clearance_emp = item.clearance_emp,
                                dep_name = item.dep_name,
                                hr_extension = item.hr_extension,
                                hr_doc = item.hr_doc,
                                hod_extension = item.hod_extension,
                                hod_doc = item.hod_doc,
                                dep_extension = item.dep_extension,
                                dep_doc = item.dep_doc,
                                ip_adrress = item.ip_adrress,

                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.ClearanceDataList = RetLst;
                        }
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


        public async Task<ClearanceReturn> DepClearanceStatusChanger(ClearanceModel getModel)
        {
            ClearanceReturn RObj = new ClearanceReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";

            int operation = getModel.Operation;
            string clearStatus = getModel.clearance_status;
            int clearID = getModel.dep_clear_id;
            int taskId = 0;
            var attendanceRequest = new ClearanceModel
            {

                Operation = 12,
                dep_clear_id = getModel.dep_clear_id,
                clearance_status = getModel.clearance_status,
                company = getModel.company,
                emp_id = getModel.emp_id,
                remarks = getModel.remarks,
                dep_extension = getModel.dep_extension,
                ip_adrress = getModel.ip_adrress,


            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/clearance";

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
                        var apiResponse = JsonConvert.DeserializeObject<ClearanceApiResponse>(jsonResponse);

                        List<ClearanceModel> RetLst = new List<ClearanceModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2 && clearStatus == "APPROVED")
                        {
                            taskId = clearID;

                            // Define the path where the image will be saved
                            string folderPath = HttpContext.Current.Server.MapPath("~/ClearanceDepDocument/");
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            // Generate a unique file name
                            string fileName = taskId.ToString() + "." + getModel.dep_extension;
                            string taskIdString = taskId.ToString();
                            string fullPath = Path.Combine(folderPath, fileName);
                            string[] existingFiles = Directory.GetFiles(folderPath, taskIdString + ".*");

                            // Check if file already exists, and delete it if so

                            foreach (string file in existingFiles)
                            {
                                File.Delete(file);  // Delete the existing file
                            }

                            if (!string.IsNullOrEmpty(getModel.dep_extension))
                            {
                                // Convert the Base64 string to a byte array
                                byte[] imageBytes = Convert.FromBase64String(getModel.dep_doc);

                                // Save the byte array to the specified path as a file
                                File.WriteAllBytes(fullPath, imageBytes);
                            }
                        }
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