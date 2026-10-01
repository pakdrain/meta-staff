using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class LeaveModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public int sr_id { get; set; }
        public int leave_id { get; set; }
        public int emp_leave_id { get; set; }
        public int available_leaves { get; set; }
        public string org_id { get; set; }
        public DateTime? from_date { get; set; }
        public String str_from_date { get; set; }
        public DateTime? to_date { get; set; }
        public String str_to_date { get; set; }

        public decimal total_days { get; set; }
        public int action { get; set; }
       // public string description { get; set; }
        public string res_message { get; set; }
        public string remark { get; set; }
        public string contact_no { get; set; }
        public string contact_adress { get; set; }
        public string hod_remarks { get; set; }
        public string hr_remarks { get; set; }
        public string rejected_remark { get; set; }
        public string rejected_by { get; set; }
        public string leave_status { get; set; }
        public string leave_duration { get; set; }
        public string leave_doc { get; set; }
        public string extension { get; set; }
        public string leave_url { get; set; }

        // public string leave_type { get; set; }
        // public string status { get; set; }
        public int avail_leaves { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }

        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string user_emp_id { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string leave_type  { get; set; }
        public decimal total_sick_leave { get; set; }
        public string new_leave_id { get; set; }
        public decimal avail_sick_leave { get; set; }
        public decimal total_annual_leave { get; set; }
        public decimal avail_annual_leave { get; set; }
        public decimal total_casual_leave { get; set; }
        public decimal avail_casual_leave { get; set; }

    }
}