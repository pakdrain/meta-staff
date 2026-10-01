using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{

    public class RankingModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }

        public int rank_id { get; set; }
        public string rank_name { get; set; }
        public string rank_type { get; set; }
        public int rank_year { get; set; }

        public string app_user { get; set; }
        public string company_id { get; set; }
        public string dep_id { get; set; }

        public int excellent_ideal { get; set; }
        public int excellent_inc { get; set; }

        public int very_good_ideal { get; set; }
        public int very_good_inc { get; set; }

        public int good_ideal { get; set; }
        public int good_inc { get; set; }

        public int need_imp_ideal { get; set; }
        public int need_imp_inc { get; set; }

        public int unsatisfactory_ideal { get; set; }
        public int unsatisfactory_inc { get; set; }

        public DateTime? appr_start_from { get; set; }
        public string str_appr_start_from { get; set; }

        public string lock_type { get; set; }

        public int years_for_promote { get; set; }
        public int years_for_excell_promote { get; set; }

        public DateTime? creation_date { get; set; }
        public string str_creation_date { get; set; }
        public string creation_by { get; set; }

        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string updated_by { get; set; }

        public int appr_from_lock { get; set; }
        public int ranking_lock { get; set; }
        public string ranking_name { get; set; }
        public DateTime? appr_apply_from { get; set; }
        public string str_appr_apply_from { get; set; }
        public int bell_curve { get; set; }
        public string appr_company { get; set; }

        public List<RankingDepModel> ranking_dep { get; set; }
    }

    public class RankingDepModel
    {

        public int hrm_rank_dep_id { get; set; }
        public int rank_id { get; set; }
        public string dep_id { get; set; }
        public string dep_name { get; set; }

        public int total_emp { get; set; }
        public int eligible_emp { get; set; }

        public decimal excellent_count { get; set; }
        public decimal very_good_count { get; set; }
        public decimal good_count { get; set; }
        public decimal needs_improvements_count { get; set; }
        public decimal unsatisfactory_count { get; set; }

        public decimal total_count { get; set; }
        public int apply_bell_curve { get; set; }
       
    }


}