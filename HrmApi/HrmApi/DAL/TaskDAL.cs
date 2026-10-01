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
        ///////////////// GET ALL task DAL //////////////////////////

        public TaskReturn GetAllTasks(TaskModel getModel) // get modal from user in getModel
        {
            TaskReturn RObj = new TaskReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.Int) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";
                var strExpectedDate = "";
                var strClosedDate = "";
                var strAssignDate = "";
                var strCompeleteDate = "";

                // for single object declaration
                var RetObj = new TaskModel();
                // for list object declaration
                List<TaskModel> RetLst = new List<TaskModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.task_id = Common.CheckIntegerNull(row["task_id"]);
                        RetObj.ticket_no = Common.CheckIntegerNull(row["ticket_no"]);
                        RetObj.from_com = Common.CheckStringNull(row["from_com"]);
                        RetObj.from_dep = Common.CheckStringNull(row["from_dep"]);
                        RetObj.task_from = Common.CheckStringNull(row["task_from"]);
                        RetObj.expected_date = Common.CheckDateTimeNull(row["expected_date"]);
                        if (!string.IsNullOrEmpty(row["expected_date"].ToString()) && row["expected_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["expected_date"]);
                            strExpectedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strExpectedDate = "00-000-0000";
                        }
                        RetObj.str_expected_date = strExpectedDate;
                        RetObj.closed_date = Common.CheckDateTimeNull(row["closed_date"]);
                        if (!string.IsNullOrEmpty(row["closed_date"].ToString()) && row["closed_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["closed_date"]);
                            strClosedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strClosedDate = "00-000-0000";
                        }
                        RetObj.str_closed_date = strClosedDate;
                        RetObj.assign_from_to_show = Common.CheckStringNull(row["assign_from_to_show"]);
                        RetObj.assign_from = Common.CheckStringNull(row["assign_from"]);
                        RetObj.assign_date = Common.CheckDateTimeNull(row["assign_date"]);
                        if (!string.IsNullOrEmpty(row["assign_date"].ToString()) && row["assign_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["assign_date"]);
                            strAssignDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strAssignDate = "00-000-0000";
                        }
                        RetObj.str_assign_date = strAssignDate;
                        RetObj.to_com = Common.CheckStringNull(row["to_com"]);
                        RetObj.to_dep = Common.CheckStringNull(row["to_dep"]);
                        RetObj.task_to = Common.CheckStringNull(row["task_to"]);
                        RetObj.task_to_to_show = Common.CheckStringNull(row["task_to_to_show"]);
                        RetObj.compelete_date = Common.CheckDateTimeNull(row["compelete_date"]);
                        if (!string.IsNullOrEmpty(row["compelete_date"].ToString()) && row["compelete_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["compelete_date"]);
                            strCompeleteDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCompeleteDate = "00-000-0000";
                        }
                        RetObj.str_compelete_date = strCompeleteDate;
                        RetObj.task_status = Common.CheckStringNull(row["task_status"]);
                        RetObj.task_img = Common.CheckStringNull(row["task_img"]);
                        RetObj.task_doc = Common.CheckStringNull(row["task_doc"]);
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
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.task_title = Common.CheckStringNull(row["task_title"]);
                        RetObj.task_desc = Common.CheckStringNull(row["task_desc"]);
                        RetObj.task_progress = Common.CheckIntegerNull(row["task_progress"]);
                        RetObj.mob_no = Common.CheckStringNull(row["mob_no"]);
                        RetObj.task_priority = Common.CheckStringNull(row["task_priority"]);
                        RetObj.from_com_to_show = Common.CheckStringNull(row["from_com_to_show"]);
                        RetObj.task_list_id = Common.CheckIntegerNull(row["task_list_id"]);
                        RetObj.task_list_id_to_show = Common.CheckStringNull(row["task_list_id_to_show"]);
                        RetObj.task_dep_to_show = Common.CheckStringNull(row["task_dep_to_show"]);
                        RetObj.doc_url = Common.CheckStringNull(row["task_url"]);
                        RetObj.rejected_remark = Common.CheckStringNull(row["rejected_remark"]);
                        RetObj.task_to_to_show = Common.CheckStringNull(row["task_to_to_show"]);

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
                RObj.TaskDataList = RetLst;
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

        ///////////////// GET ALL task DAL //////////////////////////

        public TaskReturn GetTaskById(TaskModel getModel) // get modal from user in getModel
        {
            TaskReturn RObj = new TaskReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[5];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 5 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[4] = new SqlParameter("@task_id", SqlDbType.VarChar) { Value = getModel.task_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";
                var strExpectedDate = "";
                var strClosedDate = "";
                var strAssignDate = "";
                var strCompeleteDate = "";

                // for single object declaration
                var RetObj = new TaskModel();
                // for list object declaration
                List<TaskModel> RetLst = new List<TaskModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.task_id = Common.CheckIntegerNull(row["task_id"]);
                        RetObj.ticket_no = Common.CheckIntegerNull(row["ticket_no"]);
                        RetObj.from_com = Common.CheckStringNull(row["from_com"]);
                        RetObj.from_dep = Common.CheckStringNull(row["from_dep"]);
                        RetObj.task_from = Common.CheckStringNull(row["task_from"]);
                        RetObj.expected_date = Common.CheckDateTimeNull(row["expected_date"]);
                        if (!string.IsNullOrEmpty(row["expected_date"].ToString()) && row["expected_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["expected_date"]);
                            strExpectedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strExpectedDate = "00-000-0000";
                        }
                        RetObj.str_expected_date = strExpectedDate;
                        RetObj.closed_date = Common.CheckDateTimeNull(row["closed_date"]);
                        if (!string.IsNullOrEmpty(row["closed_date"].ToString()) && row["closed_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["closed_date"]);
                            strClosedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strClosedDate = "00-000-0000";
                        }
                        RetObj.str_closed_date = strClosedDate;
                        RetObj.assign_from_to_show = Common.CheckStringNull(row["assign_from_to_show"]);
                        RetObj.assign_from = Common.CheckStringNull(row["assign_from"]);
                        RetObj.assign_date = Common.CheckDateTimeNull(row["assign_date"]);
                        if (!string.IsNullOrEmpty(row["assign_date"].ToString()) && row["assign_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["assign_date"]);
                            strAssignDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strAssignDate = "00-000-0000";
                        }
                        RetObj.str_assign_date = strAssignDate;
                        RetObj.to_com = Common.CheckStringNull(row["to_com"]);
                        RetObj.to_dep = Common.CheckStringNull(row["to_dep"]);
                        RetObj.task_to = Common.CheckStringNull(row["task_to"]);
                        RetObj.task_to_to_show = Common.CheckStringNull(row["task_to_to_show"]);
                        RetObj.compelete_date = Common.CheckDateTimeNull(row["compelete_date"]);
                        if (!string.IsNullOrEmpty(row["compelete_date"].ToString()) && row["compelete_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["compelete_date"]);
                            strCompeleteDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCompeleteDate = "00-000-0000";
                        }
                        RetObj.str_compelete_date = strCompeleteDate;
                        RetObj.task_status = Common.CheckStringNull(row["task_status"]);
                        RetObj.task_img = Common.CheckStringNull(row["task_img"]);
                        RetObj.task_doc = Common.CheckStringNull(row["task_doc"]);
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
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.task_title = Common.CheckStringNull(row["task_title"]);
                        RetObj.task_desc = Common.CheckStringNull(row["task_desc"]);
                        RetObj.task_progress = Common.CheckIntegerNull(row["task_progress"]);
                        RetObj.mob_no = Common.CheckStringNull(row["mob_no"]);
                        RetObj.task_priority = Common.CheckStringNull(row["task_priority"]);
                        RetObj.from_com_to_show = Common.CheckStringNull(row["from_com_to_show"]);
                        RetObj.task_list_id = Common.CheckIntegerNull(row["task_list_id"]);
                        RetObj.task_list_id_to_show = Common.CheckStringNull(row["task_list_id_to_show"]);
                        RetObj.task_dep_to_show = Common.CheckStringNull(row["task_dep_to_show"]);
                        RetObj.rejected_remark = Common.CheckStringNull(row["rejected_remark"]);

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
                RObj.TaskDataList = RetLst;
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


        ///////////////// GET GIVEN TASKS DAL //////////////////////////

        public TaskReturn GetGivenTasks(TaskModel getModel) // get modal from user in getModel
        {
            TaskReturn RObj = new TaskReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 7 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";
                var strExpectedDate = "";
                var strClosedDate = "";
                var strAssignDate = "";
                var strCompeleteDate = "";

                // for single object declaration
                var RetObj = new TaskModel();
                // for list object declaration
                List<TaskModel> RetLst = new List<TaskModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.task_id = Common.CheckIntegerNull(row["task_id"]);
                        RetObj.ticket_no = Common.CheckIntegerNull(row["ticket_no"]);
                        RetObj.from_com = Common.CheckStringNull(row["from_com"]);
                        RetObj.from_dep = Common.CheckStringNull(row["from_dep"]);
                        RetObj.task_from = Common.CheckStringNull(row["task_from"]);
                        RetObj.expected_date = Common.CheckDateTimeNull(row["expected_date"]);
                        if (!string.IsNullOrEmpty(row["expected_date"].ToString()) && row["expected_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["expected_date"]);
                            strExpectedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strExpectedDate = "00-000-0000";
                        }
                        RetObj.str_expected_date = strExpectedDate;
                        RetObj.closed_date = Common.CheckDateTimeNull(row["closed_date"]);
                        if (!string.IsNullOrEmpty(row["closed_date"].ToString()) && row["closed_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["closed_date"]);
                            strClosedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strClosedDate = "00-000-0000";
                        }
                        RetObj.str_closed_date = strClosedDate;
                        RetObj.assign_from = Common.CheckStringNull(row["assign_from"]);
                        RetObj.assign_from_to_show = Common.CheckStringNull(row["assign_from_to_show"]);
                        RetObj.assign_date = Common.CheckDateTimeNull(row["assign_date"]);
                        if (!string.IsNullOrEmpty(row["assign_date"].ToString()) && row["assign_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["assign_date"]);
                            strAssignDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strAssignDate = "00-000-0000";
                        }
                        RetObj.str_assign_date = strAssignDate;
                        RetObj.to_com = Common.CheckStringNull(row["to_com"]);
                        RetObj.to_dep = Common.CheckStringNull(row["to_dep"]);
                        RetObj.task_to = Common.CheckStringNull(row["task_to"]);
                        RetObj.compelete_date = Common.CheckDateTimeNull(row["compelete_date"]);
                        if (!string.IsNullOrEmpty(row["compelete_date"].ToString()) && row["compelete_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["compelete_date"]);
                            strCompeleteDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCompeleteDate = "00-000-0000";
                        }
                        RetObj.str_compelete_date = strCompeleteDate;
                        RetObj.task_status = Common.CheckStringNull(row["task_status"]);
                        RetObj.task_img = Common.CheckStringNull(row["task_img"]);
                        RetObj.task_doc = Common.CheckStringNull(row["task_doc"]);
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
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.task_title = Common.CheckStringNull(row["task_title"]);
                        RetObj.task_desc = Common.CheckStringNull(row["task_desc"]);
                        RetObj.mob_no = Common.CheckStringNull(row["mob_no"]);
                        RetObj.task_priority = Common.CheckStringNull(row["task_priority"]);
                        RetObj.from_com_to_show = Common.CheckStringNull(row["from_com_to_show"]);
                        RetObj.task_list_id = Common.CheckIntegerNull(row["task_list_id"]);
                        RetObj.task_list_id_to_show = Common.CheckStringNull(row["task_list_id_to_show"]);
                        RetObj.task_dep_to_show = Common.CheckStringNull(row["task_dep_to_show"]);
                        RetObj.doc_url = Common.CheckStringNull(row["task_url"]);
                        RetObj.rejected_remark = Common.CheckStringNull(row["rejected_remark"]);

                        RetObj.task_to_to_show = Common.CheckStringNull(row["task_to_to_show"]);



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
                RObj.TaskDataList = RetLst;
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

        ///////////////// GET GIVEN TASKS DAL //////////////////////////

        public TaskReturn GetPOCTasks(TaskModel getModel) // get modal from user in getModel
        {
            TaskReturn RObj = new TaskReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 16 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";
                var strExpectedDate = "";
                var strClosedDate = "";
                var strAssignDate = "";
                var strCompeleteDate = "";

                // for single object declaration
                var RetObj = new TaskModel();
                // for list object declaration
                List<TaskModel> RetLst = new List<TaskModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);

                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.task_id = Common.CheckIntegerNull(row["task_id"]);
                        RetObj.ticket_no = Common.CheckIntegerNull(row["ticket_no"]);
                        RetObj.from_com = Common.CheckStringNull(row["from_com"]);
                        RetObj.from_dep = Common.CheckStringNull(row["from_dep"]);
                        RetObj.task_from = Common.CheckStringNull(row["task_from"]);
                        RetObj.expected_date = Common.CheckDateTimeNull(row["expected_date"]);
                        if (!string.IsNullOrEmpty(row["expected_date"].ToString()) && row["expected_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["expected_date"]);
                            strExpectedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strExpectedDate = "00-000-0000";
                        }
                        RetObj.str_expected_date = strExpectedDate;
                        RetObj.closed_date = Common.CheckDateTimeNull(row["closed_date"]);
                        if (!string.IsNullOrEmpty(row["closed_date"].ToString()) && row["closed_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["closed_date"]);
                            strClosedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strClosedDate = "00-000-0000";
                        }
                        RetObj.str_closed_date = strClosedDate;
                        RetObj.assign_from = Common.CheckStringNull(row["assign_from"]);
                        RetObj.assign_from_to_show = Common.CheckStringNull(row["assign_from_to_show"]);
                        RetObj.assign_date = Common.CheckDateTimeNull(row["assign_date"]);
                        if (!string.IsNullOrEmpty(row["assign_date"].ToString()) && row["assign_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["assign_date"]);
                            strAssignDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strAssignDate = "00-000-0000";
                        }
                        RetObj.str_assign_date = strAssignDate;
                        RetObj.to_com = Common.CheckStringNull(row["to_com"]);
                        RetObj.to_dep = Common.CheckStringNull(row["to_dep"]);
                        RetObj.task_to = Common.CheckStringNull(row["task_to"]);
                        RetObj.compelete_date = Common.CheckDateTimeNull(row["compelete_date"]);
                        if (!string.IsNullOrEmpty(row["compelete_date"].ToString()) && row["compelete_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["compelete_date"]);
                            strCompeleteDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCompeleteDate = "00-000-0000";
                        }
                        RetObj.str_compelete_date = strCompeleteDate;
                        RetObj.task_status = Common.CheckStringNull(row["task_status"]);
                        RetObj.task_img = Common.CheckStringNull(row["task_img"]);
                        RetObj.task_doc = Common.CheckStringNull(row["task_doc"]);
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
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.task_title = Common.CheckStringNull(row["task_title"]);
                        RetObj.task_desc = Common.CheckStringNull(row["task_desc"]);
                        RetObj.mob_no = Common.CheckStringNull(row["mob_no"]);
                        RetObj.task_priority = Common.CheckStringNull(row["task_priority"]);
                        RetObj.from_com_to_show = Common.CheckStringNull(row["from_com_to_show"]);
                        RetObj.task_list_id = Common.CheckIntegerNull(row["task_list_id"]);
                        RetObj.task_list_id_to_show = Common.CheckStringNull(row["task_list_id_to_show"]);
                        RetObj.task_dep_to_show = Common.CheckStringNull(row["task_dep_to_show"]);
                        RetObj.doc_url = Common.CheckStringNull(row["task_url"]);
                        RetObj.rejected_remark = Common.CheckStringNull(row["rejected_remark"]);
                        RetObj.task_to_to_show = Common.CheckStringNull(row["task_to_to_show"]);


                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                RObj.poc_total_task = 1;
                RObj.poc_in_prog_task = 1;
                RObj.poc_complete_task = 1;

                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.TaskDataList = RetLst;
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

        ///////////////// GET Recieved TASKS DAL //////////////////////////

        public TaskReturn GetRecievedTasks(TaskModel getModel) // get modal from user in getModel
        {
            TaskReturn RObj = new TaskReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 8 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";
                var strExpectedDate = "";
                var strClosedDate = "";
                var strAssignDate = "";
                var strCompeleteDate = "";

                // for single object declaration
                var RetObj = new TaskModel();
                // for list object declaration
                List<TaskModel> RetLst = new List<TaskModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.task_id = Common.CheckIntegerNull(row["task_id"]);
                        RetObj.ticket_no = Common.CheckIntegerNull(row["ticket_no"]);
                        RetObj.from_com = Common.CheckStringNull(row["from_com"]);
                        RetObj.from_dep = Common.CheckStringNull(row["from_dep"]);
                        RetObj.task_from = Common.CheckStringNull(row["task_from"]);
                        RetObj.expected_date = Common.CheckDateTimeNull(row["expected_date"]);
                        if (!string.IsNullOrEmpty(row["expected_date"].ToString()) && row["expected_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["expected_date"]);
                            strExpectedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strExpectedDate = "00-000-0000";
                        }
                        RetObj.str_expected_date = strExpectedDate;
                        RetObj.closed_date = Common.CheckDateTimeNull(row["closed_date"]);
                        if (!string.IsNullOrEmpty(row["closed_date"].ToString()) && row["closed_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["closed_date"]);
                            strClosedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strClosedDate = "00-000-0000";
                        }
                        RetObj.str_closed_date = strClosedDate;
                        RetObj.assign_from = Common.CheckStringNull(row["assign_from"]);
                        RetObj.assign_from_to_show = Common.CheckStringNull(row["assign_from_to_show"]);

                        RetObj.assign_date = Common.CheckDateTimeNull(row["assign_date"]);
                        if (!string.IsNullOrEmpty(row["assign_date"].ToString()) && row["assign_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["assign_date"]);
                            strAssignDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strAssignDate = "00-000-0000";
                        }
                        RetObj.str_assign_date = strAssignDate;
                        RetObj.to_com = Common.CheckStringNull(row["to_com"]);
                        RetObj.to_dep = Common.CheckStringNull(row["to_dep"]);
                        RetObj.task_to = Common.CheckStringNull(row["task_to"]);
                        RetObj.compelete_date = Common.CheckDateTimeNull(row["compelete_date"]);
                        if (!string.IsNullOrEmpty(row["compelete_date"].ToString()) && row["compelete_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["compelete_date"]);
                            strCompeleteDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCompeleteDate = "00-000-0000";
                        }
                        RetObj.str_compelete_date = strCompeleteDate;
                        RetObj.task_status = Common.CheckStringNull(row["task_status"]);
                        RetObj.task_img = Common.CheckStringNull(row["task_img"]);
                        RetObj.task_doc = Common.CheckStringNull(row["task_doc"]);
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
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.task_title = Common.CheckStringNull(row["task_title"]);
                        RetObj.task_desc = Common.CheckStringNull(row["task_desc"]);
                        RetObj.mob_no = Common.CheckStringNull(row["mob_no"]);
                        RetObj.task_priority = Common.CheckStringNull(row["task_priority"]);
                        RetObj.from_com_to_show = Common.CheckStringNull(row["from_com_to_show"]);
                        RetObj.task_list_id = Common.CheckIntegerNull(row["task_list_id"]);
                        RetObj.task_list_id_to_show = Common.CheckStringNull(row["task_list_id_to_show"]);
                        RetObj.task_dep_to_show = Common.CheckStringNull(row["task_dep_to_show"]);
                        RetObj.doc_url = Common.CheckStringNull(row["task_url"]);
                        RetObj.rejected_remark = Common.CheckStringNull(row["rejected_remark"]);
                        RetObj.task_to_to_show = Common.CheckStringNull(row["task_to_to_show"]);




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
                RObj.TaskDataList = RetLst;
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


        ///////////////// add UPDATE task IN LIST //////////////////////////
        public TaskReturn AUDTasks(List<TaskModel> getModels)
        {
            TaskReturn RObj = new TaskReturn(); // For result body object declaration
            int retstatus = 50;
            int taskId = 0;
            string action = "";
            string retmessage = "No Data Found!";

            try
            {
                foreach (var getModel in getModels) // Iterate over each model
                {
                    action = getModel.action;
                    // Parameters array for the stored procedure
                    var parm = new SqlParameter[33];
                    parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                    parm[1] = new SqlParameter("action", SqlDbType.VarChar) { Value = getModel.action };
                    parm[2] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                    parm[3] = new SqlParameter("task_id", SqlDbType.Int) { Value = getModel.task_id };
                    parm[4] = new SqlParameter("ticket_no", SqlDbType.Int) { Value = getModel.ticket_no };
                    parm[5] = new SqlParameter("from_com", SqlDbType.VarChar) { Value = getModel.from_com };
                    parm[6] = new SqlParameter("from_dep", SqlDbType.VarChar) { Value = getModel.from_dep };
                    parm[7] = new SqlParameter("task_from", SqlDbType.VarChar) { Value = getModel.task_from };
                    parm[8] = new SqlParameter("expected_date", SqlDbType.DateTime) { Value = getModel.expected_date };
                    parm[9] = new SqlParameter("closed_date", SqlDbType.DateTime) { Value = getModel.closed_date };
                    parm[10] = new SqlParameter("assign_from", SqlDbType.VarChar) { Value = getModel.assign_from };
                    parm[11] = new SqlParameter("assign_date", SqlDbType.DateTime) { Value = getModel.assign_date };
                    parm[12] = new SqlParameter("to_com", SqlDbType.VarChar) { Value = getModel.to_com };
                    parm[13] = new SqlParameter("to_dep", SqlDbType.VarChar) { Value = getModel.to_dep };
                    parm[14] = new SqlParameter("task_to", SqlDbType.VarChar) { Value = getModel.task_to };
                    parm[15] = new SqlParameter("compelete_date", SqlDbType.DateTime) { Value = getModel.compelete_date };
                    parm[16] = new SqlParameter("task_status", SqlDbType.VarChar) { Value = getModel.task_status };
                    parm[17] = new SqlParameter("task_img", SqlDbType.VarChar) { Value = getModel.task_img };
                    parm[18] = new SqlParameter("task_doc", SqlDbType.VarChar) { Value = getModel.task_doc };
                    parm[19] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                    parm[20] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date };
                    parm[21] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
                    parm[22] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date };
                    parm[23] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                    parm[24] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                    parm[25] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                    parm[26] = new SqlParameter("task_title", SqlDbType.VarChar) { Value = getModel.task_title };
                    parm[27] = new SqlParameter("task_desc", SqlDbType.VarChar) { Value = getModel.task_desc };
                    parm[28] = new SqlParameter("mob_no", SqlDbType.VarChar) { Value = getModel.mob_no };
                    parm[29] = new SqlParameter("task_priority", SqlDbType.VarChar) { Value = getModel.task_priority };
                    parm[30] = new SqlParameter("task_list_id", SqlDbType.Int) { Value = getModel.task_list_id };
                    parm[31] = new SqlParameter("extension", SqlDbType.VarChar) { Value = getModel.extension };
                    parm[32] = new SqlParameter("rejected_remark", SqlDbType.VarChar) { Value = getModel.rejected_remark };

                    //parm[30] = new SqlParameter("Task_Files", SqlDbType.VarChar) { Value = getModel.Task_Files?.FileName ?? string.Empty };

                    // Execute the stored procedure
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                    foreach (DataRow row in dsuser.Rows)
                    {
                        retstatus = Common.CheckIntegerNull(row["res_status"]);
                        retmessage = Common.CheckStringNull(row["res_message"]);

                        if (retstatus == 2 && action == "CREATE")
                        {
                            taskId = Common.CheckIntegerNull(row["task_id"]);

                            // Define the path where the image will be saved
                            string folderPath = HttpContext.Current.Server.MapPath("~/TaskDocument/");
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            // Generate a unique file name
                            string fileName = taskId.ToString() + "." + getModel.extension;
                            string taskIdString = taskId.ToString();
                            string fullPath = Path.Combine(folderPath, fileName);
                            string[] existingFiles = Directory.GetFiles(folderPath, taskIdString + ".*");

                            // Check if file already exists, and delete it if so

                            foreach (string file in existingFiles)
                            {
                                File.Delete(file);  // Delete the existing file
                            }

                            if (!string.IsNullOrEmpty(getModel.extension))
                            {
                                // Convert the Base64 string to a byte array
                                byte[] imageBytes = Convert.FromBase64String(getModel.task_doc);

                                // Save the byte array to the specified path as a file
                                File.WriteAllBytes(fullPath, imageBytes);
                            }
                        }

                    }
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


        ///////////////// add UPDATE task IN LIST //////////////////////////
        public TaskReturn AUDSingleTask(TaskModel getModel)
        {
            TaskReturn RObj = new TaskReturn(); // For result body object declaration
            int retstatus = 50;
            
            string action = "";
            string retmessage = "No Data Found!";

            try
            {

                action = getModel.action;
                // Parameters array for the stored procedure
                var parm = new SqlParameter[32];
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("action", SqlDbType.VarChar) { Value = getModel.action };
                parm[2] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                parm[3] = new SqlParameter("task_id", SqlDbType.Int) { Value = getModel.task_id };
                parm[4] = new SqlParameter("ticket_no", SqlDbType.Int) { Value = getModel.ticket_no };
                parm[5] = new SqlParameter("from_com", SqlDbType.VarChar) { Value = getModel.from_com };
                parm[6] = new SqlParameter("from_dep", SqlDbType.VarChar) { Value = getModel.from_dep };
                parm[7] = new SqlParameter("task_from", SqlDbType.VarChar) { Value = getModel.task_from };
                parm[8] = new SqlParameter("expected_date", SqlDbType.DateTime) { Value = getModel.expected_date };
                parm[9] = new SqlParameter("closed_date", SqlDbType.DateTime) { Value = getModel.closed_date };
                parm[10] = new SqlParameter("assign_from", SqlDbType.VarChar) { Value = getModel.assign_from };
                parm[11] = new SqlParameter("assign_date", SqlDbType.DateTime) { Value = getModel.assign_date };
                parm[12] = new SqlParameter("to_com", SqlDbType.VarChar) { Value = getModel.to_com };
                parm[13] = new SqlParameter("to_dep", SqlDbType.VarChar) { Value = getModel.to_dep };
                parm[14] = new SqlParameter("task_to", SqlDbType.VarChar) { Value = getModel.task_to };
                parm[15] = new SqlParameter("compelete_date", SqlDbType.DateTime) { Value = getModel.compelete_date };
                parm[16] = new SqlParameter("task_status", SqlDbType.VarChar) { Value = getModel.task_status };
                parm[17] = new SqlParameter("task_img", SqlDbType.VarChar) { Value = getModel.task_img };
                parm[18] = new SqlParameter("task_doc", SqlDbType.VarChar) { Value = getModel.task_doc };
                parm[19] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                parm[20] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date };
                parm[21] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
                parm[22] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date };
                parm[23] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[24] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                parm[25] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[26] = new SqlParameter("task_title", SqlDbType.VarChar) { Value = getModel.task_title };
                parm[27] = new SqlParameter("task_desc", SqlDbType.VarChar) { Value = getModel.task_desc };
                parm[28] = new SqlParameter("mob_no", SqlDbType.VarChar) { Value = getModel.mob_no };
                parm[29] = new SqlParameter("task_priority", SqlDbType.VarChar) { Value = getModel.task_priority };
                parm[30] = new SqlParameter("task_list_id", SqlDbType.Int) { Value = getModel.task_list_id };
                parm[31] = new SqlParameter("extension", SqlDbType.VarChar) { Value = getModel.extension };

                //parm[30] = new SqlParameter("Task_Files", SqlDbType.VarChar) { Value = getModel.Task_Files?.FileName ?? string.Empty };

                // Execute the stored procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
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


        private void SaveFileExtensionToDatabase(string taskId, string fileExtension)
        {
            using (var connection = new SqlConnection("YourConnectionString"))
            {
                string query = "UPDATE Task_Files SET FileExtension = @FileExtension WHERE task_id = @task_id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FileExtension", fileExtension);
                command.Parameters.AddWithValue("@task_id", taskId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public TaskReturn GetTasksCount(TaskModel getModel) // get modal from user in getModel
        {
            TaskReturn RObj = new TaskReturn(); // for result body object declaration
            int retstatus; string retmessage; int totalTasks; int inProgTasks; int completeTasks;
            int gave_totalTasks; int gave_inProgTasks; int gave_completeTasks;
            int poc_totalTasks; int poc_inProgTasks; int poc_completeTasks;
            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                totalTasks = 0;
                inProgTasks = 0;
                completeTasks = 0;
                gave_totalTasks = 0;
                gave_inProgTasks = 0;
                gave_completeTasks = 0;
                poc_totalTasks = 0;
                poc_inProgTasks = 0;
                poc_completeTasks = 0;

                // for single object declaration
                var RetObj = new TaskModel();

                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        totalTasks = Common.CheckIntegerNull(row["total_task"]);
                        inProgTasks = Common.CheckIntegerNull(row["in_progress_task"]);
                        completeTasks = Common.CheckIntegerNull(row["complete_task"]);
                        gave_totalTasks = Common.CheckIntegerNull(row["gave_total_task"]);
                        gave_inProgTasks = Common.CheckIntegerNull(row["gave_in_progress_task"]);
                        gave_completeTasks = Common.CheckIntegerNull(row["gave_complete_task"]);
                        poc_totalTasks = Common.CheckIntegerNull(row["poc_total_task"]);
                        poc_inProgTasks = Common.CheckIntegerNull(row["poc_in_progress_task"]);
                        poc_completeTasks = Common.CheckIntegerNull(row["poc_complete_task"]);
                    }



                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                RObj.total_task = totalTasks;
                RObj.in_prog_task = inProgTasks;
                RObj.complete_task = completeTasks;
                RObj.gave_total_task = gave_totalTasks;
                RObj.gave_in_prog_task = gave_inProgTasks;
                RObj.gave_complete_task = gave_completeTasks;
                RObj.poc_total_task = poc_totalTasks;
                RObj.poc_in_prog_task = poc_inProgTasks;
                RObj.poc_complete_task = poc_completeTasks;

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


        ///////////////// GET ALL task cheat DAL //////////////////////////

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


        ///////////////// aud chats //////////////////////////
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

        public TaskReturn TaskStatusUpdater(TaskModel getModel)
        {
            TaskReturn RObj = new TaskReturn(); // For result body object declaration
            int retstatus = 50;
            string retmessage = "No Data Found!";

            try
            {

                // Parameters array for the stored procedure
                var parm = new SqlParameter[5];
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("task_id", SqlDbType.Int) { Value = getModel.task_id };
                parm[2] = new SqlParameter("task_status", SqlDbType.VarChar) { Value = getModel.task_status };
                parm[3] = new SqlParameter("rejected_remark", SqlDbType.VarChar) { Value = getModel.rejected_remark };
                parm[4] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                // Execute the stored procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
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

        public TaskTypeAndListsReturn AUDSingleTaskType(TaskTypeAndLists getModel)
        {
            TaskTypeAndListsReturn RObj = new TaskTypeAndListsReturn(); // For result body object declaration
            int retstatus = 50;
           
            string retmessage = "No Data Found!";

            try
            {

               
                // Parameters array for the stored procedure
                var parm = new SqlParameter[11];
                parm[0] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                parm[1] = new SqlParameter("task_type_id", SqlDbType.Int) { Value = getModel.task_type_id };
                parm[2] = new SqlParameter("task_type_name", SqlDbType.VarChar) { Value = getModel.task_type_name };
                parm[3] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                parm[4] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date };
                parm[5] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
                parm[6] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date };
                parm[7] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[8] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                parm[9] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[10] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };


                //parm[30] = new SqlParameter("Task_Files", SqlDbType.VarChar) { Value = getModel.Task_Files?.FileName ?? string.Empty };

                // Execute the stored procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
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


        ///////////////// GET ALL Task Types //////////////////////////

        public TaskTypeAndListsReturn GetAllTaskTypes(TaskTypeAndLists getModel) // get modal from user in getModel
        {
            TaskTypeAndListsReturn RObj = new TaskTypeAndListsReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 10 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";


                // for single object declaration
                var RetObj = new TaskTypeAndLists();
                // for list object declaration
                List<TaskTypeAndLists> RetLst = new List<TaskTypeAndLists>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskTypeAndLists(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.task_type_id = Common.CheckIntegerNull(row["task_type_id"]);
                        RetObj.task_type_name = Common.CheckStringNull(row["task_type_name"]);
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
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);

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
                RObj.TaskTypeAndListsDataList = RetLst;
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


        public TaskTypeAndListsReturn AUDSingleTaskSubType(TaskTypeAndLists getModel)
        {
            TaskTypeAndListsReturn RObj = new TaskTypeAndListsReturn(); // For result body object declaration
            int retstatus = 50;

            string retmessage = "No Data Found!";

            try
            {


                // Parameters array for the stored procedure
                var parm = new SqlParameter[12];
                parm[0] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                parm[1] = new SqlParameter("task_type_id", SqlDbType.Int) { Value = getModel.task_type_id };
                parm[2] = new SqlParameter("task_list_id", SqlDbType.Int) { Value = getModel.task_list_id };
                parm[3] = new SqlParameter("task_list_name", SqlDbType.VarChar) { Value = getModel.task_list_name };
                parm[4] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                parm[5] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date };
                parm[6] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
                parm[7] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date };
                parm[8] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[9] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                parm[10] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[11] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };



                //parm[30] = new SqlParameter("Task_Files", SqlDbType.VarChar) { Value = getModel.Task_Files?.FileName ?? string.Empty };

                // Execute the stored procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
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

        ///////////////// GET ALL Task list //////////////////////////

        public TaskTypeAndListsReturn GetAllTaskList(TaskTypeAndLists getModel) // get modal from user in getModel
        {
            TaskTypeAndListsReturn RObj = new TaskTypeAndListsReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[5];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 13 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[4] = new SqlParameter("@task_type_id", SqlDbType.Int) { Value = getModel.task_type_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Task_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";


                // for single object declaration
                var RetObj = new TaskTypeAndLists();
                // for list object declaration
                List<TaskTypeAndLists> RetLst = new List<TaskTypeAndLists>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new TaskTypeAndLists(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping


                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.task_type_id = Common.CheckIntegerNull(row["task_type_id"]);
                        RetObj.task_type_name = Common.CheckStringNull(row["task_type_name"]);
                        RetObj.task_list_id = Common.CheckIntegerNull(row["task_list_id"]);
                        RetObj.task_list_name = Common.CheckStringNull(row["task_list_name"]);
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
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);

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
                RObj.TaskTypeAndListsDataList = RetLst;
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


        #endregion
    }
}