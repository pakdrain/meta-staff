using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HrmApi.DAL.TestDAL;

namespace HrmApi.Models
{
    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
    public class CompanyApiResponse
    {
        [JsonProperty("items")]
        public List<CompaniesModel> Items { get; set; }

        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class EmployeeApiResponse
    {
        [JsonProperty("items")]
        public List<EmployeeModel> Items { get; set; }

        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    

    public class AttApiResponse
    {
        [JsonProperty("items")]
        public List<AttendanceModel> Items { get; set; }


    }
    public class ItemApiResponse
    {
        [JsonProperty("items")]
        public List<ItemModel> Items { get; set; }


    }
    public class LeaveApiResponse
    {
        [JsonProperty("items")]
        public List<LeaveModel> Items { get; set; }
    }

    public class LoansApiResponse
    {
        [JsonProperty("items")]
        public List<LoansModel> Items { get; set; }
       
    }

    public class BonusDeductionApiResponse
    {
        [JsonProperty("res_status")]
        public int ResStatus { get; set; }

        [JsonProperty("res_message")]
        public string ResMessage { get; set; }

        [JsonProperty("new_id")]
        public int NewId { get; set; }

        [JsonProperty("items")]
        public List<DeductionMaster> Items { get; set; }

        [JsonProperty("deduction_data")]
        public List<DeductionMaster> DeductionMaster { get; set; }
    }

    public class WpPolicyApiResponse
    {
        [JsonProperty("items")]
        public List<WpPolicyModel> Items { get; set; }

    }

    public class ProjectsApiResponse
    {
        [JsonProperty("items")]
        public List<ProjectModel> Items { get; set; }

    }

    public class ProgTrackApiResponse
    {
        [JsonProperty("items")]
        public List<ProgTrack> Items { get; set; }

    }

    public class HcApiResponse
    {
        [JsonProperty("items")]
        public List<HcModel> Items { get; set; }

    }

    public class SalSlipApiResponse
    {
        [JsonProperty("items")]
        public List<SalSlipModel> Items { get; set; }

    }

    public class EvalFormApiResponse
    {
        [JsonProperty("items")]
        public List<EvalFormModel> Items { get; set; }

    }

    public class ApprEmpApiResponse
    {
        [JsonProperty("items")]
        public List<ApprEmpModel> Items { get; set; }

    }

    public class ApprSummariesApiResponse
    {
        [JsonProperty("items")]
        public List<ApprSummariesModel> Items { get; set; }

    }

    public class ApprSetupApiResponse
    {
        [JsonProperty("res_status")]
        public int ResStatus { get; set; }

        [JsonProperty("res_message")]
        public string ResMessage { get; set; }

        [JsonProperty("items")]
        public List<ApprSetupModel> Items { get; set; }

    }

    public class RankingApiResponse
    {
        [JsonProperty("res_status")]
        public int ResStatus { get; set; }

        [JsonProperty("res_message")]
        public string ResMessage { get; set; }

        [JsonProperty("items")]
        public List<RankingModel> Items { get; set; }

    }

    public class SheetApiResponse
    {
        [JsonProperty("res_status")]
        public int ResStatus { get; set; }

        [JsonProperty("res_message")]
        public string ResMessage { get; set; }

        [JsonProperty("items")]
        public List<SheetModel> Items { get; set; }

    }

    public class SheetDetApiResponse
    {
        [JsonProperty("res_status")]
        public int ResStatus { get; set; }

        [JsonProperty("res_message")]
        public string ResMessage { get; set; }

        [JsonProperty("items")]
        public List<SheetDetModel> Items { get; set; }

    }

    public class OwnedVehicleApiResponse
    {
        [JsonProperty("items")]
        public List<OwnedVehicle> Items { get; set; }

    }

    public class TaskTypessApiResponse
    {
        [JsonProperty("items")]
        public List<TaskTypes> Items { get; set; }

    }

    public class TasksModelApiResponse
    {
        [JsonProperty("items")]
        public List<TasksModel> Items { get; set; }

    }

    public class VehiclesApiResponse
    {
        [JsonProperty("items")]
        public List<VehiclesModel> Items { get; set; }

    }

    public class VehiclesDetApiResponse
    {
        [JsonProperty("items")]
        public List<VehiclesDet> Items { get; set; }

    }

    public class ComplainApiResponse
    {
        [JsonProperty("items")]
        public List<ComplainModel> Items { get; set; }

    }

    public class NotifApiResponse
    {
        [JsonProperty("items")]
        public List<NotifModel> Items { get; set; }

    }

    public class ClearanceApiResponse
    {
        [JsonProperty("items")]
        public List<ClearanceModel> Items { get; set; }
    }

    public class OfficeApiResponse
    {
        [JsonProperty("items")]
        public List<OfficeModel> Items { get; set; }
    }

    public class RegionApiResponse
    {
        [JsonProperty("items")]
        public List<RegionModel> Items { get; set; }
    }

    public class OrderApiResponse
    {
        [JsonProperty("items")]
        public List<OrderMastModel> Items { get; set; }
    }

    public class DepartmentApiResponse
    {
        [JsonProperty("items")]
        public List<DepartmentModel> Items { get; set; }

    }

    public class OutdoorApiResponse
    {
        [JsonProperty("items")]
        public List<OutDoorModel> Items { get; set; }


    }

    public class LoanSummaryApiResponse
    {
        [JsonProperty("items")]
        public List<LoanSummary> Items { get; set; }


    }

}