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
    public class WpPolicyDAL
    {
        private string _ConnString; //variable for connection string
        public WpPolicyDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region WpPolicy

        public async Task<WpPolicyReturn> GetMyWpPolicy(WpPolicyModel getModel)
        {
            WpPolicyReturn RObj = new WpPolicyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new WpPolicyModel
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                wp_id = getModel.wp_id,
                company = getModel.company,
                subject = getModel.subject,
                description = getModel.description,
                wp_doc_extension = getModel.wp_doc_extension,
                policy_type = getModel.policy_type,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                wp_status = getModel.wp_status,
                emp_id = getModel.emp_id,
                doc_url = getModel.doc_url,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/wp-policy";


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
                        var apiResponse = JsonConvert.DeserializeObject<WpPolicyApiResponse>(jsonResponse);

                        List<WpPolicyModel> RetLst = new List<WpPolicyModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            WpPolicyModel RetObj = new WpPolicyModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                wp_id = item.wp_id,
                                company = item.company,
                                subject = item.subject,
                                description = item.description,
                                wp_doc_extension = item.wp_doc_extension,
                                policy_type = item.policy_type,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                wp_status = item.wp_status,
                                emp_id = item.emp_id,
                                doc_url = item.doc_url,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                com_name = item.com_name,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.WpPolicyDataList = RetLst;
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

        public async Task<WpPolicyReturn> GetHrWpPolicy(WpPolicyModel getModel)
        {
            WpPolicyReturn RObj = new WpPolicyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new WpPolicyModel
            {
                Operation = 8,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                wp_id = getModel.wp_id,
                company = getModel.company,
                subject = getModel.subject,
                description = getModel.description,
                wp_doc_extension = getModel.wp_doc_extension,
                policy_type = getModel.policy_type,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                wp_status = getModel.wp_status,
                emp_id = getModel.emp_id,
                doc_url = getModel.doc_url,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/wp-policy";


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
                        var apiResponse = JsonConvert.DeserializeObject<WpPolicyApiResponse>(jsonResponse);

                        List<WpPolicyModel> RetLst = new List<WpPolicyModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            WpPolicyModel RetObj = new WpPolicyModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                wp_id = item.wp_id,
                                company = item.company,
                                subject = item.subject,
                                description = item.description,
                                wp_doc_extension = item.wp_doc_extension,
                                policy_type = item.policy_type,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                wp_status = item.wp_status,
                                emp_id = item.emp_id,
                                doc_url = item.doc_url,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                com_name    = item.com_name,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.WpPolicyDataList = RetLst;
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

        public async Task<WpPolicyReturn> GetWpPolicyById(WpPolicyModel getModel)
        {
            WpPolicyReturn RObj = new WpPolicyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new WpPolicyModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                wp_id = getModel.wp_id,
                company = getModel.company,
                subject = getModel.subject,
                description = getModel.description,
                wp_doc_extension = getModel.wp_doc_extension,
                policy_type = getModel.policy_type,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                wp_status = getModel.wp_status,
                emp_id = getModel.emp_id,
                doc_url = getModel.doc_url,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/wp-policy";


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
                        var apiResponse = JsonConvert.DeserializeObject<WpPolicyApiResponse>(jsonResponse);

                        List<WpPolicyModel> RetLst = new List<WpPolicyModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            WpPolicyModel RetObj = new WpPolicyModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                wp_id = item.wp_id,
                                company = item.company,
                                subject = item.subject,
                                description = item.description,
                                wp_doc_extension = item.wp_doc_extension,
                                policy_type = item.policy_type,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                wp_status = item.wp_status,
                                emp_id = item.emp_id,
                                doc_url = item.doc_url,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                com_name = item.com_name,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.WpPolicyDataList = RetLst;
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

        public async Task<WpPolicyReturn> AUDWpPolicy(WpPolicyModel getModel)
        {
            WpPolicyReturn RObj = new WpPolicyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int newWpId = 0;
            int operation = getModel.Operation;

            var attendanceRequest = new WpPolicyModel
            {
                
                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                wp_id = getModel.wp_id,
                company = getModel.company,
                subject = getModel.subject,
                description = getModel.description,
                wp_doc_extension = getModel.wp_doc_extension,
                policy_type = getModel.policy_type,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                wp_status = getModel.wp_status,
                emp_id = getModel.emp_id,
                doc_url = getModel.doc_url,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/wp-policy";


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
                        var apiResponse = JsonConvert.DeserializeObject<WpPolicyApiResponse>(jsonResponse);

                        List<WpPolicyModel> RetLst = new List<WpPolicyModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newWpId = item.new_wp_id;

                        }
                        if (retstatus == 2 && operation == 2 )
                        {
                            if (!string.IsNullOrEmpty(getModel.wp_doc) && !string.IsNullOrEmpty(getModel.wp_doc_extension))
                            {
                                SaveBase64File(getModel.wp_doc, getModel.wp_doc_extension, "~/WpIdDocument/", newWpId.ToString());
                            }                         
                        }

                        if (retstatus == 2 && operation == 3)
                        {
                            if (!string.IsNullOrEmpty(getModel.wp_doc) && !string.IsNullOrEmpty(getModel.wp_doc_extension))
                            {
                                SaveBase64File(getModel.wp_doc, getModel.wp_doc_extension, "~/WpIdDocument/", getModel.wp_id.ToString());
                            }
                        }




                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_wp_id = newWpId;
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