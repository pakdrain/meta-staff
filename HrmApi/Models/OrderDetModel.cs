using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class OrderDetModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int order_id { get; set; }
        public int new_order_id { get; set; }
        public int order_det_id { get; set; }
        public int item_id { get; set; }
        public string item_desc { get; set; }

        public decimal qty { get; set; }
        public decimal weight_in_kg { get; set; }
        public decimal total_kgs { get; set; }
        public decimal item_rate { get; set; }
        public decimal amount { get; set; }
        public decimal discount_percentage { get; set; }
        public decimal discount_amount { get; set; }
        public decimal total_amount { get; set; }
        public decimal tax_amount { get; set; }
       

    }
}