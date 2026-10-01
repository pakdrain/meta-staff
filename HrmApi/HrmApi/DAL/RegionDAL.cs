using HrmApi.Models;
using HrmApi.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static HrmApi.DAL.SQLHelper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Protobuf;

namespace HrmApi.DAL
{
    public class RegionDAL
    {
        private string _ConnString; //variable for connection string
        public RegionDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Region        

        public async Task<RegionReturn> GetRegions(RegionModel getModel)
        {
            RegionReturn RObj = new RegionReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OfficeModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company,
            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/region";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<RegionApiResponse>(jsonResponse);

                        List<RegionModel> RetLst = new List<RegionModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            RegionModel RetObj = new RegionModel
                            {

                                Operation = item.Operation,
                                company = item.company,
                                emp_id = item.emp_id,
                                region_id = item.region_id,
                                region_name = item.region_name,
                              

                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.RegionDataList = RetLst;
                        }
                    }
                    else
                    {
                        retstatus = 70;
                        retmessage = "API returned an error: " + response.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                retstatus = 70;
                retmessage = "No Data Found. Error: " + ex.Message;
            }

            RObj.Status = retstatus;
            RObj.Message = retmessage;

            return RObj;
        }

        #endregion
    }
}