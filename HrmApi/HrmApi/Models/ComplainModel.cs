using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class ComplainModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int cmplnt_id { get; set; }
        public string complnt_no { get; set; }
        public string complainee { get; set; }
        public string cmplnt_name { get; set; }
        public string complainee_dep { get; set; }
        public string complainee_designation { get; set; }
        public string overview { get; set; }
        public string resolution_step { get; set; }
        public string actions { get; set; }
        public string cmplnt_sub { get; set; }
        public string description { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string department { get; set; }
        public string act_first_head { get; set; }
        public string act_first_desc { get; set; }
        public string act_second_head { get; set; }
        public string act_second_desc { get; set; }
        public string act_third_head { get; set; }
        public string act_third_desc { get; set; }
        public string complain_from_to_show { get; set; }
        public string complain_from_dep_to_show { get; set; }
        public string complain_to_to_show { get; set; }
        public string complain_to_dep_to_show { get; set; }

    }
}