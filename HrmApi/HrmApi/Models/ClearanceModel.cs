using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class ClearanceModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int clear_id { get; set; }
        public int clear_no { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string clearance_emp { get; set; }             
        public string clearance_status { get; set; }
        public int dep_id { get; set; }
        public string dep_id_s { get; set; }
        public string dep_name { get; set; }
        public DateTime? date_of_res { get; set; }
        public string str_date_of_res { get; set; }
        public DateTime? last_working_day { get; set; }
        public string str_last_working_day { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string hod_app_by { get; set; }
        public DateTime? hod_app_date { get; set; }
        public string str_hod_app_date { get; set; }
        public string dep_app_by { get; set; }
        public DateTime? dep_app_date { get; set; }
        public string str_dep_app_date { get; set; }
        public string app_by { get; set; }
        public DateTime? app_date { get; set; }
        public string str_app_date { get; set; }
        public string rej_by { get; set; }
        public DateTime? rej_date { get; set; }
        public string str_rej_date { get; set; }
        public string hod_remarks { get; set; }
        public string hr_remarks { get; set; }
        public string remarks { get; set; }
        public string domain { get; set; }
        public int dep_clear_id { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string str_doj { get; set; }
        public string str_dor { get; set; }
        public string hr_extension { get; set; }
        public string hod_extension { get; set; }
        public string dep_extension { get; set; }
        public string hr_doc { get; set; }
        public string hod_doc { get; set; }
        public string dep_doc { get; set; }
        public string ip_adrress { get; set; }

    }
}