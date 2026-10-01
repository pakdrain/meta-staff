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

namespace HrmApi.DAL
{
    public class RoleDAL
    {
        private string _ConnString; //variable for connection string
        public RoleDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region  Roles

        public RoleReturn GetAllRoles(RoleModel getModel) // get modal from user in getModel
        {
            RoleReturn RObj = new RoleReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 1 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_roles_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";

                var strCreatedDate = "";
                var strUpdateDate = "";


                // for single object declaration
                var RetObj = new RoleModel();
                // for list object declaration
                List<RoleModel> RetLst = new List<RoleModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new RoleModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.role_id = Common.CheckIntegerNull(row["role_id"]);
                        RetObj.role_name = Common.CheckStringNull(row["role_name"]);
                        RetObj.role_description = Common.CheckStringNull(row["role_description"]);
                        RetObj.att_view = Common.CheckIntegerNull(row["att_view"]);
                        RetObj.att_add = Common.CheckIntegerNull(row["att_add"]);
                        RetObj.att_edit = Common.CheckIntegerNull(row["att_edit"]);
                        RetObj.att_del = Common.CheckIntegerNull(row["att_del"]);
                        RetObj.task_view = Common.CheckIntegerNull(row["task_view"]);
                        RetObj.task_add = Common.CheckIntegerNull(row["task_add"]);
                        RetObj.task_edit = Common.CheckIntegerNull(row["task_edit"]);
                        RetObj.task_del = Common.CheckIntegerNull(row["task_del"]);
                        RetObj.task_assignee = Common.CheckIntegerNull(row["task_assignee"]);
                        RetObj.leave_view = Common.CheckIntegerNull(row["leave_view"]);
                        RetObj.leave_add = Common.CheckIntegerNull(row["leave_add"]);
                        RetObj.leave_edit = Common.CheckIntegerNull(row["leave_edit"]);
                        RetObj.leave_del = Common.CheckIntegerNull(row["leave_del"]);
                        RetObj.leave_app = Common.CheckIntegerNull(row["leave_app"]);
                        RetObj.leave_rej = Common.CheckIntegerNull(row["leave_rej"]);
                        RetObj.order_view = Common.CheckIntegerNull(row["order_view"]);
                        RetObj.order_add = Common.CheckIntegerNull(row["order_add"]);
                        RetObj.order_edit = Common.CheckIntegerNull(row["order_edit"]);
                        RetObj.order_del = Common.CheckIntegerNull(row["order_del"]);
                        RetObj.order_app = Common.CheckIntegerNull(row["order_app"]);
                        RetObj.order_rej = Common.CheckIntegerNull(row["order_rej"]);
                        RetObj.cmpln_view = Common.CheckIntegerNull(row["cmpln_view"]);
                        RetObj.cmpln_add = Common.CheckIntegerNull(row["cmpln_add"]);
                        RetObj.cmpln_edit = Common.CheckIntegerNull(row["cmpln_edit"]);
                        RetObj.cmpln_del = Common.CheckIntegerNull(row["cmpln_del"]);
                        RetObj.cmpln_action = Common.CheckIntegerNull(row["cmpln_action"]);
                        RetObj.loan_view = Common.CheckIntegerNull(row["loan_view"]);
                        RetObj.loan_add = Common.CheckIntegerNull(row["loan_add"]);
                        RetObj.loan_edit = Common.CheckIntegerNull(row["loan_edit"]);
                        RetObj.loan_del = Common.CheckIntegerNull(row["loan_del"]);
                        RetObj.loan_app = Common.CheckIntegerNull(row["loan_app"]);
                        RetObj.loan_reject = Common.CheckIntegerNull(row["loan_reject"]);
                        RetObj.loan_app_hod_show = Common.CheckIntegerNull(row["loan_app_hod_show"]);
                        RetObj.loan_app_gm_show = Common.CheckIntegerNull(row["loan_app_gm_show"]);
                        RetObj.loan_app_hr_show = Common.CheckIntegerNull(row["loan_app_hr_show"]);
                        RetObj.loan_app_ed_show = Common.CheckIntegerNull(row["loan_app_ed_show"]);
                        RetObj.loan_reject_show = Common.CheckIntegerNull(row["loan_reject_show"]);
                        RetObj.notif_view = Common.CheckIntegerNull(row["notif_view"]);
                        RetObj.notif_add = Common.CheckIntegerNull(row["notif_add"]);
                        RetObj.notif_edit = Common.CheckIntegerNull(row["notif_edit"]);
                        RetObj.notif_del = Common.CheckIntegerNull(row["notif_del"]);
                        RetObj.resig_view = Common.CheckIntegerNull(row["resig_view"]);
                        RetObj.resig_add = Common.CheckIntegerNull(row["resig_add"]);
                        RetObj.resig_edit = Common.CheckIntegerNull(row["resig_edit"]);
                        RetObj.resig_del = Common.CheckIntegerNull(row["resig_del"]);
                        RetObj.resig_app = Common.CheckIntegerNull(row["resig_app"]);
                        RetObj.resig_reject = Common.CheckIntegerNull(row["resig_reject"]);
                        RetObj.user_view = Common.CheckIntegerNull(row["user_view"]);
                        RetObj.user_add = Common.CheckIntegerNull(row["user_add"]);
                        RetObj.user_edit = Common.CheckIntegerNull(row["user_edit"]);
                        RetObj.user_del = Common.CheckIntegerNull(row["user_del"]);
                        RetObj.role_view = Common.CheckIntegerNull(row["role_view"]);
                        RetObj.role_add = Common.CheckIntegerNull(row["role_add"]);
                        RetObj.role_edit = Common.CheckIntegerNull(row["role_edit"]);
                        RetObj.role_del = Common.CheckIntegerNull(row["role_del"]);
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
                            strUpdateDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdateDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdateDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.vehic_view = Common.CheckIntegerNull(row["vehic_view"]);
                        RetObj.vehic_add = Common.CheckIntegerNull(row["vehic_add"]);
                        RetObj.vehic_edit = Common.CheckIntegerNull(row["vehic_edit"]);
                        RetObj.vehic_del = Common.CheckIntegerNull(row["vehic_del"]);
                        RetObj.mark_attendance = Common.CheckIntegerNull(row["mark_attendance"]);
                        RetObj.dash_att_rep = Common.CheckIntegerNull(row["dash_att_rep"]);
                        RetObj.dash_att_count = Common.CheckIntegerNull(row["dash_att_count"]);
                        RetObj.dash_leave_bal = Common.CheckIntegerNull(row["dash_leave_bal"]);
                        RetObj.leave_menu = Common.CheckIntegerNull(row["leave_menu"]);
                        RetObj.leave_my_req_sm = Common.CheckIntegerNull(row["leave_my_req_sm"]);
                        RetObj.leave_hod_app_sm = Common.CheckIntegerNull(row["leave_hod_app_sm"]);
                        RetObj.leave_hr_app_sm = Common.CheckIntegerNull(row["leave_hr_app_sm"]);
                        RetObj.leave_hod_app_btn = Common.CheckIntegerNull(row["leave_hod_app_btn"]);
                        RetObj.o_d_menu = Common.CheckIntegerNull(row["o_d_menu"]);
                        RetObj.o_d_my_req_sm = Common.CheckIntegerNull(row["o_d_my_req_sm"]);
                        RetObj.o_d_hod_app_sm = Common.CheckIntegerNull(row["o_d_hod_app_sm"]);
                        RetObj.o_d_hr_app_sm = Common.CheckIntegerNull(row["o_d_hr_app_sm"]);
                        RetObj.o_d_hod_app_btn = Common.CheckIntegerNull(row["o_d_hod_app_btn"]);
                        RetObj.task_menu = Common.CheckIntegerNull(row["task_menu"]);
                        RetObj.task_assigned_sm = Common.CheckIntegerNull(row["task_assigned_sm"]);
                        RetObj.task_rej = Common.CheckIntegerNull(row["task_rej"]);
                        RetObj.task_poc_app = Common.CheckIntegerNull(row["task_poc_app"]);
                        RetObj.task_compelete = Common.CheckIntegerNull(row["task_compelete"]);
                        RetObj.order_menu = Common.CheckIntegerNull(row["order_menu"]);
                        RetObj.o_d_view = Common.CheckIntegerNull(row["o_d_view"]);
                        RetObj.o_d_add = Common.CheckIntegerNull(row["o_d_add"]);
                        RetObj.o_d_edit = Common.CheckIntegerNull(row["o_d_edit"]);
                        RetObj.o_d_del = Common.CheckIntegerNull(row["o_d_del"]);
                        RetObj.o_d_app = Common.CheckIntegerNull(row["o_d_app"]);
                        RetObj.clear_menu = Common.CheckIntegerNull(row["clear_menu"]);
                        RetObj.clear_hr_app_sm = Common.CheckIntegerNull(row["clear_hr_app_sm"]);
                        RetObj.clear_hod_app_sm = Common.CheckIntegerNull(row["clear_hod_app_sm"]);
                        RetObj.clear_dep_app_sm = Common.CheckIntegerNull(row["clear_dep_app_sm"]);
                        RetObj.clear_add = Common.CheckIntegerNull(row["clear_add"]);
                        RetObj.clear_edit = Common.CheckIntegerNull(row["clear_edit"]);
                        RetObj.clear_del = Common.CheckIntegerNull(row["clear_del"]);
                        RetObj.clear_hod_app = Common.CheckIntegerNull(row["clear_hod_app"]);
                        RetObj.clear_dep_app = Common.CheckIntegerNull(row["clear_dep_app"]);
                        RetObj.clear_app = Common.CheckIntegerNull(row["clear_app"]);
                        RetObj.o_d_rej = Common.CheckIntegerNull(row["o_d_rej"]);
                        RetObj.dash_menu = Common.CheckIntegerNull(row["dash_menu"]);
                        RetObj.compln_menu = Common.CheckIntegerNull(row["compln_menu"]);
                        RetObj.compln_my_sm = Common.CheckIntegerNull(row["compln_my_sm"]);
                        RetObj.compln_hr_sm = Common.CheckIntegerNull(row["compln_hr_sm"]);
                        RetObj.compln_add = Common.CheckIntegerNull(row["compln_add"]);
                        RetObj.compln_del = Common.CheckIntegerNull(row["compln_del"]);
                        RetObj.compln_edit = Common.CheckIntegerNull(row["compln_edit"]);
                        RetObj.compln_app = Common.CheckIntegerNull(row["compln_app"]);
                        RetObj.compln_rej = Common.CheckIntegerNull(row["compln_rej"]);
                        RetObj.hr_corsp_menu = Common.CheckIntegerNull(row["hr_corsp_menu"]);
                        RetObj.notification_menu = Common.CheckIntegerNull(row["notification_menu"]);
                        RetObj.wpp_menu = Common.CheckIntegerNull(row["wpp_menu"]);
                        RetObj.user_pswd_update = Common.CheckIntegerNull(row["user_pswd_update"]);
                        RetObj.allowed_company = Common.CheckIntegerNull(row["allowed_company"]);
                        RetObj.allowed_department = Common.CheckIntegerNull(row["allowed_department"]);
                        RetObj.setup_menu = Common.CheckIntegerNull(row["setup_menu"]);
                        RetObj.setup_sm_task_type = Common.CheckIntegerNull(row["setup_sm_task_type"]);
                        RetObj.setup_sm_task_sub_type = Common.CheckIntegerNull(row["setup_sm_task_sub_type"]);
                        RetObj.logs = Common.CheckIntegerNull(row["logs"]);
                        RetObj.master_acc = Common.CheckIntegerNull(row["master_acc"]);
                        RetObj.office_add = Common.CheckIntegerNull(row["office_add"]);
                        RetObj.office_del = Common.CheckIntegerNull(row["office_del"]);
                        RetObj.office_edit = Common.CheckIntegerNull(row["office_edit"]);
                        RetObj.office_menu = Common.CheckIntegerNull(row["office_menu"]);
                        RetObj.map_office = Common.CheckIntegerNull(row["map_office"]);
                        RetObj.vehic_hod_sub_menu = Common.CheckIntegerNull(row["vehic_hod_sub_menu"]);
                        RetObj.vehic_gm_sub_menu = Common.CheckIntegerNull(row["vehic_gm_sub_menu"]);
                        RetObj.vehic_hr_sub_menu = Common.CheckIntegerNull(row["vehic_hr_sub_menu"]);
                        RetObj.vehic_admin_sub_menu = Common.CheckIntegerNull(row["vehic_admin_sub_menu"]);
                        RetObj.vehic_ed_sub_menu = Common.CheckIntegerNull(row["vehic_ed_sub_menu"]);
                        RetObj.hc_menu = Common.CheckIntegerNull(row["hc_menu"]);
                        RetObj.hc_invid_sub_menu = Common.CheckIntegerNull(row["hc_invid_sub_menu"]);
                        RetObj.hc_dep_sub_menu = Common.CheckIntegerNull(row["hc_dep_sub_menu"]);
                        RetObj.hc_notice_sub_menu = Common.CheckIntegerNull(row["hc_notice_sub_menu"]);
                        RetObj.sal_menu = Common.CheckIntegerNull(row["sal_menu"]);
                        RetObj.my_sub_menu = Common.CheckIntegerNull(row["my_sub_menu"]);
                        RetObj.my_hod_menu = Common.CheckIntegerNull(row["my_hod_menu"]);
                        RetObj.my_hr_menu = Common.CheckIntegerNull(row["my_hr_menu"]);
                        RetObj.wwp_create_sub_menu = Common.CheckIntegerNull(row["wwp_create_sub_menu"]);
                        RetObj.wwp_inbox_sub_menu = Common.CheckIntegerNull(row["wwp_inbox_sub_menu"]);
                        RetObj.leave_apply_sm = Common.CheckIntegerNull(row["leave_apply_sm"]);
                        RetObj.od_apply_sm = Common.CheckIntegerNull(row["od_apply_sm"]);
                        RetObj.loan_apply_sm = Common.CheckIntegerNull(row["loan_apply_sm"]);
                        RetObj.vehicle_apply_sm = Common.CheckIntegerNull(row["vehicle_apply_sm"]);
                        RetObj.task_hod = Common.CheckIntegerNull(row["task_hod"]);
                        RetObj.task_ed = Common.CheckIntegerNull(row["task_ed"]);
                        RetObj.prog_tracking = Common.CheckIntegerNull(row["prog_tracking"]);
                        RetObj.prog_track_report = Common.CheckIntegerNull(row["prog_track_report"]);
                        RetObj.projects = Common.CheckIntegerNull(row["projects"]);
                        RetObj.prog_menu = Common.CheckIntegerNull(row["prog_menu"]);
                        RetObj.prog_status = Common.CheckIntegerNull(row["prog_status"]);
                        RetObj.sync_emp = Common.CheckIntegerNull(row["sync_emp"]);
                        RetObj.add_job = Common.CheckIntegerNull(row["add_job"]);
                        RetObj.add_location = Common.CheckIntegerNull(row["add_location"]);
                        RetObj.bonus_ded = Common.CheckIntegerNull(row["bonus_ded"]);
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
                RObj.RoleDataList = RetLst;
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
        public RoleReturn GetSingleRole(RoleModel getModel) // get modal from user in getModel
        {
            RoleReturn RObj = new RoleReturn(); // for result body object declaration
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
                parm[4] = new SqlParameter("@role_id", SqlDbType.Int) { Value = getModel.role_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_roles_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";

                var strCreatedDate = "";
                var strUpdateDate = "";


                // for single object declaration
                var RetObj = new RoleModel();
                // for list object declaration
                List<RoleModel> RetLst = new List<RoleModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new RoleModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.role_id = Common.CheckIntegerNull(row["role_id"]);
                        RetObj.role_name = Common.CheckStringNull(row["role_name"]);
                        RetObj.role_description = Common.CheckStringNull(row["role_description"]);
                        RetObj.att_view = Common.CheckIntegerNull(row["att_view"]);
                        RetObj.att_add = Common.CheckIntegerNull(row["att_add"]);
                        RetObj.att_edit = Common.CheckIntegerNull(row["att_edit"]);
                        RetObj.att_del = Common.CheckIntegerNull(row["att_del"]);
                        RetObj.task_view = Common.CheckIntegerNull(row["task_view"]);
                        RetObj.task_add = Common.CheckIntegerNull(row["task_add"]);
                        RetObj.task_edit = Common.CheckIntegerNull(row["task_edit"]);
                        RetObj.task_del = Common.CheckIntegerNull(row["task_del"]);
                        RetObj.task_assignee = Common.CheckIntegerNull(row["task_assignee"]);
                        RetObj.leave_view = Common.CheckIntegerNull(row["leave_view"]);
                        RetObj.leave_add = Common.CheckIntegerNull(row["leave_add"]);
                        RetObj.leave_edit = Common.CheckIntegerNull(row["leave_edit"]);
                        RetObj.leave_del = Common.CheckIntegerNull(row["leave_del"]);
                        RetObj.leave_app = Common.CheckIntegerNull(row["leave_app"]);
                        RetObj.leave_rej = Common.CheckIntegerNull(row["leave_rej"]);
                        RetObj.order_view = Common.CheckIntegerNull(row["order_view"]);
                        RetObj.order_add = Common.CheckIntegerNull(row["order_add"]);
                        RetObj.order_edit = Common.CheckIntegerNull(row["order_edit"]);
                        RetObj.order_del = Common.CheckIntegerNull(row["order_del"]);
                        RetObj.order_app = Common.CheckIntegerNull(row["order_app"]);
                        RetObj.order_rej = Common.CheckIntegerNull(row["order_rej"]);
                        RetObj.cmpln_view = Common.CheckIntegerNull(row["cmpln_view"]);
                        RetObj.cmpln_add = Common.CheckIntegerNull(row["cmpln_add"]);
                        RetObj.cmpln_edit = Common.CheckIntegerNull(row["cmpln_edit"]);
                        RetObj.cmpln_del = Common.CheckIntegerNull(row["cmpln_del"]);
                        RetObj.cmpln_action = Common.CheckIntegerNull(row["cmpln_action"]);
                        RetObj.loan_view = Common.CheckIntegerNull(row["loan_view"]);
                        RetObj.loan_add = Common.CheckIntegerNull(row["loan_add"]);
                        RetObj.loan_edit = Common.CheckIntegerNull(row["loan_edit"]);
                        RetObj.loan_del = Common.CheckIntegerNull(row["loan_del"]);
                        RetObj.loan_app = Common.CheckIntegerNull(row["loan_app"]);
                        RetObj.loan_reject = Common.CheckIntegerNull(row["loan_reject"]);
                        RetObj.loan_app_hod_show = Common.CheckIntegerNull(row["loan_app_hod_show"]);
                        RetObj.loan_app_gm_show = Common.CheckIntegerNull(row["loan_app_gm_show"]);
                        RetObj.loan_app_hr_show = Common.CheckIntegerNull(row["loan_app_hr_show"]);
                        RetObj.loan_app_ed_show = Common.CheckIntegerNull(row["loan_app_ed_show"]);
                        RetObj.loan_reject_show = Common.CheckIntegerNull(row["loan_reject_show"]);
                        RetObj.notif_view = Common.CheckIntegerNull(row["notif_view"]);
                        RetObj.notif_add = Common.CheckIntegerNull(row["notif_add"]);
                        RetObj.notif_edit = Common.CheckIntegerNull(row["notif_edit"]);
                        RetObj.notif_del = Common.CheckIntegerNull(row["notif_del"]);
                        RetObj.resig_view = Common.CheckIntegerNull(row["resig_view"]);
                        RetObj.resig_add = Common.CheckIntegerNull(row["resig_add"]);
                        RetObj.resig_edit = Common.CheckIntegerNull(row["resig_edit"]);
                        RetObj.resig_del = Common.CheckIntegerNull(row["resig_del"]);
                        RetObj.resig_app = Common.CheckIntegerNull(row["resig_app"]);
                        RetObj.resig_reject = Common.CheckIntegerNull(row["resig_reject"]);
                        RetObj.user_view = Common.CheckIntegerNull(row["user_view"]);
                        RetObj.user_add = Common.CheckIntegerNull(row["user_add"]);
                        RetObj.user_edit = Common.CheckIntegerNull(row["user_edit"]);
                        RetObj.user_del = Common.CheckIntegerNull(row["user_del"]);
                        RetObj.role_view = Common.CheckIntegerNull(row["role_view"]);
                        RetObj.role_add = Common.CheckIntegerNull(row["role_add"]);
                        RetObj.role_edit = Common.CheckIntegerNull(row["role_edit"]);
                        RetObj.role_del = Common.CheckIntegerNull(row["role_del"]);
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
                            strUpdateDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdateDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdateDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.vehic_view = Common.CheckIntegerNull(row["vehic_view"]);
                        RetObj.vehic_add = Common.CheckIntegerNull(row["vehic_add"]);
                        RetObj.vehic_edit = Common.CheckIntegerNull(row["vehic_edit"]);
                        RetObj.vehic_del = Common.CheckIntegerNull(row["vehic_del"]);
                        RetObj.mark_attendance = Common.CheckIntegerNull(row["mark_attendance"]);
                        RetObj.dash_att_rep = Common.CheckIntegerNull(row["dash_att_rep"]);
                        RetObj.dash_att_count = Common.CheckIntegerNull(row["dash_att_count"]);
                        RetObj.dash_leave_bal = Common.CheckIntegerNull(row["dash_leave_bal"]);
                        RetObj.leave_menu = Common.CheckIntegerNull(row["leave_menu"]);
                        RetObj.leave_my_req_sm = Common.CheckIntegerNull(row["leave_my_req_sm"]);
                        RetObj.leave_hod_app_sm = Common.CheckIntegerNull(row["leave_hod_app_sm"]);
                        RetObj.leave_hr_app_sm = Common.CheckIntegerNull(row["leave_hr_app_sm"]);
                        RetObj.leave_hod_app_btn = Common.CheckIntegerNull(row["leave_hod_app_btn"]);
                        RetObj.o_d_menu = Common.CheckIntegerNull(row["o_d_menu"]);
                        RetObj.o_d_my_req_sm = Common.CheckIntegerNull(row["o_d_my_req_sm"]);
                        RetObj.o_d_hod_app_sm = Common.CheckIntegerNull(row["o_d_hod_app_sm"]);
                        RetObj.o_d_hr_app_sm = Common.CheckIntegerNull(row["o_d_hr_app_sm"]);
                        RetObj.o_d_hod_app_btn = Common.CheckIntegerNull(row["o_d_hod_app_btn"]);
                        RetObj.task_menu = Common.CheckIntegerNull(row["task_menu"]);
                        RetObj.task_assigned_sm = Common.CheckIntegerNull(row["task_assigned_sm"]);
                        RetObj.task_rej = Common.CheckIntegerNull(row["task_rej"]);
                        RetObj.task_poc_app = Common.CheckIntegerNull(row["task_poc_app"]);
                        RetObj.task_compelete = Common.CheckIntegerNull(row["task_compelete"]);
                        RetObj.order_menu = Common.CheckIntegerNull(row["order_menu"]);
                        RetObj.o_d_view = Common.CheckIntegerNull(row["o_d_view"]);
                        RetObj.o_d_add = Common.CheckIntegerNull(row["o_d_add"]);
                        RetObj.o_d_edit = Common.CheckIntegerNull(row["o_d_edit"]);
                        RetObj.o_d_del = Common.CheckIntegerNull(row["o_d_del"]);
                        RetObj.o_d_app = Common.CheckIntegerNull(row["o_d_app"]);
                        RetObj.clear_menu = Common.CheckIntegerNull(row["clear_menu"]);
                        RetObj.clear_hr_app_sm = Common.CheckIntegerNull(row["clear_hr_app_sm"]);
                        RetObj.clear_hod_app_sm = Common.CheckIntegerNull(row["clear_hod_app_sm"]);
                        RetObj.clear_dep_app_sm = Common.CheckIntegerNull(row["clear_dep_app_sm"]);
                        RetObj.clear_add = Common.CheckIntegerNull(row["clear_add"]);
                        RetObj.clear_edit = Common.CheckIntegerNull(row["clear_edit"]);
                        RetObj.clear_del = Common.CheckIntegerNull(row["clear_del"]);
                        RetObj.clear_hod_app = Common.CheckIntegerNull(row["clear_hod_app"]);
                        RetObj.clear_dep_app = Common.CheckIntegerNull(row["clear_dep_app"]);
                        RetObj.clear_app = Common.CheckIntegerNull(row["clear_app"]);
                        RetObj.o_d_rej = Common.CheckIntegerNull(row["o_d_rej"]);
                        RetObj.dash_menu = Common.CheckIntegerNull(row["dash_menu"]);
                        RetObj.compln_menu = Common.CheckIntegerNull(row["compln_menu"]);
                        RetObj.compln_my_sm = Common.CheckIntegerNull(row["compln_my_sm"]);
                        RetObj.compln_hr_sm = Common.CheckIntegerNull(row["compln_hr_sm"]);
                        RetObj.compln_add = Common.CheckIntegerNull(row["compln_add"]);
                        RetObj.compln_del = Common.CheckIntegerNull(row["compln_del"]);
                        RetObj.compln_edit = Common.CheckIntegerNull(row["compln_edit"]);
                        RetObj.compln_app = Common.CheckIntegerNull(row["compln_app"]);
                        RetObj.compln_rej = Common.CheckIntegerNull(row["compln_rej"]);
                        RetObj.hr_corsp_menu = Common.CheckIntegerNull(row["hr_corsp_menu"]);
                        RetObj.notification_menu = Common.CheckIntegerNull(row["notification_menu"]);
                        RetObj.wpp_menu = Common.CheckIntegerNull(row["wpp_menu"]);
                        RetObj.user_pswd_update = Common.CheckIntegerNull(row["user_pswd_update"]);
                        RetObj.allowed_company = Common.CheckIntegerNull(row["allowed_company"]);
                        RetObj.allowed_department = Common.CheckIntegerNull(row["allowed_department"]);
                        RetObj.setup_menu = Common.CheckIntegerNull(row["setup_menu"]);
                        RetObj.setup_sm_task_type = Common.CheckIntegerNull(row["setup_sm_task_type"]);
                        RetObj.setup_sm_task_sub_type = Common.CheckIntegerNull(row["setup_sm_task_sub_type"]);
                        RetObj.logs = Common.CheckIntegerNull(row["logs"]);
                        RetObj.master_acc = Common.CheckIntegerNull(row["master_acc"]);
                        RetObj.office_add = Common.CheckIntegerNull(row["office_add"]);
                        RetObj.office_del = Common.CheckIntegerNull(row["office_del"]);
                        RetObj.office_edit = Common.CheckIntegerNull(row["office_edit"]);
                        RetObj.office_menu = Common.CheckIntegerNull(row["office_menu"]);
                        RetObj.map_office = Common.CheckIntegerNull(row["map_office"]);
                        RetObj.vehic_hod_sub_menu = Common.CheckIntegerNull(row["vehic_hod_sub_menu"]);
                        RetObj.vehic_gm_sub_menu = Common.CheckIntegerNull(row["vehic_gm_sub_menu"]);
                        RetObj.vehic_hr_sub_menu = Common.CheckIntegerNull(row["vehic_hr_sub_menu"]);
                        RetObj.vehic_admin_sub_menu = Common.CheckIntegerNull(row["vehic_admin_sub_menu"]);
                        RetObj.vehic_ed_sub_menu = Common.CheckIntegerNull(row["vehic_ed_sub_menu"]);
                        RetObj.hc_menu = Common.CheckIntegerNull(row["hc_menu"]);
                        RetObj.hc_invid_sub_menu = Common.CheckIntegerNull(row["hc_invid_sub_menu"]);
                        RetObj.hc_dep_sub_menu = Common.CheckIntegerNull(row["hc_dep_sub_menu"]);
                        RetObj.hc_notice_sub_menu = Common.CheckIntegerNull(row["hc_notice_sub_menu"]);
                        RetObj.sal_menu = Common.CheckIntegerNull(row["sal_menu"]);
                        RetObj.my_sub_menu = Common.CheckIntegerNull(row["my_sub_menu"]);
                        RetObj.my_hod_menu = Common.CheckIntegerNull(row["my_hod_menu"]);
                        RetObj.my_hr_menu = Common.CheckIntegerNull(row["my_hr_menu"]);
                        RetObj.wwp_create_sub_menu = Common.CheckIntegerNull(row["wwp_create_sub_menu"]);
                        RetObj.wwp_inbox_sub_menu = Common.CheckIntegerNull(row["wwp_inbox_sub_menu"]);
                        RetObj.leave_apply_sm = Common.CheckIntegerNull(row["leave_apply_sm"]);
                        RetObj.od_apply_sm = Common.CheckIntegerNull(row["od_apply_sm"]);
                        RetObj.loan_apply_sm = Common.CheckIntegerNull(row["loan_apply_sm"]);
                        RetObj.vehicle_apply_sm = Common.CheckIntegerNull(row["vehicle_apply_sm"]);
                        RetObj.task_hod = Common.CheckIntegerNull(row["task_hod"]);
                        RetObj.task_ed = Common.CheckIntegerNull(row["task_ed"]);
                        RetObj.prog_tracking = Common.CheckIntegerNull(row["prog_tracking"]);
                        RetObj.prog_track_report = Common.CheckIntegerNull(row["prog_track_report"]);
                        RetObj.projects = Common.CheckIntegerNull(row["projects"]);
                        RetObj.prog_menu = Common.CheckIntegerNull(row["prog_menu"]);
                        RetObj.prog_status = Common.CheckIntegerNull(row["prog_status"]);
                        RetObj.sync_emp = Common.CheckIntegerNull(row["sync_emp"]);
                        RetObj.add_job = Common.CheckIntegerNull(row["add_job"]);
                        RetObj.add_location = Common.CheckIntegerNull(row["add_location"]);
                        RetObj.bonus_ded = Common.CheckIntegerNull(row["bonus_ded"]);


                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;

                // if want to show in single object
                RObj.RoleData = RetObj;
                // if want to show in list of object
                //RObj.RoleDataList = RetLst;
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
        public RoleReturn GetRoleByEmp(RoleModel getModel) // get modal from user in getModel
        {
            RoleReturn RObj = new RoleReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[5];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 7 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[4] = new SqlParameter("@role_id", SqlDbType.Int) { Value = getModel.role_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_roles_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";

                var strCreatedDate = "";
                var strUpdateDate = "";


                // for single object declaration
                var RetObj = new RoleModel();
                // for list object declaration
                List<RoleModel> RetLst = new List<RoleModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new RoleModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.role_id = Common.CheckIntegerNull(row["role_id"]);
                        RetObj.role_name = Common.CheckStringNull(row["role_name"]);
                        RetObj.role_description = Common.CheckStringNull(row["role_description"]);
                        RetObj.att_view = Common.CheckIntegerNull(row["att_view"]);
                        RetObj.att_add = Common.CheckIntegerNull(row["att_add"]);
                        RetObj.att_edit = Common.CheckIntegerNull(row["att_edit"]);
                        RetObj.att_del = Common.CheckIntegerNull(row["att_del"]);
                        RetObj.task_view = Common.CheckIntegerNull(row["task_view"]);
                        RetObj.task_add = Common.CheckIntegerNull(row["task_add"]);
                        RetObj.task_edit = Common.CheckIntegerNull(row["task_edit"]);
                        RetObj.task_del = Common.CheckIntegerNull(row["task_del"]);
                        RetObj.task_assignee = Common.CheckIntegerNull(row["task_assignee"]);
                        RetObj.leave_view = Common.CheckIntegerNull(row["leave_view"]);
                        RetObj.leave_add = Common.CheckIntegerNull(row["leave_add"]);
                        RetObj.leave_edit = Common.CheckIntegerNull(row["leave_edit"]);
                        RetObj.leave_del = Common.CheckIntegerNull(row["leave_del"]);
                        RetObj.leave_app = Common.CheckIntegerNull(row["leave_app"]);
                        RetObj.leave_rej = Common.CheckIntegerNull(row["leave_rej"]);
                        RetObj.order_view = Common.CheckIntegerNull(row["order_view"]);
                        RetObj.order_add = Common.CheckIntegerNull(row["order_add"]);
                        RetObj.order_edit = Common.CheckIntegerNull(row["order_edit"]);
                        RetObj.order_del = Common.CheckIntegerNull(row["order_del"]);
                        RetObj.order_app = Common.CheckIntegerNull(row["order_app"]);
                        RetObj.order_rej = Common.CheckIntegerNull(row["order_rej"]);
                        RetObj.cmpln_view = Common.CheckIntegerNull(row["cmpln_view"]);
                        RetObj.cmpln_add = Common.CheckIntegerNull(row["cmpln_add"]);
                        RetObj.cmpln_edit = Common.CheckIntegerNull(row["cmpln_edit"]);
                        RetObj.cmpln_del = Common.CheckIntegerNull(row["cmpln_del"]);
                        RetObj.cmpln_action = Common.CheckIntegerNull(row["cmpln_action"]);
                        RetObj.loan_view = Common.CheckIntegerNull(row["loan_view"]);
                        RetObj.loan_add = Common.CheckIntegerNull(row["loan_add"]);
                        RetObj.loan_edit = Common.CheckIntegerNull(row["loan_edit"]);
                        RetObj.loan_del = Common.CheckIntegerNull(row["loan_del"]);
                        RetObj.loan_app = Common.CheckIntegerNull(row["loan_app"]);
                        RetObj.loan_reject = Common.CheckIntegerNull(row["loan_reject"]);
                        RetObj.loan_app_hod_show = Common.CheckIntegerNull(row["loan_app_hod_show"]);
                        RetObj.loan_app_gm_show = Common.CheckIntegerNull(row["loan_app_gm_show"]);
                        RetObj.loan_app_hr_show = Common.CheckIntegerNull(row["loan_app_hr_show"]);
                        RetObj.loan_app_ed_show = Common.CheckIntegerNull(row["loan_app_ed_show"]);
                        RetObj.loan_reject_show = Common.CheckIntegerNull(row["loan_reject_show"]);
                        RetObj.notif_view = Common.CheckIntegerNull(row["notif_view"]);
                        RetObj.notif_add = Common.CheckIntegerNull(row["notif_add"]);
                        RetObj.notif_edit = Common.CheckIntegerNull(row["notif_edit"]);
                        RetObj.notif_del = Common.CheckIntegerNull(row["notif_del"]);
                        RetObj.resig_view = Common.CheckIntegerNull(row["resig_view"]);
                        RetObj.resig_add = Common.CheckIntegerNull(row["resig_add"]);
                        RetObj.resig_edit = Common.CheckIntegerNull(row["resig_edit"]);
                        RetObj.resig_del = Common.CheckIntegerNull(row["resig_del"]);
                        RetObj.resig_app = Common.CheckIntegerNull(row["resig_app"]);
                        RetObj.resig_reject = Common.CheckIntegerNull(row["resig_reject"]);
                        RetObj.user_view = Common.CheckIntegerNull(row["user_view"]);
                        RetObj.user_add = Common.CheckIntegerNull(row["user_add"]);
                        RetObj.user_edit = Common.CheckIntegerNull(row["user_edit"]);
                        RetObj.user_del = Common.CheckIntegerNull(row["user_del"]);
                        RetObj.role_view = Common.CheckIntegerNull(row["role_view"]);
                        RetObj.role_add = Common.CheckIntegerNull(row["role_add"]);
                        RetObj.role_edit = Common.CheckIntegerNull(row["role_edit"]);
                        RetObj.role_del = Common.CheckIntegerNull(row["role_del"]);
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
                            strUpdateDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdateDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdateDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.vehic_view = Common.CheckIntegerNull(row["vehic_view"]);
                        RetObj.vehic_add = Common.CheckIntegerNull(row["vehic_add"]);
                        RetObj.vehic_edit = Common.CheckIntegerNull(row["vehic_edit"]);
                        RetObj.vehic_del = Common.CheckIntegerNull(row["vehic_del"]);
                        RetObj.mark_attendance = Common.CheckIntegerNull(row["mark_attendance"]);
                        RetObj.dash_att_rep = Common.CheckIntegerNull(row["dash_att_rep"]);
                        RetObj.dash_att_count = Common.CheckIntegerNull(row["dash_att_count"]);
                        RetObj.dash_leave_bal = Common.CheckIntegerNull(row["dash_leave_bal"]);
                        RetObj.leave_menu = Common.CheckIntegerNull(row["leave_menu"]);
                        RetObj.leave_my_req_sm = Common.CheckIntegerNull(row["leave_my_req_sm"]);
                        RetObj.leave_hod_app_sm = Common.CheckIntegerNull(row["leave_hod_app_sm"]);
                        RetObj.leave_hr_app_sm = Common.CheckIntegerNull(row["leave_hr_app_sm"]);
                        RetObj.leave_hod_app_btn = Common.CheckIntegerNull(row["leave_hod_app_btn"]);
                        RetObj.o_d_menu = Common.CheckIntegerNull(row["o_d_menu"]);
                        RetObj.o_d_my_req_sm = Common.CheckIntegerNull(row["o_d_my_req_sm"]);
                        RetObj.o_d_hod_app_sm = Common.CheckIntegerNull(row["o_d_hod_app_sm"]);
                        RetObj.o_d_hr_app_sm = Common.CheckIntegerNull(row["o_d_hr_app_sm"]);
                        RetObj.o_d_hod_app_btn = Common.CheckIntegerNull(row["o_d_hod_app_btn"]);
                        RetObj.task_menu = Common.CheckIntegerNull(row["task_menu"]);
                        RetObj.task_assigned_sm = Common.CheckIntegerNull(row["task_assigned_sm"]);
                        RetObj.task_rej = Common.CheckIntegerNull(row["task_rej"]);
                        RetObj.task_poc_app = Common.CheckIntegerNull(row["task_poc_app"]);
                        RetObj.task_compelete = Common.CheckIntegerNull(row["task_compelete"]);
                        RetObj.order_menu = Common.CheckIntegerNull(row["order_menu"]);
                        RetObj.o_d_view = Common.CheckIntegerNull(row["o_d_view"]);
                        RetObj.o_d_add = Common.CheckIntegerNull(row["o_d_add"]);
                        RetObj.o_d_edit = Common.CheckIntegerNull(row["o_d_edit"]);
                        RetObj.o_d_del = Common.CheckIntegerNull(row["o_d_del"]);
                        RetObj.o_d_app = Common.CheckIntegerNull(row["o_d_app"]);
                        RetObj.clear_menu = Common.CheckIntegerNull(row["clear_menu"]);
                        RetObj.clear_hr_app_sm = Common.CheckIntegerNull(row["clear_hr_app_sm"]);
                        RetObj.clear_hod_app_sm = Common.CheckIntegerNull(row["clear_hod_app_sm"]);
                        RetObj.clear_dep_app_sm = Common.CheckIntegerNull(row["clear_dep_app_sm"]);
                        RetObj.clear_add = Common.CheckIntegerNull(row["clear_add"]);
                        RetObj.clear_edit = Common.CheckIntegerNull(row["clear_edit"]);
                        RetObj.clear_del = Common.CheckIntegerNull(row["clear_del"]);
                        RetObj.clear_hod_app = Common.CheckIntegerNull(row["clear_hod_app"]);
                        RetObj.clear_dep_app = Common.CheckIntegerNull(row["clear_dep_app"]);
                        RetObj.clear_app = Common.CheckIntegerNull(row["clear_app"]);
                        RetObj.o_d_rej = Common.CheckIntegerNull(row["o_d_rej"]);
                        RetObj.dash_menu = Common.CheckIntegerNull(row["dash_menu"]);
                        RetObj.compln_menu = Common.CheckIntegerNull(row["compln_menu"]);
                        RetObj.compln_my_sm = Common.CheckIntegerNull(row["compln_my_sm"]);
                        RetObj.compln_hr_sm = Common.CheckIntegerNull(row["compln_hr_sm"]);
                        RetObj.compln_add = Common.CheckIntegerNull(row["compln_add"]);
                        RetObj.compln_del = Common.CheckIntegerNull(row["compln_del"]);
                        RetObj.compln_edit = Common.CheckIntegerNull(row["compln_edit"]);
                        RetObj.compln_app = Common.CheckIntegerNull(row["compln_app"]);
                        RetObj.compln_rej = Common.CheckIntegerNull(row["compln_rej"]);
                        RetObj.hr_corsp_menu = Common.CheckIntegerNull(row["hr_corsp_menu"]);
                        RetObj.notification_menu = Common.CheckIntegerNull(row["notification_menu"]);
                        RetObj.wpp_menu = Common.CheckIntegerNull(row["wpp_menu"]);
                        RetObj.user_pswd_update = Common.CheckIntegerNull(row["user_pswd_update"]);
                        RetObj.allowed_company = Common.CheckIntegerNull(row["allowed_company"]);
                        RetObj.allowed_department = Common.CheckIntegerNull(row["allowed_department"]);
                        RetObj.setup_menu = Common.CheckIntegerNull(row["setup_menu"]);
                        RetObj.setup_sm_task_type = Common.CheckIntegerNull(row["setup_sm_task_type"]);
                        RetObj.setup_sm_task_sub_type = Common.CheckIntegerNull(row["setup_sm_task_sub_type"]);
                        RetObj.logs = Common.CheckIntegerNull(row["logs"]);
                        RetObj.master_acc = Common.CheckIntegerNull(row["master_acc"]);
                        RetObj.office_add = Common.CheckIntegerNull(row["office_add"]);
                        RetObj.office_del = Common.CheckIntegerNull(row["office_del"]);
                        RetObj.office_edit = Common.CheckIntegerNull(row["office_edit"]);
                        RetObj.office_menu = Common.CheckIntegerNull(row["office_menu"]);
                        RetObj.map_office = Common.CheckIntegerNull(row["map_office"]);
                        RetObj.vehic_hod_sub_menu = Common.CheckIntegerNull(row["vehic_hod_sub_menu"]);
                        RetObj.vehic_gm_sub_menu = Common.CheckIntegerNull(row["vehic_gm_sub_menu"]);
                        RetObj.vehic_hr_sub_menu = Common.CheckIntegerNull(row["vehic_hr_sub_menu"]);
                        RetObj.vehic_admin_sub_menu = Common.CheckIntegerNull(row["vehic_admin_sub_menu"]);
                        RetObj.vehic_ed_sub_menu = Common.CheckIntegerNull(row["vehic_ed_sub_menu"]);
                        RetObj.hc_menu = Common.CheckIntegerNull(row["hc_menu"]);
                        RetObj.hc_invid_sub_menu = Common.CheckIntegerNull(row["hc_invid_sub_menu"]);
                        RetObj.hc_dep_sub_menu = Common.CheckIntegerNull(row["hc_dep_sub_menu"]);
                        RetObj.hc_notice_sub_menu = Common.CheckIntegerNull(row["hc_notice_sub_menu"]);
                        RetObj.sal_menu = Common.CheckIntegerNull(row["sal_menu"]);
                        RetObj.my_sub_menu = Common.CheckIntegerNull(row["my_sub_menu"]);
                        RetObj.my_hod_menu = Common.CheckIntegerNull(row["my_hod_menu"]);
                        RetObj.my_hr_menu = Common.CheckIntegerNull(row["my_hr_menu"]);
                        RetObj.wwp_create_sub_menu = Common.CheckIntegerNull(row["wwp_create_sub_menu"]);
                        RetObj.wwp_inbox_sub_menu = Common.CheckIntegerNull(row["wwp_inbox_sub_menu"]);
                        RetObj.leave_apply_sm = Common.CheckIntegerNull(row["leave_apply_sm"]);
                        RetObj.od_apply_sm = Common.CheckIntegerNull(row["od_apply_sm"]);
                        RetObj.loan_apply_sm = Common.CheckIntegerNull(row["loan_apply_sm"]);
                        RetObj.vehicle_apply_sm = Common.CheckIntegerNull(row["vehicle_apply_sm"]);
                        RetObj.task_hod = Common.CheckIntegerNull(row["task_hod"]);
                        RetObj.task_ed = Common.CheckIntegerNull(row["task_ed"]);
                        RetObj.prog_tracking = Common.CheckIntegerNull(row["prog_tracking"]);
                        RetObj.prog_track_report = Common.CheckIntegerNull(row["prog_track_report"]);
                        RetObj.projects = Common.CheckIntegerNull(row["projects"]);
                        RetObj.prog_menu = Common.CheckIntegerNull(row["prog_menu"]);
                        RetObj.prog_status = Common.CheckIntegerNull(row["prog_status"]);
                        RetObj.sync_emp = Common.CheckIntegerNull(row["sync_emp"]);
                        RetObj.add_job = Common.CheckIntegerNull(row["add_job"]);
                        RetObj.add_location = Common.CheckIntegerNull(row["add_location"]);
                        RetObj.bonus_ded = Common.CheckIntegerNull(row["bonus_ded"]);
                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;

                // if want to show in single object
                RObj.RoleData = RetObj;
                // if want to show in list of object
                //RObj.RoleDataList = RetLst;
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
        public RoleReturn AUDSingleRole(RoleModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            RoleReturn RObj = new RoleReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[160];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                parm[2] = new SqlParameter("role_id", SqlDbType.Int) { Value = getModel.role_id };
                parm[3] = new SqlParameter("role_name", SqlDbType.VarChar) { Value = getModel.role_name };
                parm[4] = new SqlParameter("role_description", SqlDbType.VarChar) { Value = getModel.role_description };
                parm[5] = new SqlParameter("att_view", SqlDbType.Int) { Value = getModel.att_view };
                parm[6] = new SqlParameter("att_add", SqlDbType.Int) { Value = getModel.att_add };
                parm[7] = new SqlParameter("att_edit", SqlDbType.Int) { Value = getModel.att_edit };
                parm[8] = new SqlParameter("att_del", SqlDbType.Int) { Value = getModel.att_del };
                parm[9] = new SqlParameter("task_view", SqlDbType.Int) { Value = getModel.task_view };
                parm[10] = new SqlParameter("task_add", SqlDbType.Int) { Value = getModel.task_add };
                parm[11] = new SqlParameter("task_edit", SqlDbType.Int) { Value = getModel.task_edit };
                parm[12] = new SqlParameter("task_del", SqlDbType.Int) { Value = getModel.task_del };
                parm[13] = new SqlParameter("task_assignee", SqlDbType.Int) { Value = getModel.task_assignee };
                parm[14] = new SqlParameter("leave_view", SqlDbType.Int) { Value = getModel.leave_view };
                parm[15] = new SqlParameter("leave_add", SqlDbType.Int) { Value = getModel.leave_add };
                parm[16] = new SqlParameter("leave_edit", SqlDbType.Int) { Value = getModel.leave_edit };
                parm[17] = new SqlParameter("leave_del", SqlDbType.Int) { Value = getModel.leave_del };
                parm[18] = new SqlParameter("leave_app", SqlDbType.Int) { Value = getModel.leave_app };
                parm[19] = new SqlParameter("leave_rej", SqlDbType.Int) { Value = getModel.leave_rej };
                parm[20] = new SqlParameter("order_view", SqlDbType.Int) { Value = getModel.order_view };
                parm[21] = new SqlParameter("order_add", SqlDbType.Int) { Value = getModel.order_add };
                parm[22] = new SqlParameter("order_edit", SqlDbType.Int) { Value = getModel.order_edit };
                parm[23] = new SqlParameter("order_del", SqlDbType.Int) { Value = getModel.order_del };
                parm[24] = new SqlParameter("order_app", SqlDbType.Int) { Value = getModel.order_app };
                parm[25] = new SqlParameter("order_rej", SqlDbType.Int) { Value = getModel.order_rej };
                parm[26] = new SqlParameter("cmpln_view", SqlDbType.Int) { Value = getModel.cmpln_view };
                parm[27] = new SqlParameter("cmpln_add", SqlDbType.Int) { Value = getModel.cmpln_add };
                parm[28] = new SqlParameter("cmpln_edit", SqlDbType.Int) { Value = getModel.cmpln_edit };
                parm[29] = new SqlParameter("cmpln_del", SqlDbType.Int) { Value = getModel.cmpln_del };
                parm[30] = new SqlParameter("cmpln_action", SqlDbType.Int) { Value = getModel.cmpln_action };
                parm[31] = new SqlParameter("loan_view", SqlDbType.Int) { Value = getModel.loan_view };
                parm[32] = new SqlParameter("loan_add", SqlDbType.Int) { Value = getModel.loan_add };
                parm[33] = new SqlParameter("loan_edit", SqlDbType.Int) { Value = getModel.loan_edit };
                parm[34] = new SqlParameter("loan_del", SqlDbType.Int) { Value = getModel.loan_del };
                parm[35] = new SqlParameter("loan_app", SqlDbType.Int) { Value = getModel.loan_app };
                parm[36] = new SqlParameter("loan_reject", SqlDbType.Int) { Value = getModel.loan_reject };
                parm[37] = new SqlParameter("loan_app_hod_show", SqlDbType.Int) { Value = getModel.loan_app_hod_show };
                parm[38] = new SqlParameter("loan_app_gm_show", SqlDbType.Int) { Value = getModel.loan_app_gm_show };
                parm[39] = new SqlParameter("loan_app_hr_show", SqlDbType.Int) { Value = getModel.loan_app_hr_show };
                parm[40] = new SqlParameter("loan_app_ed_show", SqlDbType.Int) { Value = getModel.loan_app_ed_show };
                parm[41] = new SqlParameter("loan_reject_show", SqlDbType.Int) { Value = getModel.loan_reject_show };
                parm[42] = new SqlParameter("notif_view", SqlDbType.Int) { Value = getModel.notif_view };
                parm[43] = new SqlParameter("notif_add", SqlDbType.Int) { Value = getModel.notif_add };
                parm[44] = new SqlParameter("notif_edit", SqlDbType.Int) { Value = getModel.notif_edit };
                parm[45] = new SqlParameter("notif_del", SqlDbType.Int) { Value = getModel.notif_del };
                parm[46] = new SqlParameter("resig_view", SqlDbType.Int) { Value = getModel.resig_view };
                parm[47] = new SqlParameter("resig_add", SqlDbType.Int) { Value = getModel.resig_add };
                parm[48] = new SqlParameter("resig_edit", SqlDbType.Int) { Value = getModel.resig_edit };
                parm[49] = new SqlParameter("resig_del", SqlDbType.Int) { Value = getModel.resig_del };
                parm[50] = new SqlParameter("resig_app", SqlDbType.Int) { Value = getModel.resig_app };
                parm[51] = new SqlParameter("resig_reject", SqlDbType.Int) { Value = getModel.resig_reject };
                parm[52] = new SqlParameter("user_view", SqlDbType.Int) { Value = getModel.user_view };
                parm[53] = new SqlParameter("user_add", SqlDbType.Int) { Value = getModel.user_add };
                parm[54] = new SqlParameter("user_edit", SqlDbType.Int) { Value = getModel.user_edit };
                parm[55] = new SqlParameter("user_del", SqlDbType.Int) { Value = getModel.user_del };
                parm[56] = new SqlParameter("role_view", SqlDbType.Int) { Value = getModel.role_view };
                parm[57] = new SqlParameter("role_add", SqlDbType.Int) { Value = getModel.role_add };
                parm[58] = new SqlParameter("role_edit", SqlDbType.Int) { Value = getModel.role_edit };
                parm[59] = new SqlParameter("role_del", SqlDbType.Int) { Value = getModel.role_del };
                parm[60] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                parm[61] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
                parm[62] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[63] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                parm[64] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[65] = new SqlParameter("vehic_view", SqlDbType.Int) { Value = getModel.vehic_view };
                parm[66] = new SqlParameter("vehic_add", SqlDbType.Int) { Value = getModel.vehic_add };
                parm[67] = new SqlParameter("vehic_edit", SqlDbType.Int) { Value = getModel.vehic_edit };
                parm[68] = new SqlParameter("vehic_del", SqlDbType.Int) { Value = getModel.vehic_del };
                parm[69] = new SqlParameter("mark_attendance", SqlDbType.Int) { Value = getModel.mark_attendance };
                parm[70] = new SqlParameter("dash_att_rep", SqlDbType.Int) { Value = getModel.dash_att_rep };
                parm[71] = new SqlParameter("dash_att_count", SqlDbType.Int) { Value = getModel.dash_att_count };
                parm[72] = new SqlParameter("dash_leave_bal", SqlDbType.Int) { Value = getModel.dash_leave_bal };
                parm[73] = new SqlParameter("leave_menu", SqlDbType.Int) { Value = getModel.leave_menu };
                parm[74] = new SqlParameter("leave_my_req_sm", SqlDbType.Int) { Value = getModel.leave_my_req_sm };
                parm[75] = new SqlParameter("leave_hod_app_sm", SqlDbType.Int) { Value = getModel.leave_hod_app_sm };
                parm[76] = new SqlParameter("leave_hr_app_sm", SqlDbType.Int) { Value = getModel.leave_hr_app_sm };
                parm[77] = new SqlParameter("leave_hod_app_btn", SqlDbType.Int) { Value = getModel.leave_hod_app_btn };
                parm[78] = new SqlParameter("o_d_menu", SqlDbType.Int) { Value = getModel.o_d_menu };
                parm[79] = new SqlParameter("o_d_my_req_sm", SqlDbType.Int) { Value = getModel.o_d_my_req_sm };
                parm[80] = new SqlParameter("o_d_hod_app_sm", SqlDbType.Int) { Value = getModel.o_d_hod_app_sm };
                parm[81] = new SqlParameter("o_d_hr_app_sm", SqlDbType.Int) { Value = getModel.o_d_hr_app_sm };
                parm[82] = new SqlParameter("o_d_hod_app_btn", SqlDbType.Int) { Value = getModel.o_d_hod_app_btn };
                parm[83] = new SqlParameter("task_menu", SqlDbType.Int) { Value = getModel.task_menu };
                parm[84] = new SqlParameter("task_assigned_sm", SqlDbType.Int) { Value = getModel.task_assigned_sm };
                parm[85] = new SqlParameter("task_rej", SqlDbType.Int) { Value = getModel.task_rej };
                parm[86] = new SqlParameter("task_poc_app", SqlDbType.Int) { Value = getModel.task_poc_app };
                parm[87] = new SqlParameter("task_compelete", SqlDbType.Int) { Value = getModel.task_compelete };
                parm[88] = new SqlParameter("order_menu", SqlDbType.Int) { Value = getModel.order_menu };
                parm[89] = new SqlParameter("o_d_view", SqlDbType.Int) { Value = getModel.o_d_view };
                parm[90] = new SqlParameter("o_d_add", SqlDbType.Int) { Value = getModel.o_d_add };
                parm[91] = new SqlParameter("o_d_edit", SqlDbType.Int) { Value = getModel.o_d_edit };
                parm[92] = new SqlParameter("o_d_del", SqlDbType.Int) { Value = getModel.o_d_del };
                parm[93] = new SqlParameter("o_d_app", SqlDbType.Int) { Value = getModel.o_d_app };
                parm[94] = new SqlParameter("clear_menu", SqlDbType.Int) { Value = getModel.clear_menu };
                parm[95] = new SqlParameter("clear_hr_app_sm", SqlDbType.Int) { Value = getModel.clear_hr_app_sm };
                parm[96] = new SqlParameter("clear_hod_app_sm", SqlDbType.Int) { Value = getModel.clear_hod_app_sm };
                parm[97] = new SqlParameter("clear_dep_app_sm", SqlDbType.Int) { Value = getModel.clear_dep_app_sm };
                parm[98] = new SqlParameter("clear_add", SqlDbType.Int) { Value = getModel.clear_add };
                parm[99] = new SqlParameter("clear_edit", SqlDbType.Int) { Value = getModel.clear_edit };
                parm[100] = new SqlParameter("clear_del", SqlDbType.Int) { Value = getModel.clear_del };
                parm[101] = new SqlParameter("clear_hod_app", SqlDbType.Int) { Value = getModel.clear_hod_app };
                parm[102] = new SqlParameter("clear_dep_app", SqlDbType.Int) { Value = getModel.clear_dep_app };
                parm[103] = new SqlParameter("clear_app", SqlDbType.Int) { Value = getModel.clear_app };
                parm[104] = new SqlParameter("o_d_rej", SqlDbType.Int) { Value = getModel.o_d_rej };
                parm[105] = new SqlParameter("dash_menu", SqlDbType.Int) { Value = getModel.dash_menu };
                parm[106] = new SqlParameter("compln_menu", SqlDbType.Int) { Value = getModel.compln_menu };
                parm[107] = new SqlParameter("compln_my_sm", SqlDbType.Int) { Value = getModel.compln_my_sm };
                parm[108] = new SqlParameter("compln_hr_sm", SqlDbType.Int) { Value = getModel.compln_hr_sm };
                parm[109] = new SqlParameter("compln_add", SqlDbType.Int) { Value = getModel.compln_add };
                parm[110] = new SqlParameter("compln_del", SqlDbType.Int) { Value = getModel.compln_del };
                parm[111] = new SqlParameter("compln_edit", SqlDbType.Int) { Value = getModel.compln_edit };
                parm[112] = new SqlParameter("compln_app", SqlDbType.Int) { Value = getModel.compln_app };
                parm[113] = new SqlParameter("compln_rej", SqlDbType.Int) { Value = getModel.compln_rej };
                parm[114] = new SqlParameter("hr_corsp_menu", SqlDbType.Int) { Value = getModel.hr_corsp_menu };
                parm[115] = new SqlParameter("notification_menu", SqlDbType.Int) { Value = getModel.notification_menu };
                parm[116] = new SqlParameter("wpp_menu", SqlDbType.Int) { Value = getModel.wpp_menu };
                parm[117] = new SqlParameter("user_pswd_update", SqlDbType.Int) { Value = getModel.user_pswd_update };
                parm[118] = new SqlParameter("allowed_company", SqlDbType.Int) { Value = getModel.allowed_company };
                parm[119] = new SqlParameter("allowed_department", SqlDbType.Int) { Value = getModel.allowed_department };
                parm[120] = new SqlParameter("setup_menu", SqlDbType.Int) { Value = getModel.setup_menu };
                parm[121] = new SqlParameter("setup_sm_task_type", SqlDbType.Int) { Value = getModel.setup_sm_task_type };
                parm[122] = new SqlParameter("setup_sm_task_sub_type", SqlDbType.Int) { Value = getModel.setup_sm_task_sub_type };
                parm[123] = new SqlParameter("logs", SqlDbType.Int) { Value = getModel.logs };
                parm[124] = new SqlParameter("master_acc", SqlDbType.Int) { Value = getModel.master_acc };
                parm[125] = new SqlParameter("office_add", SqlDbType.Int) { Value = getModel.office_add };
                parm[126] = new SqlParameter("office_menu", SqlDbType.Int) { Value = getModel.office_menu };
                parm[127] = new SqlParameter("office_del", SqlDbType.Int) { Value = getModel.office_del };
                parm[128] = new SqlParameter("office_edit", SqlDbType.Int) { Value = getModel.office_edit };
                parm[129] = new SqlParameter("map_office", SqlDbType.Int) { Value = getModel.map_office };
                parm[130] = new SqlParameter("vehic_hod_sub_menu", SqlDbType.Int) { Value = getModel.vehic_hod_sub_menu };
                parm[131] = new SqlParameter("vehic_gm_sub_menu", SqlDbType.Int) { Value = getModel.vehic_gm_sub_menu };
                parm[132] = new SqlParameter("vehic_hr_sub_menu", SqlDbType.Int) { Value = getModel.vehic_hr_sub_menu };
                parm[133] = new SqlParameter("vehic_admin_sub_menu", SqlDbType.Int) { Value = getModel.vehic_admin_sub_menu };
                parm[134] = new SqlParameter("vehic_ed_sub_menu", SqlDbType.Int) { Value = getModel.vehic_ed_sub_menu };
                parm[135] = new SqlParameter("hc_menu", SqlDbType.Int) { Value = getModel.hc_menu };
                parm[136] = new SqlParameter("hc_invid_sub_menu", SqlDbType.Int) { Value = getModel.hc_invid_sub_menu };
                parm[137] = new SqlParameter("hc_dep_sub_menu", SqlDbType.Int) { Value = getModel.hc_dep_sub_menu };
                parm[138] = new SqlParameter("hc_notice_sub_menu", SqlDbType.Int) { Value = getModel.hc_notice_sub_menu };
                parm[139] = new SqlParameter("sal_menu", SqlDbType.Int) { Value = getModel.sal_menu };
                parm[140] = new SqlParameter("my_sub_menu", SqlDbType.Int) { Value = getModel.my_sub_menu };
                parm[141] = new SqlParameter("my_hod_menu", SqlDbType.Int) { Value = getModel.my_hod_menu };
                parm[142] = new SqlParameter("my_hr_menu", SqlDbType.Int) { Value = getModel.my_hr_menu };
                parm[143] = new SqlParameter("wwp_create_sub_menu", SqlDbType.Int) { Value = getModel.wwp_create_sub_menu };
                parm[144] = new SqlParameter("wwp_inbox_sub_menu", SqlDbType.Int) { Value = getModel.wwp_inbox_sub_menu };
                parm[145] = new SqlParameter("leave_apply_sm", SqlDbType.Int) { Value = getModel.leave_apply_sm };
                parm[146] = new SqlParameter("od_apply_sm", SqlDbType.Int) { Value = getModel.od_apply_sm };
                parm[147] = new SqlParameter("loan_apply_sm", SqlDbType.Int) { Value = getModel.loan_apply_sm };
                parm[148] = new SqlParameter("vehicle_apply_sm", SqlDbType.Int) { Value = getModel.vehicle_apply_sm };

                parm[149] = new SqlParameter("task_hod", SqlDbType.Int) { Value = getModel.task_hod };
                parm[150] = new SqlParameter("task_ed", SqlDbType.Int) { Value = getModel.task_ed };
                parm[151] = new SqlParameter("prog_tracking", SqlDbType.Int) { Value = getModel.prog_tracking };
                parm[152] = new SqlParameter("prog_track_report", SqlDbType.Int) { Value = getModel.prog_track_report };
                parm[153] = new SqlParameter("projects", SqlDbType.Int) { Value = getModel.projects };
                parm[154] = new SqlParameter("prog_menu", SqlDbType.Int) { Value = getModel.prog_menu };
                parm[155] = new SqlParameter("prog_status", SqlDbType.Int) { Value = getModel.prog_status };
                parm[156] = new SqlParameter("sync_emp", SqlDbType.Int) { Value = getModel.sync_emp };
                parm[157] = new SqlParameter("add_job", SqlDbType.Int) { Value = getModel.add_job };
                parm[158] = new SqlParameter("add_location", SqlDbType.Int) { Value = getModel.add_location };
                parm[159] = new SqlParameter("bonus_ded", SqlDbType.Int) { Value = getModel.bonus_ded };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_roles_proc]", parm);
                foreach (DataRow row in dsuser.Rows)
                {
                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                }


                RObj.Status = retstatus; // Success status
                RObj.Message = retmessage;
                return RObj;
            }
            // If an error occurs during API execution
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