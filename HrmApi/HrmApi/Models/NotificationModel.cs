using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class NotificationModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int notif_id { get; set; }
        public string notif_title { get; set; }
        public string notif_desc { get; set; }
        public string notif_type { get; set; }
        public string notif_status { get; set; }
        public string for_comp { get; set; }
        public string for_emp { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
    }
}