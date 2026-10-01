using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class RegionModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public int region_id { get; set; }
        public string region_name { get; set; }
       

    }
}