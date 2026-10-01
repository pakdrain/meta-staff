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

    public class ComplainApiResponse
    {
        [JsonProperty("items")]
        public List<ComplainModel> Items { get; set; }

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

}