using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class ClientModel
    {
        public int Operation { get; set; }
        public int client_id {get; set; }
        public string client_code { get; set; }
        public string client_name { get; set; }
        public int is_active { get; set; }
    }
}