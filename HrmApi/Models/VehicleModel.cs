using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class OwnedVehicle
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public int desig_id { get; set; }
        public string desig_name { get; set; }
        public int dep_id { get; set; }
        public string dep_name { get; set; }
        public DateTime? doj { get; set; }
        public string str_doj { get; set; }
        public string employment_type { get; set; }
        public int division_id { get; set; }
        public string current_function_type { get; set; }
        public int buy_pack_elig { get; set; }
        public string vehicle_type { get; set; }
        public string vehicle_category { get; set; }
        public string reg_no { get; set; }
        public DateTime? registration_date { get; set; }
        public string str_registration_date { get; set; }
        public string owner_company { get; set; }
        public string maker { get; set; }
        public int yom { get; set; }
        public string engine_no { get; set; }
        public string chassis { get; set; }
        public decimal vehicle_cost { get; set; }
        public DateTime? hand_over_date { get; set; }
        public string str_hand_over_date { get; set; }
        public int desig_eligible { get; set; }
        public int min_sal_req { get; set; }
        public int app_in_two_mon { get; set; }
        public int tenure_limit_comp { get; set; }
    }

    public class VehiclesModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int new_vehicle_id { get; set; }
        public int hrm_vehicle_id { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string str_from_date { get; set; }
        public string str_to_date { get; set; }
        public string emp_id { get; set; }
        public string vehicle_type { get; set; }
        public string vehicle_category { get; set; }
        public string reg_no { get; set; }
        public DateTime? registration_date { get; set; }
        public string str_registration_date { get; set; }
        public string owner_company { get; set; }
        public string maker { get; set; }
        public int yom { get; set; }
        public string engine_no { get; set; }
        public string chassis { get; set; }
        public decimal vehicle_cost { get; set; }
        public DateTime? hand_over_date { get; set; }
        public string str_hand_over_date { get; set; }
        public int desig_eligible { get; set; }
        public int min_sal_req { get; set; }
        public int app_in_two_mon { get; set; }
        public int tenure_limit_comp { get; set; }
        public string status { get; set; }

        public string hod_app_by { get; set; }
        public string gm_app_by { get; set; }
        public string hr_first_app_by { get; set; }
        public string hr_second_app_by { get; set; }
        public string admin_app_by { get; set; }
        public string ed_app_by { get; set; }
        public string close_by { get; set; }
        public string rej_by { get; set; }

        public string hod_remarks { get; set; }
        public string gm_remarks { get; set; }
        public string hr_first_remarks { get; set; }
        public string hr_second_remarks { get; set; }
        public string admin_remarks { get; set; }
        public string ed_remarks { get; set; }
        public string close_remarks { get; set; }
        public string rej_remarks { get; set; }

        public DateTime? hod_app_date { get; set; }
        public string str_hod_app_date { get; set; }
        public DateTime? gm_app_date { get; set; }
        public string str_gm_app_date { get; set; }
        public DateTime? hr_first_app_date { get; set; }
        public string str_hr_first_app_date { get; set; }
        public DateTime? hr_second_app_date { get; set; }
        public string str_hr_second_app_date { get; set; }
        public DateTime? admin_app_date { get; set; }
        public string str_admin_app_date { get; set; }
        public DateTime? ed_app_date { get; set; }
        public string str_ed_app_date { get; set; }
        public DateTime? close_date { get; set; }
        public string str_close_date { get; set; }
        public DateTime? rej_date { get; set; }
        public string str_rej_date { get; set; }

        public string action_page { get; set; }
        public string created_by { get; set; }
        public DateTime? creation_date { get; set; }
        public string str_creation_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_update_date { get; set; }
        public string str_last_update_date { get; set; }

        public string emp_name { get; set; }
        public int desig_id { get; set; }
        public string desig_name { get; set; }
        public int dep_id { get; set; }
        public string dep_name { get; set; }
        public DateTime? doj { get; set; }
        public string str_doj { get; set; }
        public string employment_type { get; set; }
        public int division_id { get; set; }
        public string current_function_type { get; set; }
        public int buy_pack_elig { get; set; }
        public decimal buy_back_amt { get; set; }
        public decimal market_value { get; set; }
        public decimal reg_amount { get; set; }
        public decimal first_year_inss_amt { get; set; }
        public int no_of_installment { get; set; }
        public decimal per_mon_installment { get; set; }
        public List<VehiclesDet> Installments { get; set; }
        public List<YearlyInss> yearly_inss { get; set; }
    }


    public class VehiclesDet
    {
        public int Operation { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public int det_id { get; set; }
        public int hrm_vehicle_id { get; set; }
        public int instal_no { get; set; }
        public DateTime? instal_date { get; set; }
        public string str_instal_date { get; set; }
        public decimal instal_amount { get; set; }
        public decimal inss_amount { get; set; }
        public decimal reg_amount { get; set; }
        public decimal total_amount { get; set; }
        public int no_of_installment { get; set; }
        public int market_value { get; set; }
    }

    public class YearlyInss
    {
        public string year_no { get; set; }
        public string fiscal_year { get; set; }
        public int total_month { get; set; }
        public int total_amount { get; set; }
     
    }

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