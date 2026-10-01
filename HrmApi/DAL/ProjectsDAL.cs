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
    public class ProjectsDAL
    {
        private string _ConnString; //variable for connection string
        public ProjectsDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Projects

        public async Task<ProjectReturn> GetDepProject(ProjectModel getModel)
        {
            ProjectReturn RObj = new ProjectReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ProjectModel
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                proj_id = getModel.proj_id,
                proj_name = getModel.proj_name,
                plan_s_date = getModel.plan_s_date, 
                str_plan_s_date = getModel.str_plan_s_date,
                plan_e_date = getModel.plan_e_date,
                str_plan_e_date = getModel.str_plan_e_date,
                s_date = getModel.s_date,
                str_s_date = getModel.str_s_date,
                e_date = getModel.e_date,
                str_e_date = getModel.str_e_date,
                company = getModel.company,
                dep_id = getModel.dep_id,
                comp_name = getModel.comp_name,
                dep_name = getModel.dep_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                created_by = getModel.created_by,
                created_by_name = getModel.created_by_name,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                updated_by = getModel.updated_by,
                updated_by_name = getModel.updated_by_name
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/projects";


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
                        var apiResponse = JsonConvert.DeserializeObject<ProjectsApiResponse>(jsonResponse);

                        List<ProjectModel> RetLst = new List<ProjectModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ProjectModel RetObj = new ProjectModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                proj_id = item.proj_id,
                                proj_name = item.proj_name,
                                plan_s_date = item.plan_s_date,
                                str_plan_s_date = item.str_plan_s_date,
                                plan_e_date = item.plan_e_date,
                                str_plan_e_date = item.str_plan_e_date,
                                s_date = item.s_date,
                                str_s_date = item.str_s_date,
                                e_date = item.e_date,
                                str_e_date = item.str_e_date,
                                company = item.company,
                                dep_id = item.dep_id,
                                comp_name = item.comp_name,
                                dep_name = item.dep_name,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                created_by = item.created_by,
                                created_by_name = item.created_by_name,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                updated_by = item.updated_by,
                                updated_by_name = item.updated_by_name


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ProjectsDataList = RetLst;
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

        public async Task<ProjectReturn> GetByIdProject(ProjectModel getModel)
        {
            ProjectReturn RObj = new ProjectReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ProjectModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                proj_id = getModel.proj_id,
                proj_name = getModel.proj_name,
                plan_s_date = getModel.plan_s_date,
                str_plan_s_date = getModel.str_plan_s_date,
                plan_e_date = getModel.plan_e_date,
                str_plan_e_date = getModel.str_plan_e_date,
                s_date = getModel.s_date,
                str_s_date = getModel.str_s_date,
                e_date = getModel.e_date,
                str_e_date = getModel.str_e_date,
                company = getModel.company,
                dep_id = getModel.dep_id,
                comp_name = getModel.comp_name,
                dep_name = getModel.dep_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                created_by = getModel.created_by,
                created_by_name = getModel.created_by_name,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                updated_by = getModel.updated_by,
                updated_by_name = getModel.updated_by_name
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/projects";


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
                        var apiResponse = JsonConvert.DeserializeObject<ProjectsApiResponse>(jsonResponse);

                        List<ProjectModel> RetLst = new List<ProjectModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ProjectModel RetObj = new ProjectModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                proj_id = item.proj_id,
                                proj_name = item.proj_name,
                                plan_s_date = item.plan_s_date,
                                str_plan_s_date = item.str_plan_s_date,
                                plan_e_date = item.plan_e_date,
                                str_plan_e_date = item.str_plan_e_date,
                                s_date = item.s_date,
                                str_s_date = item.str_s_date,
                                e_date = item.e_date,
                                str_e_date = item.str_e_date,
                                company = item.company,
                                dep_id = item.dep_id,
                                comp_name = item.comp_name,
                                dep_name = item.dep_name,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                created_by = item.created_by,
                                created_by_name = item.created_by_name,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                updated_by = item.updated_by,
                                updated_by_name = item.updated_by_name


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ProjectsDataList = RetLst;
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



        public async Task<ProjectReturn> AUDProjects(ProjectModel getModel)
        {
            ProjectReturn RObj = new ProjectReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int newWpId = 0;
            int operation = getModel.Operation;

            var attendanceRequest = new ProjectModel
            {

                Operation = getModel.Operation,
                app_user = getModel.app_user,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                proj_id = getModel.proj_id,
                proj_name = getModel.proj_name,
                plan_s_date = getModel.plan_s_date,
                str_plan_s_date = getModel.str_plan_s_date,
                plan_e_date = getModel.plan_e_date,
                str_plan_e_date = getModel.str_plan_e_date,
                s_date = getModel.s_date,
                str_s_date = getModel.str_s_date,
                e_date = getModel.e_date,
                str_e_date = getModel.str_e_date,
                company = getModel.company,
                dep_id = getModel.dep_id,
                comp_name = getModel.comp_name,
                dep_name = getModel.dep_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                created_by = getModel.created_by,
                created_by_name = getModel.created_by_name,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                updated_by = getModel.updated_by,
                updated_by_name = getModel.updated_by_name

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/projects";


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
                        var apiResponse = JsonConvert.DeserializeObject<ProjectsApiResponse>(jsonResponse);

                        List<ProjectModel> RetLst = new List<ProjectModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newWpId = item.new_proj_id;

                        }
                        


                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_proj_id = newWpId;
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