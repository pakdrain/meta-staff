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
    public class LoanDAL
    {
        private string _ConnString; //variable for connection string
        public LoanDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Loan

        public async Task<LoanSummaryReturn> GetLoanSummary(LoanSummary getModel)
        {
            LoanSummaryReturn RObj = new LoanSummaryReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoanSummary
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loan-summary";


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
                        retmessage = "Api Run But Data not fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<LoanSummaryApiResponse>(jsonResponse);

                        List<LoanSummary> RetLst = new List<LoanSummary>();

                        foreach (var item in apiResponse.Items)
                        {


                            LoanSummary RetObj = new LoanSummary
                            {
                                emp_no = item.emp_no,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                department = item.department,
                                current_pay = item.current_pay,
                                prev_amount = item.prev_amount,
                                balance = item.balance,
                                joining_diff = item.joining_diff,
                                year_diff = item.year_diff,
                                third_party = item.third_party,
                                loan_limit = item.loan_limit,
                                doj = item.doj,
                                prev_instal_date = item.prev_instal_date,
                                str_prev_instal_date    = item.str_prev_instal_date,
                                prev_status = item.prev_status,
                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.LoanSummaryDataList = RetLst;
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
        public async Task<LoansReturn> GetMyLoans(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoansModel
            {
                Operation = 1,
                company = getModel.company,
                org_id = getModel.org_id,
                emp_id = getModel.emp_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            LoansModel RetObj = new LoansModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                loan_id = item.loan_id,
                                emp_id = item.emp_id,
                                loan_start_date = item.loan_start_date,
                                str_loan_start_date = item.str_loan_start_date,
                                loan_amount = item.loan_amount,
                                per_month_instalment = item.per_month_instalment,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,
                                approved_by = item.approved_by,
                                approved_date = item.approved_date,
                                str_approved_date = item.str_approved_date,
                                loan_status = item.loan_status,
                                loan_no = item.loan_no,
                                deduction_type = item.deduction_type,
                                markup = item.markup,
                                exemption_amount = item.exemption_amount,
                                company_type = item.company_type,
                                branch_id = item.branch_id,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                org_id = item.org_id,
                                total_loan_installments = item.total_loan_installments,
                                loan_insurance = item.loan_insurance,
                                loan_type = item.loan_type,
                                id_doc = item.id_doc,
                                id_extension = item.id_extension,
                                b_c_doc = item.b_c_doc,
                                b_c_extension = item.b_c_extension,
                                other_doc = item.other_doc,
                                other_extension = item.other_extension,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,
                                department_name = item.department_name,
                                company_name = item.company_name,
                                designation = item.designation,
                                remarks = item.remarks,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                deduct_from_bonus = item.deduct_from_bonus,
                                document_clear = item.document_clear,
                                doj = item.doj
                                ,
                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_app_by = item.hr_app_by,
                                ed_app_by = item.ed_app_by,
                                rej_by = item.rej_by,
                                id_doc_clear = item.id_doc_clear,
                                b_c_doc_clear = item.b_c_doc_clear,
                                ed_remarks = item.ed_remarks,
                                gm_remarks = item.gm_remarks,
                                action_page = item.action_page,
                                current_pay = item.current_pay,
                                com_id = item.com_id,
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
                            RObj.LoanDataList = RetLst;
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
        public async Task<LoansReturn> GetHODLoans(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoansModel
            {
                Operation = 7,
                company = getModel.company,
                org_id = getModel.org_id,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            LoansModel RetObj = new LoansModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                loan_id = item.loan_id,
                                emp_id = item.emp_id,
                                loan_start_date = item.loan_start_date,
                                str_loan_start_date = item.str_loan_start_date,
                                loan_amount = item.loan_amount,
                                per_month_instalment = item.per_month_instalment,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,
                                approved_by = item.approved_by,
                                approved_date = item.approved_date,
                                str_approved_date = item.str_approved_date,
                                loan_status = item.loan_status,
                                loan_no = item.loan_no,
                                deduction_type = item.deduction_type,
                                markup = item.markup,
                                exemption_amount = item.exemption_amount,
                                company_type = item.company_type,
                                branch_id = item.branch_id,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                org_id = item.org_id,
                                total_loan_installments = item.total_loan_installments,
                                loan_insurance = item.loan_insurance,
                                loan_type = item.loan_type,
                                id_doc = item.id_doc,
                                id_extension = item.id_extension,
                                b_c_doc = item.b_c_doc,
                                b_c_extension = item.b_c_extension,
                                other_doc = item.other_doc,
                                other_extension = item.other_extension,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,
                                department_name = item.department_name,
                                company_name = item.company_name,
                                designation = item.designation,
                                remarks = item.remarks,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                deduct_from_bonus = item.deduct_from_bonus,
                                document_clear = item.document_clear,
                                doj = item.doj,
                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_app_by = item.hr_app_by,
                                ed_app_by = item.ed_app_by,
                                rej_by = item.rej_by,
                                id_doc_clear = item.id_doc_clear,
                                b_c_doc_clear = item.b_c_doc_clear,
                                ed_remarks = item.ed_remarks,
                                gm_remarks = item.gm_remarks,
                                action_page = item.action_page,
                                current_pay = item.current_pay,
                                com_id = item.com_id,
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
                            RObj.LoanDataList = RetLst;
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
        public async Task<LoansReturn> GetAppliedLoans(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoansModel
            {
                Operation = 15,
                company = getModel.company,
                org_id = getModel.org_id,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            LoansModel RetObj = new LoansModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                loan_id = item.loan_id,
                                emp_id = item.emp_id,
                                loan_start_date = item.loan_start_date,
                                str_loan_start_date = item.str_loan_start_date,
                                loan_amount = item.loan_amount,
                                per_month_instalment = item.per_month_instalment,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,
                                approved_by = item.approved_by,
                                approved_date = item.approved_date,
                                str_approved_date = item.str_approved_date,
                                loan_status = item.loan_status,
                                loan_no = item.loan_no,
                                deduction_type = item.deduction_type,
                                markup = item.markup,
                                exemption_amount = item.exemption_amount,
                                company_type = item.company_type,
                                branch_id = item.branch_id,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                org_id = item.org_id,
                                total_loan_installments = item.total_loan_installments,
                                loan_insurance = item.loan_insurance,
                                loan_type = item.loan_type,
                                id_doc = item.id_doc,
                                id_extension = item.id_extension,
                                b_c_doc = item.b_c_doc,
                                b_c_extension = item.b_c_extension,
                                other_doc = item.other_doc,
                                other_extension = item.other_extension,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,
                                department_name = item.department_name,
                                company_name = item.company_name,
                                designation = item.designation,
                                remarks = item.remarks,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                deduct_from_bonus = item.deduct_from_bonus,
                                document_clear = item.document_clear,
                                doj = item.doj,
                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_app_by = item.hr_app_by,
                                ed_app_by = item.ed_app_by,
                                rej_by = item.rej_by,
                                id_doc_clear = item.id_doc_clear,
                                b_c_doc_clear = item.b_c_doc_clear,
                                ed_remarks = item.ed_remarks,
                                gm_remarks = item.gm_remarks,
                                action_page = item.action_page,
                                current_pay = item.current_pay,
                                com_id = item.com_id,
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
                            RObj.LoanDataList = RetLst;
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
        public async Task<LoansReturn> GetGMLoans(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoansModel
            {
                Operation = 8,
                company = getModel.company,
                org_id = getModel.org_id,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            LoansModel RetObj = new LoansModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                loan_id = item.loan_id,
                                emp_id = item.emp_id,
                                loan_start_date = item.loan_start_date,
                                str_loan_start_date = item.str_loan_start_date,
                                loan_amount = item.loan_amount,
                                per_month_instalment = item.per_month_instalment,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,
                                approved_by = item.approved_by,
                                approved_date = item.approved_date,
                                str_approved_date = item.str_approved_date,
                                loan_status = item.loan_status,
                                loan_no = item.loan_no,
                                deduction_type = item.deduction_type,
                                markup = item.markup,
                                exemption_amount = item.exemption_amount,
                                company_type = item.company_type,
                                branch_id = item.branch_id,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                org_id = item.org_id,
                                total_loan_installments = item.total_loan_installments,
                                loan_insurance = item.loan_insurance,
                                loan_type = item.loan_type,
                                id_doc = item.id_doc,
                                id_extension = item.id_extension,
                                b_c_doc = item.b_c_doc,
                                b_c_extension = item.b_c_extension,
                                other_doc = item.other_doc,
                                other_extension = item.other_extension,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,
                                department_name = item.department_name,
                                company_name = item.company_name,
                                designation = item.designation,
                                remarks = item.remarks,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                deduct_from_bonus = item.deduct_from_bonus,
                                document_clear = item.document_clear,
                                doj = item.doj,
                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_app_by = item.hr_app_by,
                                ed_app_by = item.ed_app_by,
                                rej_by = item.rej_by,
                                id_doc_clear = item.id_doc_clear,
                                b_c_doc_clear = item.b_c_doc_clear,
                                ed_remarks = item.ed_remarks,
                                gm_remarks = item.gm_remarks,
                                action_page = item.action_page,
                                current_pay = item.current_pay,
                                com_id = item.com_id,
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
                            RObj.LoanDataList = RetLst;
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
        public async Task<LoansReturn> GetHRLoans(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoansModel
            {
                Operation = 9,
                company = getModel.company,
                org_id = getModel.org_id,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            LoansModel RetObj = new LoansModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                loan_id = item.loan_id,
                                emp_id = item.emp_id,
                                loan_start_date = item.loan_start_date,
                                str_loan_start_date = item.str_loan_start_date,
                                loan_amount = item.loan_amount,
                                per_month_instalment = item.per_month_instalment,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,
                                approved_by = item.approved_by,
                                approved_date = item.approved_date,
                                str_approved_date = item.str_approved_date,
                                loan_status = item.loan_status,
                                loan_no = item.loan_no,
                                deduction_type = item.deduction_type,
                                markup = item.markup,
                                exemption_amount = item.exemption_amount,
                                company_type = item.company_type,
                                branch_id = item.branch_id,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                org_id = item.org_id,
                                total_loan_installments = item.total_loan_installments,
                                loan_insurance = item.loan_insurance,
                                loan_type = item.loan_type,
                                id_doc = item.id_doc,
                                id_extension = item.id_extension,
                                b_c_doc = item.b_c_doc,
                                b_c_extension = item.b_c_extension,
                                other_doc = item.other_doc,
                                other_extension = item.other_extension,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,
                                department_name = item.department_name,
                                company_name = item.company_name,
                                designation = item.designation,
                                remarks = item.remarks,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                deduct_from_bonus = item.deduct_from_bonus,
                                document_clear = item.document_clear,
                                doj = item.doj,
                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_app_by = item.hr_app_by,
                                ed_app_by = item.ed_app_by,
                                rej_by = item.rej_by,
                                id_doc_clear = item.id_doc_clear,
                                b_c_doc_clear = item.b_c_doc_clear,
                                ed_remarks = item.ed_remarks,
                                gm_remarks = item.gm_remarks,
                                action_page = item.action_page,
                                current_pay = item.current_pay,
                                com_id = item.com_id,
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
                            RObj.LoanDataList = RetLst;
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
        public async Task<LoansReturn> GetEDLoans(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoansModel
            {
                Operation = 10,
                company = getModel.company,
                org_id = getModel.org_id,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            LoansModel RetObj = new LoansModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                loan_id = item.loan_id,
                                emp_id = item.emp_id,
                                loan_start_date = item.loan_start_date,
                                str_loan_start_date = item.str_loan_start_date,
                                loan_amount = item.loan_amount,
                                per_month_instalment = item.per_month_instalment,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,
                                approved_by = item.approved_by,
                                approved_date = item.approved_date,
                                str_approved_date = item.str_approved_date,
                                loan_status = item.loan_status,
                                loan_no = item.loan_no,
                                deduction_type = item.deduction_type,
                                markup = item.markup,
                                exemption_amount = item.exemption_amount,
                                company_type = item.company_type,
                                branch_id = item.branch_id,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                org_id = item.org_id,
                                total_loan_installments = item.total_loan_installments,
                                loan_insurance = item.loan_insurance,
                                loan_type = item.loan_type,
                                id_doc = item.id_doc,
                                id_extension = item.id_extension,
                                b_c_doc = item.b_c_doc,
                                b_c_extension = item.b_c_extension,
                                other_doc = item.other_doc,
                                other_extension = item.other_extension,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,
                                department_name = item.department_name,
                                company_name = item.company_name,
                                designation = item.designation,
                                remarks = item.remarks,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                deduct_from_bonus = item.deduct_from_bonus,
                                document_clear = item.document_clear,
                                doj = item.doj,
                                hod_app_by = item.hod_app_by,
                                gm_app_by   = item.gm_app_by,
                                hr_app_by= item.hr_app_by ,
                                ed_app_by = item.ed_app_by,
                                rej_by = item.rej_by,
                                id_doc_clear = item.id_doc_clear,
                                b_c_doc_clear = item.b_c_doc_clear,
                                ed_remarks = item.ed_remarks,
                                gm_remarks = item.gm_remarks,
                                action_page = item.action_page,
                                current_pay = item.current_pay,
                                com_id = item.com_id,
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
                            RObj.LoanDataList = RetLst;
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
        public async Task<LoansReturn> GetLoanById(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new LoansModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                loan_id = getModel.loan_id,
                emp_id = getModel.emp_id,
                loan_start_date = getModel.loan_start_date,
                str_loan_start_date = getModel.str_loan_start_date,
                loan_amount = getModel.loan_amount,
                per_month_instalment = getModel.per_month_instalment,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,
                approved_by = getModel.approved_by,
                approved_date = getModel.approved_date,
                str_approved_date = getModel.str_approved_date,
                loan_status = getModel.loan_status,
                loan_no = getModel.loan_no,
                deduction_type = getModel.deduction_type,
                markup = getModel.markup,
                exemption_amount = getModel.exemption_amount,
                company_type = getModel.company_type,
                branch_id = getModel.branch_id,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                org_id = getModel.org_id,
                total_loan_installments = getModel.total_loan_installments,
                loan_insurance = getModel.loan_insurance,
                loan_type = getModel.loan_type,
                id_doc = getModel.id_doc,
                id_extension = getModel.id_extension,
                b_c_doc = getModel.b_c_doc,
                b_c_extension = getModel.b_c_extension,
                other_doc = getModel.other_doc,
                other_extension = getModel.other_extension,
                emp_id_to_show = getModel.emp_id_to_show,
                emp_name = getModel.emp_name,
                department_name = getModel.department_name,
                company_name = getModel.company_name,
                designation = getModel.designation,
                remarks = getModel.remarks,
                hod_remarks = getModel.hod_remarks,
                hr_remarks = getModel.hr_remarks,
                deduct_from_bonus = getModel.deduct_from_bonus,
                document_clear = getModel.document_clear



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            LoansModel RetObj = new LoansModel
                            {

                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                loan_id = item.loan_id,
                                emp_id = item.emp_id,
                                loan_start_date = item.loan_start_date,
                                str_loan_start_date = item.str_loan_start_date,
                                loan_amount = item.loan_amount,
                                per_month_instalment = item.per_month_instalment,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,
                                approved_by = item.approved_by,
                                approved_date = item.approved_date,
                                str_approved_date = item.str_approved_date,
                                loan_status = item.loan_status,
                                loan_no = item.loan_no,
                                deduction_type = item.deduction_type,
                                markup = item.markup,
                                exemption_amount = item.exemption_amount,
                                company_type = item.company_type,
                                branch_id = item.branch_id,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                org_id = item.org_id,
                                total_loan_installments = item.total_loan_installments,
                                loan_insurance = item.loan_insurance,
                                loan_type = item.loan_type,
                                id_doc = item.id_doc,
                                id_extension = item.id_extension,
                                b_c_doc = item.b_c_doc,
                                b_c_extension = item.b_c_extension,
                                other_doc = item.other_doc,
                                other_extension = item.other_extension,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,
                                department_name = item.department_name,
                                company_name = item.company_name,
                                designation = item.designation,
                                remarks = item.remarks,
                                hod_remarks = item.hod_remarks,
                                hr_remarks = item.hr_remarks,
                                deduct_from_bonus = item.deduct_from_bonus,
                                document_clear = item.document_clear,
                                doj = item.doj,
                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_app_by = item.hr_app_by,
                                ed_app_by = item.ed_app_by,
                                rej_by = item.rej_by,
                                id_doc_clear = item.id_doc_clear,
                                b_c_doc_clear = item.b_c_doc_clear,
                                ed_remarks = item.ed_remarks,
                                gm_remarks = item.gm_remarks,
                                action_page = item.action_page,
                                current_pay = item.current_pay,
                                com_id = item.com_id,
                                com_name = item.com_name,
                                SchedData = item.SchedData
                                /////////////// detail table start ////////////
                                ///////////////detail table end /////////////////

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.LoanDataList = RetLst;
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
        public async Task<LoansReturn> AUDLoan(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            string taskId = "";
            int operation = getModel.Operation;
            var attendanceRequest = new LoansModel
            {

                Operation = getModel.Operation,
                company = getModel.company,
                loan_id = getModel.loan_id,
                emp_id = getModel.emp_id,
                str_loan_start_date = getModel.str_loan_start_date,
                loan_amount = getModel.loan_amount,
                per_month_instalment = getModel.per_month_instalment,
                total_loan_installments = getModel.total_loan_installments,
                loan_type = getModel.loan_type,
                remarks = getModel.remarks,
                id_extension = getModel.id_extension,
                b_c_extension = getModel.b_c_extension,
                other_extension = getModel.other_extension,
                action_page = getModel.action_page,
            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            //if (operation == 2)
                            //{
                            //    taskId = item.new_loan_id;
                            //}
                            if (operation == 3)
                            {
                                taskId = getModel.loan_id.ToString();
                            }
                        }

                        //if (retstatus == 2 && operation == 2 || operation == 3)
                        //{

                        //    SaveBase64File(getModel.id_doc, getModel.id_extension, "~/LoanIdDocument/", taskId);
                        //    SaveBase64File(getModel.b_c_doc, getModel.b_c_extension, "~/LoanBcDocument/", taskId);
                        //    SaveBase64File(getModel.other_doc, getModel.other_extension, "~/LoanOtherDocument/", taskId);
                        //}
                        if (retstatus == 2 && operation == 3 && /*!string.IsNullOrEmpty(getModel.action_page) &&*/ getModel.action_page.ToUpper() == "HR")
                        {
                            if (!string.IsNullOrEmpty(getModel.id_doc) && !string.IsNullOrEmpty(getModel.id_extension))
                            {
                                SaveBase64File(getModel.id_doc, getModel.id_extension, "~/LoanIdDocument/", taskId);
                            }

                            if (!string.IsNullOrEmpty(getModel.b_c_doc) && !string.IsNullOrEmpty(getModel.b_c_extension))
                            {
                                SaveBase64File(getModel.b_c_doc, getModel.b_c_extension, "~/LoanBcDocument/", taskId);
                            }

                            if (!string.IsNullOrEmpty(getModel.other_doc) && !string.IsNullOrEmpty(getModel.other_extension))
                            {
                                SaveBase64File(getModel.other_doc, getModel.other_extension, "~/LoanOtherDocument/", taskId);
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
        public async Task<LoansReturn> LoanStatusChanger(LoansModel getModel)
        {
            LoansReturn RObj = new LoansReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            string taskId = "";
            int operation = getModel.Operation;
            var attendanceRequest = new LoansModel
            {

                Operation = 11,
                company = getModel.company,
                emp_id = getModel.emp_id,
                loan_id = getModel.loan_id,
                str_loan_start_date = getModel.str_loan_start_date,
                action_page = getModel.action_page,
                loan_status = getModel.loan_status,
                hr_remarks = getModel.hr_remarks,
                hod_remarks = getModel.hod_remarks,
                deduct_from_bonus = getModel.deduct_from_bonus,
                document_clear = getModel.document_clear,
                id_doc_clear = getModel.id_doc_clear,
                b_c_doc_clear = getModel.b_c_doc_clear,
                ed_remarks = getModel.ed_remarks,
                gm_remarks = getModel.gm_remarks,
            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/loans";


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
                        var apiResponse = JsonConvert.DeserializeObject<LoansApiResponse>(jsonResponse);

                        List<LoansModel> RetLst = new List<LoansModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            if (operation == 2)
                            {
                                taskId = item.new_loan_id;
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
        public async Task<BonusDeductionReturn> AUDBonusDeduction(BonusDeduction getModel)
        {
            BonusDeductionReturn result = new BonusDeductionReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };
            try
            {
                string requestUri = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/hrm-bonus-ded";
                using (HttpClient client = new HttpClient()) { 
                string content = JsonConvert.SerializeObject(getModel);
                StringContent content2 = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await client.PostAsync(requestUri, content2);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    result.Status = 70;
                    result.Message = "API Error: " + httpResponseMessage.StatusCode;
                    return result;
                }

                BonusDeductionApiResponse bonusDeductionApiResponse = JsonConvert.DeserializeObject<BonusDeductionApiResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
                result.Status = bonusDeductionApiResponse.ResStatus;
                result.Message = bonusDeductionApiResponse.ResMessage;
                result.new_id = bonusDeductionApiResponse.NewId;
                if (bonusDeductionApiResponse.ResStatus == 2 && !string.IsNullOrEmpty(getModel.master.doc_ext) && !string.IsNullOrEmpty(getModel.master.doc))
                {
                    SaveBase64File(getModel.master.doc, getModel.master.doc_ext, "~/BonusDedDoc/", bonusDeductionApiResponse.NewId.ToString());
                }
            }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
        }
        public async Task<BonusDeductionReturn> GetBonusDed(BonusDeduction getModel)
        {
            BonusDeductionReturn result = new BonusDeductionReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };
            try
            {
                getModel.master.Operation = 1;
                string requestUri = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/hrm-bonus-ded";
                using (HttpClient client = new HttpClient()) { 
                string content = JsonConvert.SerializeObject(getModel);
                StringContent content2 = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await client.PostAsync(requestUri, content2);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    result.Status = 70;
                    result.Message = "API Error: " + httpResponseMessage.StatusCode;
                    return result;
                }

                BonusDeductionApiResponse bonusDeductionApiResponse = JsonConvert.DeserializeObject<BonusDeductionApiResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
                result.Status = bonusDeductionApiResponse.ResStatus;
                result.Message = bonusDeductionApiResponse.ResMessage;
                result.new_id = bonusDeductionApiResponse.NewId;
                if (bonusDeductionApiResponse.ResStatus == 2)
                {
                    result.DeductionMaster = bonusDeductionApiResponse.Items;
                }
            }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
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