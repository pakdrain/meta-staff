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

namespace HrmApi.DAL
{
    public class ComplainDAL
    {
        private string _ConnString; //variable for connection string
        public ComplainDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Complain        
        public async Task<ComplainReturn> GetAllComplains(ComplainModel getModel)
        {
            ComplainReturn RObj = new ComplainReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ComplainModel
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_cmplnt_id = getModel.new_cmplnt_id,
                cmplt_id = getModel.cmplt_id,
                cmplt_no = getModel.cmplt_no,
                subject = getModel.subject,
                company = getModel.company,
                org_id = getModel.org_id,
                description = getModel.description,
                doc_extension = getModel.doc_extension,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                rej_by = getModel.rej_by,
                action_page = getModel.action_page,
                emp_id = getModel.emp_id,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                doc_url = getModel.doc_url,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_app_date = getModel.hr_app_date,
                str_hr_app_date = getModel.str_hr_app_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/complaint";


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
                        var apiResponse = JsonConvert.DeserializeObject<ComplainApiResponse>(jsonResponse);

                        List<ComplainModel> RetLst = new List<ComplainModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ComplainModel RetObj = new ComplainModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                new_cmplnt_id = item.new_cmplnt_id,
                                cmplt_id = item.cmplt_id,
                                cmplt_no = item.cmplt_no,
                                subject = item.subject,
                                company = item.company,
                                org_id = item.org_id,
                                description = item.description,
                                doc_extension = item.doc_extension,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                rej_by = item.rej_by,
                                action_page = item.action_page,
                                emp_id = item.emp_id,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                doc_url = item.doc_url,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_app_date = item.hr_app_date,
                                str_hr_app_date = item.str_hr_app_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                desig_name = item.desig_name,
                                hr_doc_one_ext = item.hr_doc_one_ext,
                                hr_doc_two_ext = item.hr_doc_two_ext,
                                hr_doc_three_ext = item.hr_doc_three_ext,
                                hr_doc_one_url = item.hr_doc_one_url,
                                hr_doc_two_url = item.hr_doc_two_url,
                                hr_doc_three_url = item.hr_doc_three_url,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ComplainDataList = RetLst;
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

        public async Task<ComplainReturn> GetComplainById(ComplainModel getModel)
        {
            ComplainReturn RObj = new ComplainReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ComplainModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_cmplnt_id = getModel.new_cmplnt_id,
                cmplt_id = getModel.cmplt_id,
                cmplt_no = getModel.cmplt_no,
                subject = getModel.subject,
                company = getModel.company,
                org_id = getModel.org_id,
                description = getModel.description,
                doc_extension = getModel.doc_extension,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                rej_by = getModel.rej_by,
                action_page = getModel.action_page,
                emp_id = getModel.emp_id,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                doc_url = getModel.doc_url,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_app_date = getModel.hr_app_date,
                str_hr_app_date = getModel.str_hr_app_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                desig_name = getModel.desig_name,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/complaint";


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
                        var apiResponse = JsonConvert.DeserializeObject<ComplainApiResponse>(jsonResponse);

                        List<ComplainModel> RetLst = new List<ComplainModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ComplainModel RetObj = new ComplainModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                new_cmplnt_id = item.new_cmplnt_id,
                                cmplt_id = item.cmplt_id,
                                cmplt_no = item.cmplt_no,
                                subject = item.subject,
                                company = item.company,
                                org_id = item.org_id,
                                description = item.description,
                                doc_extension = item.doc_extension,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                rej_by = item.rej_by,
                                action_page = item.action_page,
                                emp_id = item.emp_id,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                doc_url = item.doc_url,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_app_date = item.hr_app_date,
                                str_hr_app_date = item.str_hr_app_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                desig_name = item.desig_name,
                                hr_doc_one_ext = item.hr_doc_one_ext,
                                hr_doc_two_ext = item.hr_doc_two_ext,
                                hr_doc_three_ext = item.hr_doc_three_ext,
                                hr_doc_one_url = item.hr_doc_one_url,
                                hr_doc_two_url = item.hr_doc_two_url,
                                hr_doc_three_url = item.hr_doc_three_url,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ComplainDataList = RetLst;
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

        public async Task<ComplainReturn> GetHodComplains(ComplainModel getModel)
        {
            ComplainReturn RObj = new ComplainReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ComplainModel
            {
                Operation = 7,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_cmplnt_id = getModel.new_cmplnt_id,
                cmplt_id = getModel.cmplt_id,
                cmplt_no = getModel.cmplt_no,
                subject = getModel.subject,
                company = getModel.company,
                org_id = getModel.org_id,
                description = getModel.description,
                doc_extension = getModel.doc_extension,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                rej_by = getModel.rej_by,
                action_page = getModel.action_page,
                emp_id = getModel.emp_id,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                doc_url = getModel.doc_url,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_app_date = getModel.hr_app_date,
                str_hr_app_date = getModel.str_hr_app_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                desig_name = getModel.desig_name,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/complaint";


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
                        var apiResponse = JsonConvert.DeserializeObject<ComplainApiResponse>(jsonResponse);

                        List<ComplainModel> RetLst = new List<ComplainModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ComplainModel RetObj = new ComplainModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                new_cmplnt_id = item.new_cmplnt_id,
                                cmplt_id = item.cmplt_id,
                                cmplt_no = item.cmplt_no,
                                subject = item.subject,
                                company = item.company,
                                org_id = item.org_id,
                                description = item.description,
                                doc_extension = item.doc_extension,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                rej_by = item.rej_by,
                                action_page = item.action_page,
                                emp_id = item.emp_id,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                doc_url = item.doc_url,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_app_date = item.hr_app_date,
                                str_hr_app_date = item.str_hr_app_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                desig_name = item.desig_name,
                                hr_doc_one_ext = item.hr_doc_one_ext,
                                hr_doc_two_ext = item.hr_doc_two_ext,
                                hr_doc_three_ext = item.hr_doc_three_ext,
                                hr_doc_one_url = item.hr_doc_one_url,
                                hr_doc_two_url = item.hr_doc_two_url,
                                hr_doc_three_url = item.hr_doc_three_url,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ComplainDataList = RetLst;
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

        public async Task<ComplainReturn> GetHrComplains(ComplainModel getModel)
        {
            ComplainReturn RObj = new ComplainReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ComplainModel
            {
                Operation = 8,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_cmplnt_id = getModel.new_cmplnt_id,
                cmplt_id = getModel.cmplt_id,
                cmplt_no = getModel.cmplt_no,
                subject = getModel.subject,
                company = getModel.company,
                org_id = getModel.org_id,
                description = getModel.description,
                doc_extension = getModel.doc_extension,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                rej_by = getModel.rej_by,
                action_page = getModel.action_page,
                emp_id = getModel.emp_id,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                doc_url = getModel.doc_url,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_app_date = getModel.hr_app_date,
                str_hr_app_date = getModel.str_hr_app_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                desig_name = getModel.desig_name,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/complaint";


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
                        var apiResponse = JsonConvert.DeserializeObject<ComplainApiResponse>(jsonResponse);

                        List<ComplainModel> RetLst = new List<ComplainModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ComplainModel RetObj = new ComplainModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                new_cmplnt_id = item.new_cmplnt_id,
                                cmplt_id = item.cmplt_id,
                                cmplt_no = item.cmplt_no,
                                subject = item.subject,
                                company = item.company,
                                org_id = item.org_id,
                                description = item.description,
                                doc_extension = item.doc_extension,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                rej_by = item.rej_by,
                                action_page = item.action_page,
                                emp_id = item.emp_id,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                doc_url = item.doc_url,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_app_date = item.hr_app_date,
                                str_hr_app_date = item.str_hr_app_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                desig_name = item.desig_name,
                                hr_doc_one_ext = item.hr_doc_one_ext,
                                hr_doc_two_ext = item.hr_doc_two_ext,
                                hr_doc_three_ext = item.hr_doc_three_ext,
                                hr_doc_one_url = item.hr_doc_one_url,
                                hr_doc_two_url = item.hr_doc_two_url,
                                hr_doc_three_url = item.hr_doc_three_url,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ComplainDataList = RetLst;
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

        public async Task<ComplainReturn> AUDComplains(ComplainModel getModel)
        {
            ComplainReturn RObj = new ComplainReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int newComplainId = 0;
            var attendanceRequest = new ComplainModel
            {
                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_cmplnt_id = getModel.new_cmplnt_id,
                cmplt_id = getModel.cmplt_id,
                cmplt_no = getModel.cmplt_no,
                subject = getModel.subject,
                company = getModel.company,
                org_id = getModel.org_id,
                description = getModel.description,
                doc_extension = getModel.doc_extension,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                rej_by = getModel.rej_by,
                action_page = getModel.action_page,
                emp_id = getModel.emp_id,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                doc_url = getModel.doc_url,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_app_date = getModel.hr_app_date,
                str_hr_app_date = getModel.str_hr_app_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                hr_doc_one_ext = getModel.hr_doc_one_ext,
                hr_doc_two_ext = getModel.hr_doc_two_ext,
                hr_doc_three_ext = getModel.hr_doc_three_ext,
                hr_doc_one_url = getModel.hr_doc_one_url,
                hr_doc_two_url = getModel.hr_doc_two_url,
                hr_doc_three_url = getModel.hr_doc_three_url,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/complaint";


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
                        var apiResponse = JsonConvert.DeserializeObject<ComplainApiResponse>(jsonResponse);

                        List<ComplainModel> RetLst = new List<ComplainModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newComplainId = item.new_cmplnt_id;

                        }

                        if (retstatus == 2 && getModel.Operation == 2)
                        {
                            if (!string.IsNullOrEmpty(getModel.document) && !string.IsNullOrEmpty(getModel.doc_extension))
                            {
                                SaveBase64File(getModel.document, getModel.doc_extension, "~/ComplaintIdDocument/", newComplainId.ToString());
                            }
                        }

                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_cmplnt_id = newComplainId;


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

        public async Task<ComplainReturn> StatusChanger(ComplainModel getModel)
        {
            ComplainReturn RObj = new ComplainReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int newComplainId = 0;
            var attendanceRequest = new ComplainModel
            {
                Operation = 6,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_cmplnt_id = getModel.new_cmplnt_id,
                cmplt_id = getModel.cmplt_id,
                cmplt_no = getModel.cmplt_no,
                subject = getModel.subject,
                company = getModel.company,
                org_id = getModel.org_id,
                description = getModel.description,
                doc_extension = getModel.doc_extension,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                rej_by = getModel.rej_by,
                action_page = getModel.action_page,
                emp_id = getModel.emp_id,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                doc_url = getModel.doc_url,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_app_date = getModel.hr_app_date,
                str_hr_app_date = getModel.str_hr_app_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                document = getModel.document,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                desig_name = getModel.desig_name,
                hr_doc_one_ext = getModel.hr_doc_one_ext,
                hr_doc_two_ext = getModel.hr_doc_two_ext,
                hr_doc_three_ext = getModel.hr_doc_three_ext,
                hr_doc_one_url = getModel.hr_doc_one_url,
                hr_doc_two_url = getModel.hr_doc_two_url,
                hr_doc_three_url = getModel.hr_doc_three_url,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/complaint";


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
                        var apiResponse = JsonConvert.DeserializeObject<ComplainApiResponse>(jsonResponse);

                        List<ComplainModel> RetLst = new List<ComplainModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newComplainId = item.new_cmplnt_id;

                        }

                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            if (!string.IsNullOrEmpty(getModel.hr_doc_one) && !string.IsNullOrEmpty(getModel.hr_doc_one_ext))
                            {
                                SaveBase64File(getModel.hr_doc_one, getModel.hr_doc_one_ext, "~/ComplaintHrDocOne/", getModel.cmplt_id.ToString());
                            }

                            if (!string.IsNullOrEmpty(getModel.hr_doc_two) && !string.IsNullOrEmpty(getModel.hr_doc_two_ext))
                            {
                                SaveBase64File(getModel.hr_doc_two, getModel.hr_doc_two_ext, "~/ComplaintHrDocTwo/", getModel.cmplt_id.ToString());
                            }

                            if (!string.IsNullOrEmpty(getModel.hr_doc_three) && !string.IsNullOrEmpty(getModel.hr_doc_three_ext))
                            {
                                SaveBase64File(getModel.hr_doc_three, getModel.hr_doc_three_ext, "~/ComplaintHrDocThree/", getModel.cmplt_id.ToString());
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



        /////////// file upload action ////////////
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
