using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class OrderMastModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int sr_id { get; set; }
        public long order_id { get; set; }
        public long order_no { get; set; }
        public DateTime? order_date { get; set; }
        public string str_order_date { get; set; }
        public string order_status { get; set; }
        public DateTime? delivery_date { get; set; }
        public string str_delivery_date { get; set; }
        public int total_qty { get; set; }
        public decimal total_kgs { get; set; }
        public decimal discount { get; set; }
        public decimal tax { get; set; }
        public decimal total_amount { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public List<OrderDetModel> OrderDetail { get; set; }
    }
}