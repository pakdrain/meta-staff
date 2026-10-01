using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class EvalFormModel
    {
        public int Operation { get; set; }
        public string app_user { get; set; }
        public string dep_id { get; set; }
        public string company_id { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }

        public int eval_id { get; set; }
        public int appr_id { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public string dep_name { get; set; }

        public decimal gross_salary { get; set; }
        public string service_tenure { get; set; }

        public string area_unit { get; set; }
        public string successor_to { get; set; }
        public string reporting_to { get; set; }

        public string edu_rel_certi { get; set; }
        public string edu_rel_job { get; set; }

        public int core_skill_ability { get; set; }
        public int take_interest { get; set; }
        public int problem_solving { get; set; }
        public int accuracy { get; set; }
        public int adherence { get; set; }
        public int take_intiative { get; set; }
        public int meets_deadlines { get; set; }
        public int punctuality { get; set; }
        public int attendance { get; set; }
        public int dress_code { get; set; }
        public int respond_contribution { get; set; }
        public int respond_suggestion { get; set; }
        public int gives_and_seeks { get; set; }
        public int adapts_well { get; set; }
        public int synchronization { get; set; }
        public int accept_challange { get; set; }
        public int innovative_thinking { get; set; }
        public int motivation_emotional { get; set; }
        public int grand_total { get; set; }

        public string this_year_biggest_impact { get; set; }
        public string specific_area_or_skill { get; set; }
        public string overall_assessment { get; set; }
        public string remark_if_any { get; set; }

        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
        public DateTime? doj { get; set; }
        public string str_doj { get; set; }


        public int appr_year { get; set; }
    }



}