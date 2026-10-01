using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class DepartmentModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int id { get; set; }
        public string client_code { get; set; }
        public string dep_id { get; set; }
        public string dep_name { get; set; }
        public string division_id { get; set; }
        public string division_name { get; set; }
        public string status { get; set; }
        public string company { get; set; }
        public string company_name { get; set; }
        public string emp_id { get; set; }
        public int is_active { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int region_id { get; set; }
        public string region { get; set; }
        public int master_acc { get; set; }
        public int rank_year { get; set; }
    }
}