using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{

    public class SheetModel
    {
        public int Operation { get; set; }

        public string app_user { get; set; }

        public int sheet_id { get; set; }

        public int rank_id { get; set; }

        public string rank_name { get; set; }

        public string region { get; set; }

        public string dep_id { get; set; }

        public string dep_name { get; set; }

        public string hod { get; set; }

        public string hod_name { get; set; }

        public int gm_chk { get; set; }

        public string created_by { get; set; }

        public DateTime? created_date { get; set; }

        public string str_created_date { get; set; }

        public string updated_by { get; set; }

        public DateTime? updated_date { get; set; }

        public string str_updated_date { get; set; }

        public string status { get; set; }

        public int rank_year { get; set; }

        public int bell_curve { get; set; }

        public string user_type { get; set; }

        public string appr_company { get; set; }

        public string gm_emp_id { get; set; }

        public string director_emp_id { get; set; }

        public List<SheetDetModel> SheetDet { get; set; }
    }

    public class SheetDetModel
    {
        public int Operation { get; set; }
        public int sheet_id { get; set; }
        public int sheet_det_id { get; set; }
        public int rank_id { get; set; }
        public string sheet_name { get; set; }
        public Decimal? excellent_count { get; set; }
        public Decimal? very_good_count { get; set; }
        public Decimal? good_count { get; set; }
        public Decimal? need_imp_count { get; set; }
        public Decimal? unsatisfactory_count { get; set; }
        public Decimal? excellent_actual { get; set; }
        public Decimal? very_good_actual { get; set; }
        public Decimal? good_actual { get; set; }
        public Decimal? need_imp_actual { get; set; }
        public Decimal? unsatisfactory_actual { get; set; }
        public int inc_field_edit { get; set; }
        public int total_emp_count { get; set; }
        public int bell_curve_chk { get; set; }
        public string emp_type { get; set; }
        public string status { get; set; }
        public string rank_name { get; set; }

        public List<SheetEmpModel> SheetEmp { get; set; }
    }

    public class SheetEmpModel
    {
        public int sheet_det_id { get; set; }
        public int sheet_emp_id { get; set; }
        public string emp_id { get; set; }
        public string company_id { get; set; }
        public string emp_name { get; set; }
        public string designation_name { get; set; }
        public string dep_name { get; set; }
        public string com_name { get; set; }
        public int curr_salary { get; set; }
        public string nature { get; set; }
        public string area_region { get; set; }
        public int no_of_case { get; set; }
    }

    //public class SheetModel
    //{
    //    [JsonProperty("Operation")]
    //    public int Operation { get; set; }

    //    [JsonProperty("res_status")]
    //    public int res_status { get; set; }

    //    [JsonProperty("res_message")]
    //    public string res_message { get; set; }

    //    [JsonProperty("sheet_id")]
    //    public int sheet_id { get; set; }

    //    [JsonProperty("region")]
    //    public string region { get; set; }

    //    [JsonProperty("dep_id")]
    //    public string dep_id { get; set; }

    //    [JsonProperty("hod")]
    //    public string hod { get; set; }

    //    [JsonProperty("gm_chk")]
    //    public int gm_chk { get; set; }

    //    [JsonProperty("created_by")]
    //    public string created_by { get; set; }

    //    [JsonProperty("created_date")]
    //    public DateTime? created_date { get; set; }

    //    [JsonProperty("str_created_date")]
    //    public string str_created_date { get; set; }

    //    [JsonProperty("updated_by")]
    //    public string updated_by { get; set; }

    //    [JsonProperty("updated_date")]
    //    public DateTime? updated_date { get; set; }

    //    [JsonProperty("str_updated_date")]
    //    public string str_updated_date { get; set; }

    //    [JsonProperty("rank_year")]
    //    public int rank_year { get; set; }

    //    [JsonProperty("SheetDet")]
    //    public List<SheetDetModel> SheetDet { get; set; }
    //}

    //public class SheetDetModel
    //{
    //    [JsonProperty("sheet_id")]
    //    public int sheet_id { get; set; }

    //    [JsonProperty("sheet_det_id")]
    //    public int sheet_det_id { get; set; }

    //    [JsonProperty("rank_id")]
    //    public int rank_id { get; set; }

    //    [JsonProperty("sheet_name")]
    //    public string sheet_name { get; set; }

    //    [JsonProperty("excellent_count")]
    //    public int excellent_count { get; set; }

    //    [JsonProperty("very_good_count")]
    //    public int very_good_count { get; set; }

    //    [JsonProperty("good_count")]
    //    public int good_count { get; set; }

    //    [JsonProperty("need_imp_count")]
    //    public int need_imp_count { get; set; }

    //    [JsonProperty("unsatisfactory_count")]
    //    public int unsatisfactory_count { get; set; }

    //    [JsonProperty("total_emp_count")]
    //    public int total_emp_count { get; set; }

    //    [JsonProperty("emp_type")]
    //    public string emp_type { get; set; }

    //    [JsonProperty("SheetEmp")]
    //    public List<SheetEmpModel> SheetEmp { get; set; }
    //}

    //public class SheetEmpModel
    //{
    //    [JsonProperty("sheet_det_id")]
    //    public int sheet_det_id { get; set; }

    //    [JsonProperty("sheet_emp_id")]
    //    public int sheet_emp_id { get; set; }

    //    [JsonProperty("emp_id")]
    //    public string emp_id { get; set; }

    //    [JsonProperty("emp_name")]
    //    public string emp_name { get; set; }

    //    [JsonProperty("designation_name")]
    //    public string designation_name { get; set; }

    //    [JsonProperty("dep_name")]
    //    public string dep_name { get; set; }

    //    [JsonProperty("com_name")]
    //    public string com_name { get; set; }

    //    [JsonProperty("curr_salary")]
    //    public int curr_salary { get; set; }
    //}


}