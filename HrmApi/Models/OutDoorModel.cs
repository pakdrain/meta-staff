using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class OutDoorModel
    {
        public int Operation { get; set; }
        public string action { get; set; }
        public int od_id { get; set; }
        public string emp_id { get; set; }
        public string user_emp_id { get; set; }
        public DateTime? from_date { get; set; }
        public string str_from_date { get; set; }
        public DateTime? to_date { get; set; }
        public string str_to_date { get; set; }
        public string reason { get; set; }
        public string remark_by_hod { get; set; }
        public string remark_by_hr { get; set; }
        public string status { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string hod_app_by { get; set; }
        public string hr_app_by { get; set; }
        public DateTime? hod_app_date { get; set; }
        public DateTime? hr_app_date { get; set; }
        public string rej_by { get; set; }
        public string rej_remarks { get; set; }
        public DateTime? rej_date { get; set; }
        public string org_id { get; set; }
        public string company { get; set; }
        public string client_code { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string duty_duration { get; set; }
        public int role_id { get; set; }
        public string com_id { get; set; }
        public string com_name { get; set; }
        public string special_director { get; set; }

    }
}