using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class CompaniesModel
    {
        public int Operation { get; set; }
        public int sr_id { get; set; }
        public string sr_name { get; set; }
        public string client_code { get; set; }
        public string company_id_to_show { get; set; }
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string emp_id { get; set; }
        public int is_active { get; set; }
        public int master_acc { get; set; }

        public string api_url { get; set; }
     

        public int res_status { get; set; }

        public string res_message { get; set; }

        public List<SeekingCompanies> SeekingComp { get; set; }



    }
    public class SeekingCompanies
    {
        public string want { get; set; }
        public int id { get; set; }
        public string emp_id { get; set; }
        public string company_id { get; set; }

    }
}

