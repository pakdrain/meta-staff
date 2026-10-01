using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{

    public class NotifModel
    {
        public int Operation { get; set; }
        public string emp_id { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int notif_id { get; set; }
        public string notif_type { get; set; }
        public string notif_module { get; set; }
        public string notif_emp { get; set; }
        public string notif_tran_status { get; set; }
        public string notif_tran_no { get; set; }
        public DateTime? notif_tran_date { get; set; }
        public string str_notif_tran_date { get; set; }
        public int notif_read { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string updated_by { get; set; }
        public string screen { get; set; }
        public string notif_desc { get; set; }
        public int leave_my_scr { get; set; }
        public int leave_hod_scr { get; set; }
        public int leave_hr_scr { get; set; }
        public int od_hr_scr { get; set; }
        public int od_hod_scr { get; set; }
        public int od_my_scr { get; set; }
        public int loan_my_scr { get; set; }
        public int loan_hod_scr { get; set; }
        public int loan_hr_scr { get; set; }
        public int loan_gm_scr { get; set; }
        public int loan_ed_scr { get; set; }
        public int clear_hr_scr { get; set; }
        public int clear_hod_scr { get; set; }
        public int clear_departments_scr { get; set; }
        public int task_add_scr { get; set; }
        public int task_assigner_scr { get; set; }
        public int task_assigned_scr { get; set; }
        public int task_hod_scr { get; set; }
        public int cmplt_my_screen { get; set; }
        public int cmplt_hr_screen { get; set; }
        public int veh_my_scr { get; set; }
        public int veh_hod_scr { get; set; }
        public int veh_hr_scr { get; set; }
        public int veh_gm_scr { get; set; }
        public int veh_admin_scr { get; set; }
        public int veh_ed_scr { get; set; }
        public int hc_invidual_scr { get; set; }
        public int hc_dep_scr { get; set; }
        public int hc_board_scr { get; set; }
        public int sal_my_scr { get; set; }
        public int sal_hod_scr { get; set; }
        public int sal_hr_scr { get; set; }
        public int wp_create_scr { get; set; }
        public int wp_inbox_scr { get; set; }
        public int order_scr { get; set; }
        public int task_hod { get; set; }
        public int task_ed { get; set; }
    }

    public class NotificationModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int notif_id { get; set; }
        public string notif_title { get; set; }
        public string notif_desc { get; set; }
        public string notif_type { get; set; }
        public string notif_status { get; set; }
        public string for_comp { get; set; }
        public string for_emp { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
    }
}