using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class VehicleModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int vehicle_id { get; set; }
        public string vehicle_no { get; set; }
        public string own_company { get; set; }
        public string first_tag_comp { get; set; }
        public string first_tag_emp { get; set; }
        public string tag_company { get; set; }
        public string tag_person { get; set; }
        public int challan_no { get; set; }
        public string reg_no { get; set; }
        public int chasis_no { get; set; }
        public int engine_no { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public int model_year { get; set; }
        public int owner_no { get; set; }
        public int pso_card_no { get; set; }
        public int fuel_limit { get; set; }
        public DateTime? purchase_date { get; set; }
        public string str_purchase_date { get; set; }
        public string color { get; set; }
        public int registration_fee { get; set; }
        public int number_plate_price { get; set; }
        public int reg_card_fee { get; set; }
        public string token { get; set; }
        public string misc { get; set; }
        public string status { get; set; }
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