using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class AttendanceModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int sr_id { get; set; }
        public int att_id { get; set; }
        public int want { get; set; }
        public DateTime? att_date { get; set; }
        public string str_att_date { get; set; }
        public DateTime? check_in_time { get; set; }
        public string str_check_in_Date { get; set; }
        public string str_check_in_time { get; set; }
        public DateTime? check_out_time { get; set; }
        public string str_check_out_Date { get; set; }
        public string str_check_out_time { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id_to_show { get; set; }
        public string emp_id { get; set; }
        public string dayy { get; set; }
        public string emp_name { get; set; }
        public string emp_designation { get; set; }
        public string shift_type { get; set; }
        public string working_hours { get; set; }
        public string total_hours_work { get; set; }
        public string att_status { get; set; }
        public string month_year { get; set; }
        public string att_month { get; set; }
        public int present { get; set; }
        public int absent { get; set; }
        public int late_in { get; set; }
        public int early_out { get; set; }
        public int awaiting { get; set; }


    }


}