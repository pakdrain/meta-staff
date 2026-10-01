using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class DashboardAttendance  
    {
        public int Operation { get; set; }
        public int present { get; set; }
        public int absent { get; set; }
        public int short_leave { get; set; }
        public int leave { get; set; }
        public int gh_holiday { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }

    }
    public class DashWorkHoursModel
    {
        public int Operation { get; set; }
        public string day_title { get; set; }
        public int day_hours { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
    }
    public class DashLeaveBalModel
    {
        public int Operation { get; set; }
        public int leave_bal { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }

    }
   

}