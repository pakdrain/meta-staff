using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{




    public class HcModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int hc_id { get; set; }
        public string notice_type { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string doc_one_extension { get; set; }
        public string doc_two_extension { get; set; }
        public string doc_three_extension { get; set; }
        public string status { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string com_name { get; set; }
        public string doc_one_url { get; set; }
        public string doc_two_url { get; set; }
        public string doc_three_url { get; set; }
        public string hc_no { get; set; }
        public string str_from_date { get; set; }
        public string str_to_date { get; set; }
        public string doc_one { get; set; }
        public string doc_two { get; set; }
        public string doc_three { get; set; }
        public string dep_name { get; set; }
        public int new_hc_id { get; set; }

    }




}