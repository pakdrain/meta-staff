using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class ItemModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string uom { get; set; }
        public int is_active { get; set; }
        public decimal weight_in_kg { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public decimal tax { get; set; }
        public decimal discount_per { get; set; }
        public decimal total_amount { get; set; }
        public int qty { get; set; }

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