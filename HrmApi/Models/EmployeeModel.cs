using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class EmployeeModel
    {
        public int Operation { get; set; }

        public int Sr_Id { get; set; }

        public int res_status { get; set; }

        public string res_message { get; set; }

        public string client_code { get; set; }

        public string emp_id_to_show { get; set; }

        public string emp_id { get; set; }

        public string emp_name { get; set; }

        public int first_log_in { get; set; }

        public int att_radius { get; set; }

        public int office_id { get; set; }

        public int att_radius2 { get; set; }

        public int office_id2 { get; set; }

        public string device_id { get; set; }

        public int auto_logout { get; set; }

        public string login_from { get; set; }

        public string emp_father_name { get; set; }

        public string city { get; set; }

        public string mobile_phone { get; set; }

        public string sex { get; set; }

        public string company { get; set; }

        public string branch { get; set; }

        public string department { get; set; }

        public string designation { get; set; }

        public string shift_start_time { get; set; }

        public string shift_end_time { get; set; }

        public int curr_salary { get; set; }

        public int task_type_id { get; set; }

        public string cnic { get; set; }

        public DateTime? dob { get; set; }

        public string str_dob { get; set; }

        public DateTime? doj { get; set; }

        public string str_doj { get; set; }

        public DateTime? resign_date { get; set; }

        public string str_resign_date { get; set; }

        public decimal leave_allow { get; set; }

        public int per_month_order_limit { get; set; }

        public int this_month_order_limit { get; set; }

        public int per_month_qty_limit { get; set; }

        public int this_month_qty_limit { get; set; }

        public int dicount_percetage { get; set; }

        public int role_id { get; set; }

        public string pass { get; set; }

        public DateTime? creation_date { get; set; }

        public string str_creation_date { get; set; }

        public string created_by { get; set; }

        public DateTime? update_date { get; set; }

        public string str_update_date { get; set; }

        public string update_by { get; set; }

        public string emp_img { get; set; }

        public decimal loan_limit { get; set; }

        public string src { get; set; }

        public string ofc_lat { get; set; }

        public string ofc_lon { get; set; }

        public string ofc_lat2 { get; set; }

        public string ofc_lon2 { get; set; }

        public string current_pass { get; set; }

        public string new_pass { get; set; }

        public string confirm_new_pass { get; set; }

        public string image_extension { get; set; }

        public string emp_image_url { get; set; }

        public string role_name { get; set; }

        public string org_id { get; set; }

        public int is_active { get; set; }

        public int master_acc { get; set; }

        public string server { get; set; }

        public string deactive_because { get; set; }

        public DateTime? deactive_date { get; set; }

        public string str_deactive_date { get; set; }

        public string hrm_parent_comp { get; set; }

        public string hrm_parent_emp_id { get; set; }

        public string ftn_code { get; set; }

        public string app_type { get; set; }

        public int time_tracker { get; set; }

        public int special_director_lov { get; set; }

        public string appr_company { get; set; }

        public int perform_menu { get; set; }

        public int config_menu { get; set; }

        public int policy_config { get; set; }

        public int gen_sheet_menu { get; set; }

        public int hr_sheet_menu { get; set; }

        public int hod_sheet_menu { get; set; }

        public int gm_sheet_menu { get; set; }

        public int ed_sheet_menu { get; set; }

        public int region_wise_menu { get; set; }

        public int tna_summary_menu { get; set; }

        public int summary_one_menu { get; set; }

        public int summary_two_menu { get; set; }

        public int summary_three_menu { get; set; }
    }
}