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
    public class HcDAL
    {
        private string _ConnString; //variable for connection string
        public HcDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region HcDAL

        public async Task<HcReturn> GetMyHc(HcModel getModel)
        {
            HcReturn RObj = new HcReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new HcModel
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hc_id = getModel.hc_id,
                notice_type = getModel.notice_type,
                subject = getModel.subject,
                description = getModel.description,
                doc_one_extension = getModel.doc_one_extension,
                doc_two_extension = getModel.doc_two_extension,
                doc_three_extension = getModel.doc_three_extension,
                status = getModel.status,
                company = getModel.company,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                com_name = getModel.com_name,
                doc_one_url = getModel.doc_one_url,
                doc_two_url = getModel.doc_two_url,
                doc_three_url = getModel.doc_three_url,
                hc_no = getModel.hc_no,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                department = getModel.department,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/hc";


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
                        var apiResponse = JsonConvert.DeserializeObject<HcApiResponse>(jsonResponse);

                        List<HcModel> RetLst = new List<HcModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            HcModel RetObj = new HcModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hc_id = item.hc_id,
                                notice_type = item.notice_type,
                                subject = item.subject,
                                description = item.description,
                                doc_one_extension = item.doc_one_extension,
                                doc_two_extension = item.doc_two_extension,
                                doc_three_extension = item.doc_three_extension,
                                status = item.status,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                com_name = item.com_name,
                                doc_one_url = item.doc_one_url,
                                doc_two_url = item.doc_two_url,
                                doc_three_url = item.doc_three_url,
                                hc_no = item.hc_no,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                department = item.department,
                                dep_name = item.dep_name,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.HcDataList = RetLst;
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

        public async Task<HcReturn> GetHcById(HcModel getModel)
        {
            HcReturn RObj = new HcReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new HcModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hc_id = getModel.hc_id,
                notice_type = getModel.notice_type,
                subject = getModel.subject,
                description = getModel.description,
                doc_one_extension = getModel.doc_one_extension,
                doc_two_extension = getModel.doc_two_extension,
                doc_three_extension = getModel.doc_three_extension,
                status = getModel.status,
                company = getModel.company,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                com_name = getModel.com_name,
                doc_one_url = getModel.doc_one_url,
                doc_two_url = getModel.doc_two_url,
                doc_three_url = getModel.doc_three_url,
                hc_no = getModel.hc_no,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                department = getModel.department,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/hc";


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
                        var apiResponse = JsonConvert.DeserializeObject<HcApiResponse>(jsonResponse);

                        List<HcModel> RetLst = new List<HcModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            HcModel RetObj = new HcModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hc_id = item.hc_id,
                                notice_type = item.notice_type,
                                subject = item.subject,
                                description = item.description,
                                doc_one_extension = item.doc_one_extension,
                                doc_two_extension = item.doc_two_extension,
                                doc_three_extension = item.doc_three_extension,
                                status = item.status,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                com_name = item.com_name,
                                doc_one_url = item.doc_one_url,
                                doc_two_url = item.doc_two_url,
                                doc_three_url = item.doc_three_url,
                                hc_no = item.hc_no,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                department = item.department,
                                dep_name = item.dep_name,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.HcDataList = RetLst;
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

        public async Task<HcReturn> GetHrHc(HcModel getModel)
        {
            HcReturn RObj = new HcReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new HcModel
            {
                Operation = 8,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hc_id = getModel.hc_id,
                notice_type = getModel.notice_type,
                subject = getModel.subject,
                description = getModel.description,
                doc_one_extension = getModel.doc_one_extension,
                doc_two_extension = getModel.doc_two_extension,
                doc_three_extension = getModel.doc_three_extension,
                status = getModel.status,
                company = getModel.company,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                com_name = getModel.com_name,
                doc_one_url = getModel.doc_one_url,
                doc_two_url = getModel.doc_two_url,
                doc_three_url = getModel.doc_three_url,
                hc_no = getModel.hc_no,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                department = getModel.department,
                


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/hc";


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
                        var apiResponse = JsonConvert.DeserializeObject<HcApiResponse>(jsonResponse);

                        List<HcModel> RetLst = new List<HcModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            HcModel RetObj = new HcModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hc_id = item.hc_id,
                                notice_type = item.notice_type,
                                subject = item.subject,
                                description = item.description,
                                doc_one_extension = item.doc_one_extension,
                                doc_two_extension = item.doc_two_extension,
                                doc_three_extension = item.doc_three_extension,
                                status = item.status,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                com_name = item.com_name,
                                doc_one_url = item.doc_one_url,
                                doc_two_url = item.doc_two_url,
                                doc_three_url = item.doc_three_url,
                                hc_no = item.hc_no,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                department = item.department,
                                dep_name = item.dep_name,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.HcDataList = RetLst;
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

        public async Task<HcReturn> AudHc(HcModel getModel)
        {
            HcReturn RObj = new HcReturn();
            int operation = getModel.Operation;  
            int newHcId = 0;
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new HcModel
            {
                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hc_id = getModel.hc_id,
                notice_type = getModel.notice_type,
                subject = getModel.subject,
                description = getModel.description,
                doc_one_extension = getModel.doc_one_extension,
                doc_two_extension = getModel.doc_two_extension,
                doc_three_extension = getModel.doc_three_extension,
                status = getModel.status,
                company = getModel.company,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                com_name = getModel.com_name,
                doc_one_url = getModel.doc_one_url,
                doc_two_url = getModel.doc_two_url,
                doc_three_url = getModel.doc_three_url,
                hc_no = getModel.hc_no,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,   
                department = getModel.department,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/hc";


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
                        var apiResponse = JsonConvert.DeserializeObject<HcApiResponse>(jsonResponse);

                        List<HcModel> RetLst = new List<HcModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newHcId = item.new_hc_id;
                            HcModel RetObj = new HcModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hc_id = item.hc_id,
                                notice_type = item.notice_type,
                                subject = item.subject,
                                description = item.description,
                                doc_one_extension = item.doc_one_extension,
                                doc_two_extension = item.doc_two_extension,
                                doc_three_extension = item.doc_three_extension,
                                status = item.status,
                                company = item.company,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                com_name = item.com_name,
                                doc_one_url = item.doc_one_url,
                                doc_two_url = item.doc_two_url,
                                doc_three_url = item.doc_three_url,
                                hc_no = item.hc_no,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date
                            };                          
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_hc_id = newHcId;

                        if (retstatus == 2)
                        {
                            RObj.HcDataList = RetLst;
                        }

                        if (retstatus == 2 && operation == 2)
                        {
                            if (!string.IsNullOrEmpty(getModel.doc_one) && !string.IsNullOrEmpty(getModel.doc_one_extension))
                            {
                                SaveBase64File(getModel.doc_one, getModel.doc_one_extension, "~/HcDocOne/", newHcId.ToString());
                            }

                            if (!string.IsNullOrEmpty(getModel.doc_two) && !string.IsNullOrEmpty(getModel.doc_two_extension))
                            {
                                SaveBase64File(getModel.doc_two, getModel.doc_two_extension, "~/HcDocTwo/", newHcId.ToString());
                            }

                            if (!string.IsNullOrEmpty(getModel.doc_three) && !string.IsNullOrEmpty(getModel.doc_three_extension))
                            {
                                SaveBase64File(getModel.doc_three, getModel.doc_three_extension, "~/HcDocThree/", newHcId.ToString());
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



// For File Save
        private void SaveBase64File(string base64Data, string extension, string folder, string taskId)
        {
            if (string.IsNullOrEmpty(extension) || string.IsNullOrEmpty(base64Data)) return;

            string folderPath = HttpContext.Current.Server.MapPath(folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = taskId + "." + extension;
            string fullPath = Path.Combine(folderPath, fileName);

            // Optional: Remove previous versions
            foreach (var file in Directory.GetFiles(folderPath, taskId + ".*"))
            {
                File.Delete(file);
            }

            byte[] bytes = Convert.FromBase64String(base64Data);
            File.WriteAllBytes(fullPath, bytes);
        }


        #endregion
    }
}