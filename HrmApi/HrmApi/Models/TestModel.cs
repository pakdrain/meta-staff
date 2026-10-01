using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class TestModel
    {
        public int Operation { get; set; }
        public int task_id { get; set; }
        public string data { get; set; }    
        public string task_doc { get; set; }    
        public string action { get; set; }    

    }
}