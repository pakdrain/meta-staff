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
    public class EvalFormDAL
    {
        private string _ConnString; //variable for connection string
        public EvalFormDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region EvalForm

        public async Task<EvalFormReturn> GetApprEmp(EvalFormModel getModel)
        {
            EvalFormReturn RObj = new EvalFormReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new EvalFormModel
            {
                Operation = 6,
                app_user = getModel.app_user,
                dep_id = getModel.dep_id,
                company_id = getModel.company_id,
                res_status = getModel.res_status,
                res_message = getModel.res_message,

                eval_id = getModel.eval_id,
                appr_id = getModel.appr_id,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                designation = getModel.designation,
                dep_name = getModel.dep_name,

                gross_salary = getModel.gross_salary,
                service_tenure = getModel.service_tenure,

                area_unit = getModel.area_unit,
                successor_to = getModel.successor_to,
                reporting_to = getModel.reporting_to,

                edu_rel_certi = getModel.edu_rel_certi,
                edu_rel_job = getModel.edu_rel_job,

                core_skill_ability = getModel.core_skill_ability,
                take_interest = getModel.take_interest,
                problem_solving = getModel.problem_solving,
                accuracy = getModel.accuracy,
                adherence = getModel.adherence,
                take_intiative = getModel.take_intiative,
                meets_deadlines = getModel.meets_deadlines,
                punctuality = getModel.punctuality,
                attendance = getModel.attendance,
                dress_code = getModel.dress_code,
                respond_contribution = getModel.respond_contribution,
                respond_suggestion = getModel.respond_suggestion,
                gives_and_seeks = getModel.gives_and_seeks,
                adapts_well = getModel.adapts_well,
                synchronization = getModel.synchronization,
                accept_challange = getModel.accept_challange,
                innovative_thinking = getModel.innovative_thinking,
                motivation_emotional = getModel.motivation_emotional,

                this_year_biggest_impact = getModel.this_year_biggest_impact,
                specific_area_or_skill = getModel.specific_area_or_skill,
                overall_assessment = getModel.overall_assessment,
                remark_if_any = getModel.remark_if_any,

                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,

                appr_year = getModel.appr_year
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/evaluation-form";


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
                        var apiResponse = JsonConvert.DeserializeObject<EvalFormApiResponse>(jsonResponse);

                        List<EvalFormModel> RetLst = new List<EvalFormModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            EvalFormModel RetObj = new EvalFormModel
                            {
                                Operation = item.Operation,
                                app_user = item.app_user,
                                dep_id = item.dep_id,
                                company_id = item.company_id,
                                res_status = item.res_status,
                                res_message = item.res_message,

                                eval_id = item.eval_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                dep_name = item.dep_name,

                                gross_salary = item.gross_salary,
                                service_tenure = item.service_tenure,

                                area_unit = item.area_unit,
                                successor_to = item.successor_to,
                                reporting_to = item.reporting_to,

                                edu_rel_certi = item.edu_rel_certi,
                                edu_rel_job = item.edu_rel_job,

                                core_skill_ability = item.core_skill_ability,
                                take_interest = item.take_interest,
                                problem_solving = item.problem_solving,
                                accuracy = item.accuracy,
                                adherence = item.adherence,
                                take_intiative = item.take_intiative,
                                meets_deadlines = item.meets_deadlines,
                                punctuality = item.punctuality,
                                attendance = item.attendance,
                                dress_code = item.dress_code,
                                respond_contribution = item.respond_contribution,
                                respond_suggestion = item.respond_suggestion,
                                gives_and_seeks = item.gives_and_seeks,
                                adapts_well = item.adapts_well,
                                synchronization = item.synchronization,
                                accept_challange = item.accept_challange,
                                innovative_thinking = item.innovative_thinking,
                                motivation_emotional = item.motivation_emotional,

                                this_year_biggest_impact = item.this_year_biggest_impact,
                                specific_area_or_skill = item.specific_area_or_skill,
                                overall_assessment = item.overall_assessment,
                                remark_if_any = item.remark_if_any,

                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,

                                appr_year = item.appr_year,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.EvalFormDataList = RetLst;
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
        public async Task<EvalFormReturn> GetEvalDetail(EvalFormModel getModel)
        {
            EvalFormReturn RObj = new EvalFormReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new EvalFormModel
            {
                Operation = 1,
                app_user = getModel.app_user,
                dep_id = getModel.dep_id,
                company_id = getModel.company_id,
                res_status = getModel.res_status,
                res_message = getModel.res_message,

                eval_id = getModel.eval_id,
                appr_id = getModel.appr_id,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                designation = getModel.designation,
                dep_name = getModel.dep_name,

                gross_salary = getModel.gross_salary,
                service_tenure = getModel.service_tenure,

                area_unit = getModel.area_unit,
                successor_to = getModel.successor_to,
                reporting_to = getModel.reporting_to,

                edu_rel_certi = getModel.edu_rel_certi,
                edu_rel_job = getModel.edu_rel_job,

                core_skill_ability = getModel.core_skill_ability,
                take_interest = getModel.take_interest,
                problem_solving = getModel.problem_solving,
                accuracy = getModel.accuracy,
                adherence = getModel.adherence,
                take_intiative = getModel.take_intiative,
                meets_deadlines = getModel.meets_deadlines,
                punctuality = getModel.punctuality,
                attendance = getModel.attendance,
                dress_code = getModel.dress_code,
                respond_contribution = getModel.respond_contribution,
                respond_suggestion = getModel.respond_suggestion,
                gives_and_seeks = getModel.gives_and_seeks,
                adapts_well = getModel.adapts_well,
                synchronization = getModel.synchronization,
                accept_challange = getModel.accept_challange,
                innovative_thinking = getModel.innovative_thinking,
                motivation_emotional = getModel.motivation_emotional,

                this_year_biggest_impact = getModel.this_year_biggest_impact,
                specific_area_or_skill = getModel.specific_area_or_skill,
                overall_assessment = getModel.overall_assessment,
                remark_if_any = getModel.remark_if_any,

                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
               

                appr_year = getModel.appr_year
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/evaluation-form";


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
                        var apiResponse = JsonConvert.DeserializeObject<EvalFormApiResponse>(jsonResponse);

                        List<EvalFormModel> RetLst = new List<EvalFormModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            EvalFormModel RetObj = new EvalFormModel
                            {
                                Operation = item.Operation,
                                app_user = item.app_user,
                                dep_id = item.dep_id,
                                company_id = item.company_id,
                                res_status = item.res_status,
                                res_message = item.res_message,

                                eval_id = item.eval_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                dep_name = item.dep_name,

                                gross_salary = item.gross_salary,
                                service_tenure = item.service_tenure,

                                area_unit = item.area_unit,
                                successor_to = item.successor_to,
                                reporting_to = item.reporting_to,

                                edu_rel_certi = item.edu_rel_certi,
                                edu_rel_job = item.edu_rel_job,

                                core_skill_ability = item.core_skill_ability,
                                take_interest = item.take_interest,
                                problem_solving = item.problem_solving,
                                accuracy = item.accuracy,
                                adherence = item.adherence,
                                take_intiative = item.take_intiative,
                                meets_deadlines = item.meets_deadlines,
                                punctuality = item.punctuality,
                                attendance = item.attendance,
                                dress_code = item.dress_code,
                                respond_contribution = item.respond_contribution,
                                respond_suggestion = item.respond_suggestion,
                                gives_and_seeks = item.gives_and_seeks,
                                adapts_well = item.adapts_well,
                                synchronization = item.synchronization,
                                accept_challange = item.accept_challange,
                                innovative_thinking = item.innovative_thinking,
                                motivation_emotional = item.motivation_emotional,

                                this_year_biggest_impact = item.this_year_biggest_impact,
                                specific_area_or_skill = item.specific_area_or_skill,
                                overall_assessment = item.overall_assessment,
                                remark_if_any = item.remark_if_any,

                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                appr_year = item.appr_year,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.EvalFormDataList = RetLst;
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
        public async Task<EvalFormReturn> UpdateEvalForm(EvalFormModel getModel)
        {
            EvalFormReturn RObj = new EvalFormReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new EvalFormModel
            {
                Operation = 3,
                app_user = getModel.app_user,
                dep_id = getModel.dep_id,
                company_id = getModel.company_id,
                res_status = getModel.res_status,
                res_message = getModel.res_message,

                eval_id = getModel.eval_id,
                appr_id = getModel.appr_id,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                designation = getModel.designation,
                dep_name = getModel.dep_name,

                gross_salary = getModel.gross_salary,
                service_tenure = getModel.service_tenure,

                area_unit = getModel.area_unit,
                successor_to = getModel.successor_to,
                reporting_to = getModel.reporting_to,

                edu_rel_certi = getModel.edu_rel_certi,
                edu_rel_job = getModel.edu_rel_job,

                core_skill_ability = getModel.core_skill_ability,
                take_interest = getModel.take_interest,
                problem_solving = getModel.problem_solving,
                accuracy = getModel.accuracy,
                adherence = getModel.adherence,
                take_intiative = getModel.take_intiative,
                meets_deadlines = getModel.meets_deadlines,
                punctuality = getModel.punctuality,
                attendance = getModel.attendance,
                dress_code = getModel.dress_code,
                respond_contribution = getModel.respond_contribution,
                respond_suggestion = getModel.respond_suggestion,
                gives_and_seeks = getModel.gives_and_seeks,
                adapts_well = getModel.adapts_well,
                synchronization = getModel.synchronization,
                accept_challange = getModel.accept_challange,
                innovative_thinking = getModel.innovative_thinking,
                motivation_emotional = getModel.motivation_emotional,

                grand_total = getModel.grand_total,
                this_year_biggest_impact = getModel.this_year_biggest_impact,
                specific_area_or_skill = getModel.specific_area_or_skill,
                overall_assessment = getModel.overall_assessment,
                remark_if_any = getModel.remark_if_any,

                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,

                appr_year = getModel.appr_year
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/evaluation-form";


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
                        var apiResponse = JsonConvert.DeserializeObject<EvalFormApiResponse>(jsonResponse);

                        List<EvalFormModel> RetLst = new List<EvalFormModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            EvalFormModel RetObj = new EvalFormModel
                            {
                                Operation = item.Operation,
                                app_user = item.app_user,
                                dep_id = item.dep_id,
                                company_id = item.company_id,
                                res_status = item.res_status,
                                res_message = item.res_message,

                                eval_id = item.eval_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                designation = item.designation,
                                dep_name = item.dep_name,

                                gross_salary = item.gross_salary,
                                service_tenure = item.service_tenure,

                                area_unit = item.area_unit,
                                successor_to = item.successor_to,
                                reporting_to = item.reporting_to,

                                edu_rel_certi = item.edu_rel_certi,
                                edu_rel_job = item.edu_rel_job,

                                core_skill_ability = item.core_skill_ability,
                                take_interest = item.take_interest,
                                problem_solving = item.problem_solving,
                                accuracy = item.accuracy,
                                adherence = item.adherence,
                                take_intiative = item.take_intiative,
                                meets_deadlines = item.meets_deadlines,
                                punctuality = item.punctuality,
                                attendance = item.attendance,
                                dress_code = item.dress_code,
                                respond_contribution = item.respond_contribution,
                                respond_suggestion = item.respond_suggestion,
                                gives_and_seeks = item.gives_and_seeks,
                                adapts_well = item.adapts_well,
                                synchronization = item.synchronization,
                                accept_challange = item.accept_challange,
                                innovative_thinking = item.innovative_thinking,
                                motivation_emotional = item.motivation_emotional,

                                this_year_biggest_impact = item.this_year_biggest_impact,
                                specific_area_or_skill = item.specific_area_or_skill,
                                overall_assessment = item.overall_assessment,
                                remark_if_any = item.remark_if_any,

                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,

                                appr_year = item.appr_year,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        //if (retstatus == 2)
                        //{
                        //    RObj.EvalFormDataList = RetLst;
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