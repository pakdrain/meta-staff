using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
public class ApprEmpModel
    {
        public int Operation { get; set; }
        public string app_user { get; set; }
        public string company_id { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }

        public int appr_id { get; set; }
        public int sheet_id { get; set; }
        public string emp_id { get; set; }

        public DateTime? doj { get; set; }
        public string str_doj { get; set; }

        public string tenure { get; set; }
        public int allowance { get; set; }
        public int total_weight { get; set; }
        public int achieved_score { get; set; }

        public string desig { get; set; }
        public string dep_id { get; set; }
        public int curr_pay { get; set; }

        public string last_promotion { get; set; }
        public string last_rating { get; set; }
        public decimal? last_inc_amt { get; set; }
        public decimal? last_inc_per { get; set; }

        public string appr_promotion { get; set; }
        public string appr_desig_id { get; set; }
        public string appr_ranking { get; set; }
        public decimal? appr_per { get; set; }
        public decimal? appr_amt { get; set; }
        public int appr_sal_adj { get; set; }
        public decimal? final_sal { get; set; }

        public string hod_remarks { get; set; }
        public string gm_remarks { get; set; }
        public string hr_remarks { get; set; }
        public string ed_remarks { get; set; }

        public DateTime? creation_date { get; set; }
        public string str_creation_date { get; set; }
        public string creation_by { get; set; }

        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string updated_by { get; set; }

        public int appr_year { get; set; }
        public string user_type { get; set; }

        public string status { get; set; }
        public string emp_name { get; set; }
        public string dep_name { get; set; }

        public string hod_app_by { get; set; }
        public DateTime? hod_app_date { get; set; }
        public string str_hod_app_date { get; set; }

        public string hr_app_by { get; set; }
        public DateTime? hr_app_date { get; set; }
        public string str_hr_app_date { get; set; }

        public string gm_app_by { get; set; }
        public DateTime? gm_app_date { get; set; }
        public string str_gm_app_date { get; set; }

        public string ed_app_by { get; set; }
        public DateTime? ed_app_date { get; set; }
        public string str_ed_app_date { get; set; }
        public string initiate_emp { get; set; }
        public string initiate_to { get; set; }
        public string emp_type { get; set; }
        public string area_region { get; set; }
        public int no_of_case { get; set; }
        public string nature { get; set; }
        public int score_achieved { get; set; }
        public int rank_id { get; set; }
        public int gm_read_emp { get; set; }
        public int hod_read_emp { get; set; }
        public int ed_read_emp { get; set; }
        public int inc_field_edit { get; set; }
        public string company_name { get; set; }
        public int promote_allow { get; set; }
    }



    public class ApprSummariesModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }

        public string rank_type { get; set; }
        public int sheet_det_id { get; set; }
        public int bell_curve { get; set; }
        public decimal? ideal_ranking { get; set; }
        public decimal? actual_ranking { get; set; }

        public decimal? revised_sal { get; set; }
        public string emp_type { get; set; }

        public decimal? curr_pay { get; set; }
        public decimal? appr_amt { get; set; }

        public string region { get; set; }
        public string department { get; set; }

        public decimal? excellent_count { get; set; }
        public decimal? very_good_count { get; set; }
        public decimal? good_count { get; set; }
        public decimal? need_improvement_count { get; set; }
        public decimal? unsatisfactory_count { get; set; }

        public decimal? last_inc_amt { get; set; }

        public string app_user { get; set; }
        public string company_id { get; set; }
        public string dep_id { get; set; }
        public int rank_year { get; set; }
        public int sheet_id { get; set; }
    }

}