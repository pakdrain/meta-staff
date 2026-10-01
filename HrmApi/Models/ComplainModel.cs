using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{


    public class ComplainModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int new_cmplnt_id { get; set; }
        public int cmplt_id { get; set; }
        public int cmplt_no { get; set; }
        public string subject { get; set; }
        public string company { get; set; }
        public string org_id { get; set; }
        public string description { get; set; }
        public string doc_extension { get; set; }
        public string status { get; set; }
        public string hod_app_by { get; set; }
        public string hr_app_by { get; set; }
        public string rej_by { get; set; }
        public string action_page { get; set; }
        public string emp_id { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string doc_url { get; set; }
        public string emp_name { get; set; }
        public string com_name { get; set; }
        public string dep_name { get; set; }
        public DateTime? hod_app_date { get; set; }
        public string str_hod_app_date { get; set; }
        public DateTime? hr_app_date { get; set; }
        public string str_hr_app_date { get; set; }
        public string str_from_date { get; set; }
        public string str_to_date { get; set; }
        public string document { get; set; }
        public string hod_remarks { get; set; }
        public string hr_remarks { get; set; }
        public string desig_name { get; set; }
        public string hr_doc_one_ext { get; set; }
        public string hr_doc_two_ext { get; set; }
        public string hr_doc_three_ext { get; set; }
        public string hr_doc_one_url { get; set; }
        public string hr_doc_two_url { get; set; }
        public string hr_doc_three_url { get; set; }
        public string hr_doc_one { get; set; }
        public string hr_doc_two { get; set; }
        public string hr_doc_three { get; set; }
    }

}