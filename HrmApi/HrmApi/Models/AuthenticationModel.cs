using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class AuthenticationModel
    {
        public int Operation { get; set; }
        public string company { get; set; }
        public string client_code { get; set; }
        public int emp_id { get; set; }
        public string pass { get; set; }
        public string current_pass { get; set; }
        public string new_pass { get; set; }
        public string confirm_new_pass { get; set; }
        public string device_id { get; set; }
        public string login_from { get; set; }

    }
}