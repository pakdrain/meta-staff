using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
   

    public class LoanSummary
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public string emp_id { get; set; }
        public string company { get; set; }
        public string emp_no { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string doj  { get; set; }
        public decimal current_pay { get; set; }
        public decimal prev_amount { get; set; }
        public string prev_status { get; set; }
        public DateTime? prev_instal_date { get; set; }
        public string str_prev_instal_date { get; set; }
        public decimal balance { get; set; }
        public int joining_diff { get; set; }
        public int year_diff { get; set; }
        public int third_party { get; set; }
        public decimal loan_limit { get; set; }
    }

    public class LoansModel
    {
        public int Operation { get; set; }
        public int res_status { get; set; }
        public string res_message { get; set; }
        public string action_page { get; set; }
        public string company { get; set; }
        public string str_from_date { get; set; }
        public string str_to_date { get; set; }
        public string new_loan_id { get; set; }
        public int loan_id { get; set; }
        public string emp_id { get; set; }
        public string user_emp_id { get; set; }
        public DateTime? loan_start_date { get; set; }
        public string str_loan_start_date { get; set; }
        public int loan_amount { get; set; }
        public int per_month_instalment { get; set; }
        public int created_by { get; set; }
        public DateTime? creation_date { get; set; }
        public string str_creation_date { get; set; }
        public int last_updated_by { get; set; }
        public DateTime? last_update_date { get; set; }
        public string str_last_update_date { get; set; }
        public int approved_by { get; set; }
        public DateTime? approved_date { get; set; }
        public string str_approved_date { get; set; }
        public string loan_status { get; set; }
        public int loan_no { get; set; }
        public string deduction_type { get; set; }
        public int markup { get; set; }
        public int exemption_amount { get; set; }
        public string company_type { get; set; }
        public string branch_id { get; set; }
        public int close_by { get; set; }
        public DateTime? close_date { get; set; }
        public string str_close_date { get; set; }
        public string org_id { get; set; }
        public int total_loan_installments { get; set; }
        public string loan_insurance { get; set; }
        public string loan_type { get; set; }
        public string id_doc { get; set; }
        public string id_extension { get; set; }
        public string b_c_doc { get; set; }
        public string b_c_extension { get; set; }
        public string other_doc { get; set; }
        public string other_extension { get; set; }
        public string emp_id_to_show { get; set; }
        public string emp_name { get; set; }
        public string department_name { get; set; }
        public string company_name { get; set; }
        public string designation { get; set; }
        public string remarks { get; set; }
        public string hod_app_by { get; set; }
        public string hod_remarks { get; set; }
        public string hr_app_by { get; set; }
        public string hr_remarks { get; set; }
        public string gm_app_by { get; set; }
        public string gm_remarks { get; set; }
        public string ed_app_by { get; set; }
        public string ed_remarks { get; set; }
        public string rej_by { get; set; }
        public int deduct_from_bonus { get; set; }
        public int document_clear { get; set; }
        public int id_doc_clear { get; set; }
        public int b_c_doc_clear { get; set; }
        public string doj { get; set; }
        public int current_pay { get; set; }
        public string com_id { get; set; }
        public string com_name { get; set; }


        public List<LoanSchedualeModel> SchedData { get; set; }

    }

    public class LoanSchedualeModel
    {
        public int Operation { get; set; }
        public int loan_scheduale_id { get; set; }
        public int loan_id { get; set; }
        public string period_id { get; set; }
        public DateTime? installment_date { get; set; }
        public string str_installment_date { get; set; }
        public int amount { get; set; }
        public string posted { get; set; }
        public int created_by { get; set; }
        public DateTime? creation_date { get; set; }
        public string str_creation_date { get; set; }
        public int last_updated_by { get; set; }
        public DateTime? last_update_date { get; set; }
        public string str_last_update_date { get; set; }
        public int markup_amount { get; set; }
        public int installment_amount { get; set; }
        public int balance { get; set; }
        public string deducted { get; set; }
        public string in_cash { get; set; }
        public string company_type { get; set; }

    }


    public class BonusDeduction
    {
        public DeductionMaster master { get; set; }

        public List<DeductionMaster> DedmasterData { get; set; }

        public List<LoanSchedualeModel> items { get; set; }
    }

    public class DeductionMaster
    {
        public int Operation { get; set; }

        public string app_user { get; set; }

        public string ded_year { get; set; }

        public string ded_no { get; set; }

        public DateTime? ded_date { get; set; }

        public string str_ded_date { get; set; }

        public string doc_ext { get; set; }

        public string doc { get; set; }

        public string created_by { get; set; }

        public DateTime? created_date { get; set; }

        public string str_created_date { get; set; }
    }
}