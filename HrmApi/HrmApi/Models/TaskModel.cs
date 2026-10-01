using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class TaskModel
    {
        public int Operation { get; set; }
        public string action { get; set; }
        public int sr_id { get; set; }
        public int task_id { get; set; }
        public int ticket_no { get; set; }
        public string from_com { get; set; }
        public string from_com_to_show { get; set; }
        public string from_dep { get; set; }
        public string task_from { get; set; }
        public DateTime? expected_date { get; set; }
        public string str_expected_date { get; set; }
        public DateTime? closed_date { get; set; }
        public string str_closed_date { get; set; }
        public string assign_from { get; set; }
        public string assign_from_to_show { get; set; }
        public DateTime? assign_date { get; set; }
        public string str_assign_date { get; set; }
        public string to_com { get; set; }
        public string to_dep { get; set; }
        public string task_to_to_show { get; set; }
        public string task_to { get; set; }
        public DateTime? compelete_date { get; set; }
        public string str_compelete_date { get; set; }
        public string task_status { get; set; }
        public string task_img { get; set; }
        public string task_doc { get; set; }
        public string doc_url { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public DateTime? rejected_date { get; set; }
        public string str_rejected_date { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string task_title { get; set; }
        public string task_desc { get; set; }
        public int task_progress { get; set; }
        public int task_list_id { get; set; }
        public string task_list_id_to_show { get; set; }
        public string rejected_remark { get; set; }
        public string rejected_by { get; set; }

        public string mob_no { get; set; }
  
        public string extension { get; set; }
        public string task_dep_to_show { get; set; }
        public string task_priority { get; set; }
        //public string Task_Files { get; set; }
        public HttpPostedFileBase Task_Files { get; set; }
        public byte[] FileContent { get;  set; }
        public string FileName { get;  set; }
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

    public class TaskTypeAndLists
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int task_type_id { get; set; }
        public string task_type_name { get; set; }
        public int task_list_id { get; set; }
        public string task_list_name { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
    }
}