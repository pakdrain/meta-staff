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
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HrmApi.DAL
{
    public class TaskDAL
    {
        private string _ConnString; //variable for connection string
        public TaskDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Tasks

        public async Task<TasksModelReturn> TaskById(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TasksModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                creater_dep = item.creater_dep,
                                creater_dep_name = item.creater_dep_name,
                                re_open_by = item.re_open_by,
                                re_open_date = item.re_open_date,   
                                str_re_open_date = item.str_re_open_date,
                                task_sub_type_name = item.task_sub_type_name,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TasksDataList = RetLst;
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
        public async Task<TasksModelReturn> GetMyTasks(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TasksModel
            {
                Operation = 7,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                creater_dep = item.creater_dep,
                                creater_dep_name = item.creater_dep_name,
                                re_open_by = item.re_open_by,
                                re_open_date = item.re_open_date,
                                str_re_open_date = item.str_re_open_date,
                                task_sub_type_name = item.task_sub_type_name,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,

                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TasksDataList = RetLst;
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



        public async Task<TasksModelReturn> GetHODTasks(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TasksModel
            {
                Operation = 9,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                creater_dep = item.creater_dep,
                                creater_dep_name = item.creater_dep_name,
                                re_open_by = item.re_open_by,
                                re_open_date = item.re_open_date,
                                str_re_open_date = item.str_re_open_date,
                                task_sub_type_name = item.task_sub_type_name,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,

                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TasksDataList = RetLst;
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

        public async Task<TasksModelReturn> GetEDTasks(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TasksModel
            {
                Operation = 10,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                creater_dep = item.creater_dep,
                                creater_dep_name = item.creater_dep_name,
                                re_open_by = item.re_open_by,
                                re_open_date = item.re_open_date,
                                str_re_open_date = item.str_re_open_date,
                                task_sub_type_name = item.task_sub_type_name,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                acc_kpi_out = item.acc_kpi_out,
                                acc_kpi_in = item.acc_kpi_in,
                                com_kpi_out = item.com_kpi_out,
                                com_kpi_in = item.com_kpi_in,

                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TasksDataList = RetLst;
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

        public async Task<TasksModelReturn> GetAssignableTasks(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TasksModel
            {
                Operation = 8,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                creater_dep = item.creater_dep,
                                creater_dep_name = item.creater_dep_name,
                                re_open_by = item.re_open_by,
                                re_open_date = item.re_open_date,
                                str_re_open_date = item.str_re_open_date,
                                task_sub_type_name = item.task_sub_type_name,
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,

                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TasksDataList = RetLst;
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

        public async Task<TasksModelReturn> GetAddTasks(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TasksModel
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                creater_dep = item.creater_dep,
                                creater_dep_name = item.creater_dep_name,
                                re_open_by = item.re_open_by,
                                re_open_date = item.re_open_date,
                                str_re_open_date = item.str_re_open_date,
                                task_sub_type_name = item.task_sub_type_name,
                               
                                hod_app_by = item.hod_app_by,
                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,

                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TasksDataList = RetLst;
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

        public async Task<TasksModelReturn> AUDTasks(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;

            string retmessage = "Api is not Fetching!";
            int taskId = 0;
            var attendanceRequest = new TasksModel
            {
                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            taskId = item.new_task_id;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,                                
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2 && getModel.Operation == 2   )
                        {
                            if (!string.IsNullOrEmpty(getModel.t_doc) && !string.IsNullOrEmpty(getModel.t_doc_ext))
                            {
                                SaveBase64File(getModel.t_doc, getModel.t_doc_ext, "~/TaskDocument/", taskId.ToString());
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

        public async Task<TasksModelReturn> TaskStatusChanger(TasksModel getModel)
        {
            TasksModelReturn RObj = new TasksModelReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TasksModel
            {
                Operation = 6,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                dep_id = getModel.dep_id,
                task_id = getModel.task_id,
                task_no = getModel.task_no,
                task_type = getModel.task_type,
                task_sub_type = getModel.task_sub_type,
                task_title = getModel.task_title,
                task_desc = getModel.task_desc,
                status = getModel.status,
                t_doc_ext = getModel.t_doc_ext,
                assigner_by = getModel.assigner_by,
                assigner_date = getModel.assigner_date,
                str_assigner_date = getModel.str_assigner_date,
                assigner_remarks = getModel.assigner_remarks,
                assigned_persons = getModel.assigned_persons,
                t_priority = getModel.t_priority,
                complete_by = getModel.complete_by,
                complete_date = getModel.complete_date,
                str_complete_date = getModel.str_complete_date,
                complete_remarks = getModel.complete_remarks,
                re_ass_by = getModel.re_ass_by,
                re_ass_date = getModel.re_ass_date,
                str_re_ass_date = getModel.str_re_ass_date,
                close_by = getModel.close_by,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,
                action_page = getModel.action_page,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                dep_name = getModel.dep_name,
                com_name = getModel.com_name,
                t_doc_url = getModel.t_doc_url,
                ass_task_id = getModel.ass_task_id,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
                emp_id = getModel.emp_id,
                AssignedDetail = getModel.AssignedDetail,



            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/tasks";


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
                        var apiResponse = JsonConvert.DeserializeObject<TasksModelApiResponse>(jsonResponse);

                        List<TasksModel> RetLst = new List<TasksModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TasksModel RetObj = new TasksModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                dep_id = item.dep_id,
                                task_id = item.task_id,
                                task_no = item.task_no,
                                task_type = item.task_type,
                                task_sub_type = item.task_sub_type,
                                task_title = item.task_title,
                                task_desc = item.task_desc,
                                status = item.status,
                                t_doc_ext = item.t_doc_ext,
                                assigner_by = item.assigner_by,
                                assigner_date = item.assigner_date,
                                str_assigner_date = item.str_assigner_date,
                                assigner_remarks = item.assigner_remarks,
                                assigned_persons = item.assigned_persons,
                                t_priority = item.t_priority,
                                complete_by = item.complete_by,
                                complete_date = item.complete_date,
                                str_complete_date = item.str_complete_date,
                                complete_remarks = item.complete_remarks,
                                re_ass_by = item.re_ass_by,
                                re_ass_date = item.re_ass_date,
                                str_re_ass_date = item.str_re_ass_date,
                                close_by = item.close_by,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,
                                action_page = item.action_page,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                dep_name = item.dep_name,
                                com_name = item.com_name,
                                t_doc_url = item.t_doc_url,
                                ass_task_id = item.ass_task_id,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                str_from_date = item.str_from_date,
                                str_to_date = item.str_to_date,
                                user_emp_id = item.user_emp_id,
                                emp_id = item.emp_id,
                                AssignedDetail = item.AssignedDetail,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);
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

        public async Task<TaskTypesReturn> GetTaskType(TaskTypes getModel)
        {
            TaskTypesReturn RObj = new TaskTypesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TaskTypes
            {
                Operation = 10,
                sr_id = getModel.sr_id,
                task_type_id = getModel.task_type_id,
                task_type_name = getModel.task_type_name,
                task_list_id = getModel.task_list_id,
                task_list_name = getModel.task_list_name,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                created_by = getModel.created_by,
                updated_by = getModel.updated_by,
                client_code = getModel.client_code,
                company = getModel.company,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                task_type_sr_id = getModel.task_type_sr_id,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/task-types";


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
                        var apiResponse = JsonConvert.DeserializeObject<TaskTypessApiResponse>(jsonResponse);

                        List<TaskTypes> RetLst = new List<TaskTypes>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TaskTypes RetObj = new TaskTypes
                            {
                                Operation = item.Operation,
                                sr_id = item.sr_id,
                                task_type_id = item.task_type_id,
                                task_type_name = item.task_type_name,
                                task_list_id = item.task_list_id,
                                task_list_name = item.task_list_name,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                user_emp_id = item.user_emp_id,
                                task_type_sr_id = item.task_type_sr_id,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TaskTypesDataList = RetLst;
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

        public async Task<TaskTypesReturn> AUDTaskList(TaskTypes getModel)
        {
            TaskTypesReturn RObj = new TaskTypesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TaskTypes
            {
                Operation = getModel.Operation,
                sr_id = getModel.sr_id,
                task_type_id = getModel.task_type_id,
                task_type_name = getModel.task_type_name,
                task_list_id = getModel.task_list_id,
                task_list_name = getModel.task_list_name,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                created_by = getModel.created_by,
                updated_by = getModel.updated_by,
                client_code = getModel.client_code,
                company = getModel.company,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                task_type_sr_id = getModel.task_type_sr_id,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/task-types";


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
                        var apiResponse = JsonConvert.DeserializeObject<TaskTypessApiResponse>(jsonResponse);

                        List<TaskTypes> RetLst = new List<TaskTypes>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TaskTypes RetObj = new TaskTypes
                            {
                                Operation = item.Operation,
                                sr_id = item.sr_id,
                                task_type_id = item.task_type_id,
                                task_type_name = item.task_type_name,
                                task_list_id = item.task_list_id,
                                task_list_name = item.task_list_name,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                user_emp_id = item.user_emp_id,
                                task_type_sr_id = item.task_type_sr_id,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);
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

        public async Task<TaskTypesReturn> GetTaskList(TaskTypes getModel)
        {
            TaskTypesReturn RObj = new TaskTypesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TaskTypes
            {
                Operation = 13,
                sr_id = getModel.sr_id,
                task_type_id = getModel.task_type_id,
                task_type_name = getModel.task_type_name,
                task_list_id = getModel.task_list_id,
                task_list_name = getModel.task_list_name,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                created_by = getModel.created_by,
                updated_by = getModel.updated_by,
                client_code = getModel.client_code,
                company = getModel.company,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                task_type_sr_id = getModel.task_type_sr_id,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/task-types";


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
                        var apiResponse = JsonConvert.DeserializeObject<TaskTypessApiResponse>(jsonResponse);

                        List<TaskTypes> RetLst = new List<TaskTypes>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TaskTypes RetObj = new TaskTypes
                            {
                                Operation = item.Operation,
                                sr_id = item.sr_id,
                                task_type_id = item.task_type_id,
                                task_type_name = item.task_type_name,
                                task_list_id = item.task_list_id,
                                task_list_name = item.task_list_name,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                user_emp_id = item.user_emp_id,
                                task_type_sr_id = item.task_type_sr_id,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TaskTypesDataList = RetLst;
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

        public async Task<TaskTypesReturn> AUDTaskPortion(TaskTypes getModel)
        {
            TaskTypesReturn RObj = new TaskTypesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TaskTypes
            {
                Operation = getModel.Operation,
                sr_id = getModel.sr_id,
                task_type_id = getModel.task_type_id,
                task_type_name = getModel.task_type_name,
                task_list_id = getModel.task_list_id,
                task_list_name = getModel.task_list_name,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                created_by = getModel.created_by,
                updated_by = getModel.updated_by,
                client_code = getModel.client_code,
                company = getModel.company,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                task_type_sr_id = getModel.task_type_sr_id,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/task-types";


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
                        var apiResponse = JsonConvert.DeserializeObject<TaskTypessApiResponse>(jsonResponse);

                        List<TaskTypes> RetLst = new List<TaskTypes>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TaskTypes RetObj = new TaskTypes
                            {
                                Operation = item.Operation,
                                sr_id = item.sr_id,
                                task_type_id = item.task_type_id,
                                task_type_name = item.task_type_name,
                                task_list_id = item.task_list_id,
                                task_list_name = item.task_list_name,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                user_emp_id = item.user_emp_id,
                                task_type_sr_id = item.task_type_sr_id,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);
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

        public async Task<TaskTypesReturn> GetTaskPortion(TaskTypes getModel)
        {
            TaskTypesReturn RObj = new TaskTypesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new TaskTypes
            {
                Operation = 18,
                sr_id = getModel.sr_id,
                task_type_id = getModel.task_type_id,
                task_type_name = getModel.task_type_name,
                task_list_id = getModel.task_list_id,
                task_list_name = getModel.task_list_name,
                portion_id = getModel.portion_id,
                portion_name = getModel.portion_name,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                created_by = getModel.created_by,
                updated_by = getModel.updated_by,
                client_code = getModel.client_code,
                company = getModel.company,
                emp_id = getModel.emp_id,
                user_emp_id = getModel.user_emp_id,
                task_type_sr_id = getModel.task_type_sr_id,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/task-types";


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
                        var apiResponse = JsonConvert.DeserializeObject<TaskTypessApiResponse>(jsonResponse);

                        List<TaskTypes> RetLst = new List<TaskTypes>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            TaskTypes RetObj = new TaskTypes
                            {
                                Operation = item.Operation,
                                sr_id = item.sr_id,
                                task_type_id = item.task_type_id,
                                task_type_name = item.task_type_name,
                                task_list_id = item.task_list_id,
                                task_list_name = item.task_list_name,
                                portion_id = item.portion_id,
                                portion_name = item.portion_name,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                user_emp_id = item.user_emp_id,
                                task_type_sr_id = item.task_type_sr_id,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.TaskTypesDataList = RetLst;
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

        public TaskChatsReturn GetTaskChats(TaskChat getModel) // get modal from user in getModel
        {
            TaskChatsReturn RObj = new TaskChatsReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[5];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[4] = new SqlParameter("@task_id", SqlDbType.Int) { Value = getModel.task_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_msg_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";


                // for single object declaration
                var RetObj = new TaskChat();
                // for list object declaration
                List<TaskChat> RetLst = new List<TaskChat>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskChat(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.msg_id = Common.CheckIntegerNull(row["msg_id"]);
                        RetObj.task_id = Common.CheckIntegerNull(row["task_id"]);
                        RetObj.task_msg = Common.CheckStringNull(row["task_msg"]);
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
                        if (!string.IsNullOrEmpty(row["created_date"].ToString()) && row["created_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["created_date"]);
                            strCreatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCreatedDate = "00-000-0000";
                        }
                        RetObj.str_created_date = strCreatedDate;


                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.TaskChatDataList = RetLst;
                return RObj; //finaly return the object
            }
            // if api calling error
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }

        }

        public TaskChatsReturn AUDTaskChats(TaskChat getModel)
        {
            TaskChatsReturn RObj = new TaskChatsReturn(); // For result body object declaration


            try
            {
                int retstatus = 50;
                string retmessage = "No Data Found!";
                // Parameters array for the stored procedure
                var parm = new SqlParameter[7];
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("msg_id", SqlDbType.Int) { Value = getModel.msg_id };
                parm[2] = new SqlParameter("task_id", SqlDbType.Int) { Value = getModel.task_id };
                parm[3] = new SqlParameter("task_msg", SqlDbType.VarChar) { Value = getModel.task_msg };
                parm[4] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[5] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                parm[6] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                // Execute the stored procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_msg_proc]", parm);
                foreach (DataRow row in dsuser.Rows)
                {
                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);


                }


                RObj.Status = retstatus;
                RObj.Message = retmessage;
                return RObj;
            }
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found: " + ex.Message;
                return RObj;
            }
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