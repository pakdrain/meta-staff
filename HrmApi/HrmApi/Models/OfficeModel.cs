using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class OfficeModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public int location_id { get; set; }
        public string office_name { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string phone_number { get; set; }
        public decimal ofc_lat { get; set; }
        public decimal ofc_long { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public int att_radius { get; set; }



    }
}