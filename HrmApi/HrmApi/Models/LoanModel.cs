using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class LoanModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int loan_id { get; set; }
        public string loan_type { get; set; }
        public DateTime? loan_start_date { get; set; }
        public string str_loan_start_date { get; set; }
        public int loan_amount { get; set; }
        public int no_of_loan_install { get; set; }
        public int loan_install_amount { get; set; }
        public string loan_status { get; set; }
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
        public string remaining_bal { get; set; }
        public List<LoanDetailModel> LoanDtl { get; set; }
    }

    public class LoanTypeModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int loan_type_id { get; set; }
        public string loan_type_name { get; set; }
        public int no_of_install_allow { get; set; }
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

    }
}