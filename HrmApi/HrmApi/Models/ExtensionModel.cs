using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class ExtensionModel
    {
        public int Operation { get; set; }
        public int extension_id { get; set; }
        public int extension_no { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string company { get; set; }
        public string extension { get; set; }
        public string ip_phone { get; set; }
        public string extension_type { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }

    }
}