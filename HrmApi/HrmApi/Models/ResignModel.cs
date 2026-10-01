using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class ResignModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int resig_id { get; set; }
        public string reason { get; set; }
        public string resig_emp_id { get; set; }
        public string resig_status { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
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