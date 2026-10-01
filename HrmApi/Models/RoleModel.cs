using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class RoleModel
    {
        public int Operation { get; set; }

        public int sr_id { get; set; }

        public int role_id { get; set; }

        public string role_name { get; set; }

        public string role_description { get; set; }

        public int master_acc { get; set; }

        public int att_view { get; set; }

        public int att_add { get; set; }

        public int att_edit { get; set; }

        public int att_del { get; set; }

        public int task_view { get; set; }

        public int task_add { get; set; }

        public int task_edit { get; set; }

        public int task_del { get; set; }

        public int task_assignee { get; set; }

        public int leave_view { get; set; }

        public int leave_add { get; set; }

        public int leave_edit { get; set; }

        public int leave_del { get; set; }

        public int leave_app { get; set; }

        public int leave_rej { get; set; }

        public int order_view { get; set; }

        public int order_add { get; set; }

        public int order_edit { get; set; }

        public int order_del { get; set; }

        public int order_app { get; set; }

        public int order_rej { get; set; }

        public int cmpln_view { get; set; }

        public int cmpln_add { get; set; }

        public int cmpln_edit { get; set; }

        public int cmpln_del { get; set; }

        public int cmpln_action { get; set; }

        public int loan_view { get; set; }

        public int loan_add { get; set; }

        public int loan_edit { get; set; }

        public int loan_del { get; set; }

        public int loan_app { get; set; }

        public int loan_reject { get; set; }

        public int loan_app_hod_show { get; set; }

        public int loan_app_gm_show { get; set; }

        public int loan_app_hr_show { get; set; }

        public int loan_app_ed_show { get; set; }

        public int loan_reject_show { get; set; }

        public int notif_view { get; set; }

        public int notif_add { get; set; }

        public int notif_edit { get; set; }

        public int notif_del { get; set; }

        public int resig_view { get; set; }

        public int resig_add { get; set; }

        public int resig_edit { get; set; }

        public int resig_del { get; set; }

        public int resig_app { get; set; }

        public int resig_reject { get; set; }

        public int user_view { get; set; }

        public int user_add { get; set; }

        public int user_edit { get; set; }

        public int user_del { get; set; }

        public int role_view { get; set; }

        public int role_add { get; set; }

        public int role_edit { get; set; }

        public int role_del { get; set; }

        public string created_by { get; set; }

        public DateTime created_date { get; set; }

        public string str_created_date { get; set; }

        public string updated_by { get; set; }

        public DateTime updated_date { get; set; }

        public string str_updated_date { get; set; }

        public string client_code { get; set; }

        public string company { get; set; }

        public string emp_id { get; set; }

        public int vehic_view { get; set; }

        public int vehic_add { get; set; }

        public int vehic_edit { get; set; }

        public int vehic_del { get; set; }

        public int mark_attendance { get; set; }

        public int dash_att_rep { get; set; }

        public int dash_att_count { get; set; }

        public int dash_leave_bal { get; set; }

        public int leave_menu { get; set; }

        public int leave_my_req_sm { get; set; }

        public int leave_hod_app_sm { get; set; }

        public int leave_hr_app_sm { get; set; }

        public int leave_hod_app_btn { get; set; }

        public int o_d_menu { get; set; }

        public int o_d_my_req_sm { get; set; }

        public int o_d_hod_app_sm { get; set; }

        public int o_d_hr_app_sm { get; set; }

        public int o_d_hod_app_btn { get; set; }

        public int task_menu { get; set; }

        public int task_assigned_sm { get; set; }

        public int task_rej { get; set; }

        public int task_poc_app { get; set; }

        public int task_compelete { get; set; }

        public int task_hod { get; set; }

        public int task_ed { get; set; }

        public int prog_tracking { get; set; }

        public int prog_track_report { get; set; }

        public int prog_menu { get; set; }

        public int prog_status { get; set; }

        public int projects { get; set; }

        public int order_menu { get; set; }

        public int o_d_view { get; set; }

        public int o_d_add { get; set; }

        public int o_d_edit { get; set; }

        public int o_d_del { get; set; }

        public int o_d_app { get; set; }

        public int clear_menu { get; set; }

        public int clear_hr_app_sm { get; set; }

        public int clear_hod_app_sm { get; set; }

        public int clear_dep_app_sm { get; set; }

        public int clear_add { get; set; }

        public int clear_edit { get; set; }

        public int clear_del { get; set; }

        public int clear_hod_app { get; set; }

        public int clear_dep_app { get; set; }

        public int clear_app { get; set; }

        public int o_d_rej { get; set; }

        public int dash_menu { get; set; }

        public int compln_menu { get; set; }

        public int compln_my_sm { get; set; }

        public int compln_hr_sm { get; set; }

        public int compln_add { get; set; }

        public int compln_del { get; set; }

        public int compln_edit { get; set; }

        public int compln_app { get; set; }

        public int compln_rej { get; set; }

        public int hr_corsp_menu { get; set; }

        public int notification_menu { get; set; }

        public int wpp_menu { get; set; }

        public int user_pswd_update { get; set; }

        public int allowed_company { get; set; }

        public int allowed_department { get; set; }

        public int setup_menu { get; set; }

        public int setup_sm_task_type { get; set; }

        public int setup_sm_task_sub_type { get; set; }

        public int logs { get; set; }

        public int office_menu { get; set; }

        public int office_edit { get; set; }

        public int office_del { get; set; }

        public int office_add { get; set; }

        public int map_office { get; set; }

        public int vehic_hod_sub_menu { get; set; }

        public int vehic_gm_sub_menu { get; set; }

        public int vehic_hr_sub_menu { get; set; }

        public int vehic_admin_sub_menu { get; set; }

        public int vehic_ed_sub_menu { get; set; }

        public int hc_menu { get; set; }

        public int hc_invid_sub_menu { get; set; }

        public int hc_dep_sub_menu { get; set; }

        public int hc_notice_sub_menu { get; set; }

        public int sal_menu { get; set; }

        public int my_sub_menu { get; set; }

        public int my_hod_menu { get; set; }

        public int my_hr_menu { get; set; }

        public int wwp_create_sub_menu { get; set; }

        public int wwp_inbox_sub_menu { get; set; }

        public int leave_apply_sm { get; set; }

        public int od_apply_sm { get; set; }

        public int loan_apply_sm { get; set; }

        public int vehicle_apply_sm { get; set; }

        public int sync_emp { get; set; }

        public int add_job { get; set; }

        public int add_location { get; set; }

        public int bonus_ded { get; set; }
    }
}