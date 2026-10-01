using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class LoanDetailModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public int loan_id { get; set; }
        public int loan_det_id { get; set; }
        public DateTime? due_date { get; set; }
        public string str_due_date { get; set; }
        public DateTime? loan_pay_date { get; set; }
        public string str_loan_pay_date { get; set; }
        public int loan_amount { get; set; }
        public int loan_payment { get; set; }
        public int loan_balance { get; set; }
        public int installment_no { get; set; }
        public string instal_status { get; set; }
        public string client_code { get; set; }
        public string company { get; set; }
        public string emp_id { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string str_created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string str_updated_date { get; set; }
    }
}