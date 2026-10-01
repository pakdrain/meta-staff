using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
   

  

    public class WpPolicyModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int new_wp_id { get; set; }
        public int wp_id { get; set; }
        public string company { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string wp_doc_extension { get; set; }
        public string policy_type { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string wp_status { get; set; }
        public string emp_id { get; set; }
        public string doc_url { get; set; }
        public string str_from_date { get; set; }
        public string str_to_date { get; set; }
        public string wp_doc { get; set; }
        public string com_name { get; set; }

    }

    

}