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
    public class SalSlipDAL
    {
        private string _ConnString; //variable for connection string
        public SalSlipDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region SalSlip

        public async Task<SalSlipReturn> GetMySalSlip(SalSlipModel getModel)
        {
            SalSlipReturn RObj = new SalSlipReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new SalSlipModel
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                org_id = getModel.org_id,
                slip_id = getModel.slip_id,
                emp_id = getModel.emp_id,
                description = getModel.description,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_closed_by = getModel.hr_closed_by,
                hr_closed_date = getModel.hr_closed_date,
                str_hr_closed_date = getModel.str_hr_closed_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                rej_remarks = getModel.rej_remarks,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                salary_doc_one_ext = getModel.salary_doc_one_ext,
                salary_doc_two_ext = getModel.salary_doc_two_ext,
                salary_doc_three_ext = getModel.salary_doc_three_ext,
                salary_doc_one_url = getModel.salary_doc_one_url,
                salary_doc_two_url = getModel.salary_doc_two_url,
                salary_doc_three_url = getModel.salary_doc_three_url,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/salary-slip";


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
                        var apiResponse = JsonConvert.DeserializeObject<SalSlipApiResponse>(jsonResponse);

                        List<SalSlipModel> RetLst = new List<SalSlipModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            SalSlipModel RetObj = new SalSlipModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                org_id = item.org_id,
                                slip_id = item.slip_id,
                                emp_id = item.emp_id,
                                description = item.description,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_closed_by = item.hr_closed_by,
                                hr_closed_date = item.hr_closed_date,
                                str_hr_closed_date = item.str_hr_closed_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rej_remarks = item.rej_remarks,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                salary_doc_one_ext = item.salary_doc_one_ext,
                                salary_doc_two_ext = item.salary_doc_two_ext,
                                salary_doc_three_ext = item.salary_doc_three_ext,
                                salary_doc_one_url = item.salary_doc_one_url,
                                salary_doc_two_url = item.salary_doc_two_url,
                                salary_doc_three_url = item.salary_doc_three_url,



                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.SalSlipDataList = RetLst;
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

        public async Task<SalSlipReturn> GetSalSlipById(SalSlipModel getModel)
        {
            SalSlipReturn RObj = new SalSlipReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new SalSlipModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                org_id = getModel.org_id,
                slip_id = getModel.slip_id,
                emp_id = getModel.emp_id,
                description = getModel.description,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_closed_by = getModel.hr_closed_by,
                hr_closed_date = getModel.hr_closed_date,
                str_hr_closed_date = getModel.str_hr_closed_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                rej_remarks = getModel.rej_remarks,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                salary_doc_one_ext = getModel.salary_doc_one_ext,
                salary_doc_two_ext = getModel.salary_doc_two_ext,
                salary_doc_three_ext = getModel.salary_doc_three_ext,
                salary_doc_one_url = getModel.salary_doc_one_url,
                salary_doc_two_url = getModel.salary_doc_two_url,
                salary_doc_three_url = getModel.salary_doc_three_url,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/salary-slip";


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
                        var apiResponse = JsonConvert.DeserializeObject<SalSlipApiResponse>(jsonResponse);

                        List<SalSlipModel> RetLst = new List<SalSlipModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            SalSlipModel RetObj = new SalSlipModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                org_id = item.org_id,
                                slip_id = item.slip_id,
                                emp_id = item.emp_id,
                                description = item.description,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_closed_by = item.hr_closed_by,
                                hr_closed_date = item.hr_closed_date,
                                str_hr_closed_date = item.str_hr_closed_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rej_remarks = item.rej_remarks,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                salary_doc_one_ext = item.salary_doc_one_ext,
                                salary_doc_two_ext = item.salary_doc_two_ext,
                                salary_doc_three_ext = item.salary_doc_three_ext,
                                salary_doc_one_url = item.salary_doc_one_url,
                                salary_doc_two_url = item.salary_doc_two_url,
                                salary_doc_three_url = item.salary_doc_three_url,



                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.SalSlipDataList = RetLst;
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

        public async Task<SalSlipReturn> GetHodSalSlip(SalSlipModel getModel)
        {
            SalSlipReturn RObj = new SalSlipReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new SalSlipModel
            {
                Operation = 7,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                org_id = getModel.org_id,
                slip_id = getModel.slip_id,
                emp_id = getModel.emp_id,
                description = getModel.description,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_closed_by = getModel.hr_closed_by,
                hr_closed_date = getModel.hr_closed_date,
                str_hr_closed_date = getModel.str_hr_closed_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                rej_remarks = getModel.rej_remarks,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                salary_doc_one_ext = getModel.salary_doc_one_ext,
                salary_doc_two_ext = getModel.salary_doc_two_ext,
                salary_doc_three_ext = getModel.salary_doc_three_ext,
                salary_doc_one_url = getModel.salary_doc_one_url,
                salary_doc_two_url = getModel.salary_doc_two_url,
                salary_doc_three_url = getModel.salary_doc_three_url,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/salary-slip";


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
                        var apiResponse = JsonConvert.DeserializeObject<SalSlipApiResponse>(jsonResponse);

                        List<SalSlipModel> RetLst = new List<SalSlipModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            SalSlipModel RetObj = new SalSlipModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                org_id = item.org_id,
                                slip_id = item.slip_id,
                                emp_id = item.emp_id,
                                description = item.description,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_closed_by = item.hr_closed_by,
                                hr_closed_date = item.hr_closed_date,
                                str_hr_closed_date = item.str_hr_closed_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rej_remarks = item.rej_remarks,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                salary_doc_one_ext = item.salary_doc_one_ext,
                                salary_doc_two_ext = item.salary_doc_two_ext,
                                salary_doc_three_ext = item.salary_doc_three_ext,
                                salary_doc_one_url = item.salary_doc_one_url,
                                salary_doc_two_url = item.salary_doc_two_url,
                                salary_doc_three_url = item.salary_doc_three_url,



                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.SalSlipDataList = RetLst;
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

        public async Task<SalSlipReturn> GetHrSalSlip(SalSlipModel getModel)
        {
            SalSlipReturn RObj = new SalSlipReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new SalSlipModel
            {
                Operation = 8,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                org_id = getModel.org_id,
                slip_id = getModel.slip_id,
                emp_id = getModel.emp_id,
                description = getModel.description,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_closed_by = getModel.hr_closed_by,
                hr_closed_date = getModel.hr_closed_date,
                str_hr_closed_date = getModel.str_hr_closed_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                rej_remarks = getModel.rej_remarks,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                salary_doc_one_ext = getModel.salary_doc_one_ext,
                salary_doc_two_ext = getModel.salary_doc_two_ext,
                salary_doc_three_ext = getModel.salary_doc_three_ext,
                salary_doc_one_url = getModel.salary_doc_one_url,
                salary_doc_two_url = getModel.salary_doc_two_url,
                salary_doc_three_url = getModel.salary_doc_three_url,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/salary-slip";


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
                        var apiResponse = JsonConvert.DeserializeObject<SalSlipApiResponse>(jsonResponse);

                        List<SalSlipModel> RetLst = new List<SalSlipModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            SalSlipModel RetObj = new SalSlipModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                org_id = item.org_id,
                                slip_id = item.slip_id,
                                emp_id = item.emp_id,
                                description = item.description,
                                status = item.status,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                hr_closed_by = item.hr_closed_by,
                                hr_closed_date = item.hr_closed_date,
                                str_hr_closed_date = item.str_hr_closed_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                rej_remarks = item.rej_remarks,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                emp_name = item.emp_name,
                                com_name = item.com_name,
                                dep_name = item.dep_name,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                salary_doc_one_ext = item.salary_doc_one_ext,
                                salary_doc_two_ext = item.salary_doc_two_ext,
                                salary_doc_three_ext = item.salary_doc_three_ext,
                                salary_doc_one_url = item.salary_doc_one_url,
                                salary_doc_two_url = item.salary_doc_two_url,
                                salary_doc_three_url = item.salary_doc_three_url,



                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.SalSlipDataList = RetLst;
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

        public async Task<SalSlipReturn> AudSalSlip(SalSlipModel getModel)
        {
            SalSlipReturn RObj = new SalSlipReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int operation = getModel.Operation;
            int newSlipId = 0;
            var attendanceRequest = new SalSlipModel
            {
                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                org_id = getModel.org_id,
                slip_id = getModel.slip_id,
                emp_id = getModel.emp_id,
                description = getModel.description,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_closed_by = getModel.hr_closed_by,
                hr_closed_date = getModel.hr_closed_date,
                str_hr_closed_date = getModel.str_hr_closed_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                rej_remarks = getModel.rej_remarks,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                salary_doc_one_ext = getModel.salary_doc_one_ext,
                salary_doc_two_ext = getModel.salary_doc_two_ext,
                salary_doc_three_ext = getModel.salary_doc_three_ext,
                salary_doc_one_url = getModel.salary_doc_one_url,
                salary_doc_two_url = getModel.salary_doc_two_url,
                salary_doc_three_url = getModel.salary_doc_three_url,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/salary-slip";


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
                        var apiResponse = JsonConvert.DeserializeObject<SalSlipApiResponse>(jsonResponse);

                        List<SalSlipModel> RetLst = new List<SalSlipModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newSlipId = item.new_slip_id;

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_slip_id = newSlipId;

                        //if (retstatus == 2 && operation == 6 && getModel.status == "CLOSED")
                        //{
                        //    if (!string.IsNullOrEmpty(getModel.salary_doc_one) && !string.IsNullOrEmpty(getModel.salary_doc_one_ext))
                        //    {
                        //        SaveBase64File(getModel.salary_doc_one, getModel.salary_doc_one_ext, "~/SalDocOne/", newSlipId.ToString());
                        //    }

                        //    if (!string.IsNullOrEmpty(getModel.salary_doc_two) && !string.IsNullOrEmpty(getModel.salary_doc_two_ext))
                        //    {
                        //        SaveBase64File(getModel.salary_doc_two, getModel.salary_doc_two_ext, "~/SalDocTwo/", newSlipId.ToString());
                        //    }
                        //    if (!string.IsNullOrEmpty(getModel.salary_doc_three) && !string.IsNullOrEmpty(getModel.salary_doc_three_ext))
                        //    {
                        //        SaveBase64File(getModel.salary_doc_three, getModel.salary_doc_three_ext, "~/SalDocThree/", newSlipId.ToString());
                        //    }
                        //}

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

        public async Task<SalSlipReturn> SalSlipStatusChanger(SalSlipModel getModel)
        {
            SalSlipReturn RObj = new SalSlipReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new SalSlipModel
            {
                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                org_id = getModel.org_id,
                slip_id = getModel.slip_id,
                emp_id = getModel.emp_id,
                description = getModel.description,
                status = getModel.status,
                hod_app_by = getModel.hod_app_by,
                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                hr_closed_by = getModel.hr_closed_by,
                hr_closed_date = getModel.hr_closed_date,
                str_hr_closed_date = getModel.str_hr_closed_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                rej_remarks = getModel.rej_remarks,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                emp_name = getModel.emp_name,
                com_name = getModel.com_name,
                dep_name = getModel.dep_name,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                salary_doc_one_ext = getModel.salary_doc_one_ext,
                salary_doc_two_ext = getModel.salary_doc_two_ext,
                salary_doc_three_ext = getModel.salary_doc_three_ext,
                salary_doc_one_url = getModel.salary_doc_one_url,
                salary_doc_two_url = getModel.salary_doc_two_url,
                salary_doc_three_url = getModel.salary_doc_three_url,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/salary-slip";


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
                        var apiResponse = JsonConvert.DeserializeObject<SalSlipApiResponse>(jsonResponse);

                        List<SalSlipModel> RetLst = new List<SalSlipModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2  && getModel.status == "CLOSED")
                        {
                            if (!string.IsNullOrEmpty(getModel.salary_doc_one) && !string.IsNullOrEmpty(getModel.salary_doc_one_ext))
                            {
                                SaveBase64File(getModel.salary_doc_one, getModel.salary_doc_one_ext, "~/SalDocOne/", getModel.slip_id.ToString());
                            }

                            if (!string.IsNullOrEmpty(getModel.salary_doc_two) && !string.IsNullOrEmpty(getModel.salary_doc_two_ext))
                            {
                                SaveBase64File(getModel.salary_doc_two, getModel.salary_doc_two_ext, "~/SalDocTwo/", getModel.slip_id.ToString());
                            }
                            if (!string.IsNullOrEmpty(getModel.salary_doc_three) && !string.IsNullOrEmpty(getModel.salary_doc_three_ext))
                            {
                                SaveBase64File(getModel.salary_doc_three, getModel.salary_doc_three_ext, "~/SalDocThree/", getModel.slip_id.ToString());
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