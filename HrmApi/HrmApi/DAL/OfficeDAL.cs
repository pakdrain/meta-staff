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
    public class OfficeDAL
    {
        private string _ConnString; //variable for connection string
        public OfficeDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Office


        public async Task<OfficeReturn> AUDOffices(OfficeModel getModel)
        {
            OfficeReturn RObj = new OfficeReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
        
            int operation = getModel.Operation;
            var attendanceRequest = new OfficeModel
            {
                Operation = getModel.Operation,
                company = getModel.company,
                emp_id = getModel.emp_id,
                location_id = getModel.location_id,
                office_name = getModel.office_name,
                address_line1 = getModel.address_line1,
                address_line2 = getModel.address_line2,
                city = getModel.city,
                state = getModel.state,
                postal_code = getModel.postal_code,
                country = getModel.country,
                phone_number = getModel.phone_number,
                ofc_lat = getModel.ofc_lat,
                ofc_long = getModel.ofc_long,
                att_radius = getModel.att_radius,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                updated_by = getModel.updated_by,
                created_by = getModel.created_by,
            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/office";

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
                        var apiResponse = JsonConvert.DeserializeObject<OfficeApiResponse>(jsonResponse);

                        List<OfficeModel> RetLst = new List<OfficeModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

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
            if (operation == 3 && retstatus == 2)
            {
                var parm = new SqlParameter[5];
                // Setting parameters value from the user input (getModel)

                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 20 };             
                parm[1] = new SqlParameter("@office_id", SqlDbType.Int) { Value = getModel.location_id };
                parm[2] = new SqlParameter("@ofc_lat", SqlDbType.VarChar) { Value = getModel.ofc_lat };
                parm[3] = new SqlParameter("@ofc_lon", SqlDbType.VarChar) { Value = getModel.ofc_long };
                parm[4] = new SqlParameter("@att_radius", SqlDbType.Int) { Value = getModel.att_radius };           

                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);

            }
            return RObj;
        }


        public async Task<OfficeReturn> GetOffices(OfficeModel getModel)
        {
            OfficeReturn RObj = new OfficeReturn();
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

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/office";


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
                        var apiResponse = JsonConvert.DeserializeObject<OfficeApiResponse>(jsonResponse);

                        List<OfficeModel> RetLst = new List<OfficeModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            OfficeModel RetObj = new OfficeModel
                            {

                                Operation = item.Operation,
                                company = item.company,
                                emp_id = item.emp_id,
                                location_id = item.location_id,
                                office_name = item.office_name,
                                address_line1 = item.address_line1,
                                address_line2 = item.address_line2,
                                city = item.city,
                                state = item.state,
                                postal_code = item.postal_code,
                                country = item.country,
                                phone_number = item.phone_number,
                                ofc_lat = item.ofc_lat,
                                ofc_long = item.ofc_long,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                updated_by = item.updated_by,
                                created_by = item.created_by,
                                att_radius = item.att_radius

                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.OfficeDataList = RetLst;
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


        public async Task<OfficeReturn> GetOfficeById(OfficeModel getModel)
        {
            OfficeReturn RObj = new OfficeReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OfficeModel
            {
                Operation = 5,
                emp_id = getModel.emp_id,
                company = getModel.company,
                location_id = getModel.location_id
            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/office";


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
                        var apiResponse = JsonConvert.DeserializeObject<OfficeApiResponse>(jsonResponse);

                        List<OfficeModel> RetLst = new List<OfficeModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            OfficeModel RetObj = new OfficeModel
                            {

                                Operation = item.Operation,
                                company = item.company,
                                emp_id = item.emp_id,
                                location_id = item.location_id,
                                office_name = item.office_name,
                                address_line1 = item.address_line1,
                                address_line2 = item.address_line2,
                                city = item.city,
                                state = item.state,
                                postal_code = item.postal_code,
                                country = item.country,
                                phone_number = item.phone_number,
                                ofc_lat = item.ofc_lat,
                                ofc_long = item.ofc_long,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                updated_by = item.updated_by,
                                created_by = item.created_by,
                                
                                att_radius = item.att_radius
                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.OfficeDataList = RetLst;
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