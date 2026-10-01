using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
   

  

    public class ProgTrack
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int new_track_id { get; set; }
        public int track_id { get; set; }
        public int proj_id { get; set; }
        public string proj_name { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string status { get; set; }
        public DateTime? start_date { get; set; }
        public string str_start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string str_end_date { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string comp_id { get; set; }
        public string dept_id { get; set; }
        public string remarks { get; set; }
        public string task_type { get; set; }
        public string app_user { get; set; }


    }



}