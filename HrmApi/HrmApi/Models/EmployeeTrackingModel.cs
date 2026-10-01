using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class EmployeeTrackingModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int tracking_id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public DateTime? track_time { get; set; }
        public string str_track_time { get; set; }
        public string emp_id { get; set; }
    }
}