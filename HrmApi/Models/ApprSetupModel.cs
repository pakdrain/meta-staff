using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class ApprSetupModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }

        public int appr_setup_id { get; set; }
        public int appr_year { get; set; }
        public string app_user { get; set; }
        public string company_id { get; set; }
        public string dep_id { get; set; }

        public DateTime? appr_start_from { get; set; }
        public string str_appr_start_from { get; set; }

        public DateTime? appr_apply_from { get; set; }
        public string str_appr_apply_from { get; set; }

        public int years_for_promote { get; set; }
        public int years_for_excell_promote { get; set; }

        public DateTime? creation_date { get; set; }
        public string str_creation_date { get; set; }
        public string creation_by { get; set; }

        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public string updated_by { get; set; }
    }



}