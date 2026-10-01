using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
   

  

    public class ProjectModel
    {
        public int Operation {  get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int new_proj_id { get; set; }

        public int proj_id { get; set; }
        public string proj_name { get; set; }

        public DateTime? plan_s_date { get; set; }
        public string str_plan_s_date { get; set; }

        public DateTime? plan_e_date { get; set; }
        public string str_plan_e_date { get; set; }

        public DateTime? s_date { get; set; }
        public string str_s_date { get; set; }

        public DateTime? e_date { get; set; }
        public string str_e_date { get; set; }

        public string company { get; set; }
        public string dep_id { get; set; }

        public string comp_name { get; set; }
        public string dep_name { get; set; }

        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string created_by { get; set; }
        public string created_by_name { get; set; }

        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string updated_by { get; set; }
        public string updated_by_name { get; set; }
        public string app_user { get; set; }


    }



}