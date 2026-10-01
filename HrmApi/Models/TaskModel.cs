using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{

  

    public class TasksModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int new_task_id { get; set; }
        public string company { get; set; }
        public string dep_id { get; set; }
        public int task_id { get; set; }
        public int task_no { get; set; }
        public string task_type { get; set; }
        public string task_sub_type { get; set; }
        public string task_sub_type_name { get; set; }
        public string task_title { get; set; }
        public string task_desc { get; set; }
        public string status { get; set; }
        public string t_doc_ext { get; set; }
        public string t_doc { get; set; }
        public string hod_app_by { get; set; }
        public DateTime? hod_app_date { get; set; }
        public string str_hod_app_date { get; set; }
        public string assigner_by { get; set; }
        public DateTime? assigner_date { get; set; }
        public string str_assigner_date { get; set; }
        public string assigner_remarks { get; set; }
        public int assigned_persons { get; set; }
        public string t_priority { get; set; }
        public string complete_by { get; set; }
        public DateTime? complete_date { get; set; }
        public string str_complete_date { get; set; }
        public string complete_remarks { get; set; }
        public string re_ass_by { get; set; }
        public DateTime? re_ass_date { get; set; }
        public string str_re_ass_date { get; set; }
        public string close_by { get; set; }
        public DateTime? close_date { get; set; }
        public string str_close_date { get; set; }
        public string rej_by { get; set; }
        public DateTime? rej_date { get; set; }
        public string str_rej_date { get; set; }
        public string action_page { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string dep_name { get; set; }
        public string com_name { get; set; }
        public string t_doc_url { get; set; }
        public int ass_task_id { get; set; }
        public int portion_id { get; set; }
        public string portion_name { get; set; }
        public DateTime? start_date { get; set; }
        public string str_start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string str_end_date { get; set; }
        public string str_from_date { get; set; }
        public string str_to_date { get; set; }
        public string user_emp_id { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string creater_dep { get; set; }
        public string creater_dep_name { get; set; }
        public string re_open_by { get; set; }
        public DateTime? re_open_date { get; set; }
        public string str_re_open_date { get; set; }
        public int acc_kpi_out { get; set; }
        public int acc_kpi_in { get; set; }
        public int com_kpi_out { get; set; }
        public int com_kpi_in { get; set; }
        public List<AssignedTaskDetail>  AssignedDetail { get; set; }
    }

    public class AssignedTaskDetail
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int task_id { get; set; }
        public int ass_task_id { get; set; }
        public int portion_id { get; set; }
        public string str_start_date { get; set; }
        public DateTime? start_date { get; set; }
        public string str_end_date { get; set; }
        public DateTime? end_date { get; set; }

        public string str_complete_date { get; set; }
        public DateTime? complete_date { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string t_priority { get; set; }
        public string portion_name { get; set; }
        public int project_id { get; set; }
        public string project_name { get; set; }
    }

    public class TaskTypes
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public string task_type_id { get; set; }
        public string task_type_name { get; set; }
        public int task_list_id { get; set; }
        public string task_list_name { get; set; }
        public string portion_id { get; set; }
        public string portion_name { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }

        public string user_emp_id { get; set; }
        public int  task_type_sr_id { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }

    }

    public class TaskChat
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int task_id { get; set; }
        public int msg_id { get; set; }
        public string task_msg { get; set; }
   
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string created_by { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
    }

   
}