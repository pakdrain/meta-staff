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
using static HrmApi.DAL.TestDAL;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace HrmApi.DAL
{
    public class CompanyDAL
    {
        private string _ConnString; //variable for connection string
        public CompanyDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region companies

        public async Task<CompanyReturn> SyncCompanies(CompaniesModel getModel)
        {
            CompanyReturn RObj = new CompanyReturn();
            int retstatus;
            string retmessage;
            string syncingClientCode = getModel.client_code;
            retstatus = 80;
            retmessage = "No Company Api's Url Find by this Client!";
            try
            {
                //array for parameters
                var ApiUrlGetparm = new SqlParameter[2];
                //setting parameters value get from user
                ApiUrlGetparm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 1 };
                ApiUrlGetparm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = syncingClientCode };

                //calling procedure
                var ApiUrlGetdsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Api_Url_proc]", ApiUrlGetparm);


                // for single object declaration
                var ApiUrlGetRetObj = new CompaniesModel();
                // for list object declaration
                List<CompaniesModel> ApiUrlGetRetLst = new List<CompaniesModel>();
                // getting all rows
                foreach (DataRow row in ApiUrlGetdsuser.Rows)
                {
                    ApiUrlGetRetObj = new CompaniesModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        ApiUrlGetRetObj.company_name = Common.CheckStringNull(row["api_url"]);
                        //add all get object in list 
                        ApiUrlGetRetLst.Add(ApiUrlGetRetObj);
                    }


                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                //RObj.CompaniesDataList = ApiUrlGetRetLst;

                if (retstatus == 2)
                {
                    foreach (var ApiUrlGetItems in ApiUrlGetRetLst) // Iterate over each employee model in the list
                    {
                        try
                        {
                            // Define the API URL
                            string apiUrl = ApiUrlGetItems.company_name;

                            // Create a new HttpClient instance
                            using (HttpClient client = new HttpClient())
                            {
                                // Send GET request to the API
                                HttpResponseMessage response = await client.GetAsync(apiUrl);

                                // Ensure the response is successful
                                response.EnsureSuccessStatusCode();

                                // Read the JSON response body
                                string jsonResponse = await response.Content.ReadAsStringAsync();

                                // Deserialize the JSON into an object
                                var apiResponse = JsonConvert.DeserializeObject<CompanyApiResponse>(jsonResponse);

                                // Initialize the list for storing complaint data
                                List<CompaniesModel> RetLst = new List<CompaniesModel>();

                                // Map the API response "items" to the ComplainModel
                                foreach (var item in apiResponse.Items)
                                {
                                    CompaniesModel RetObj = new CompaniesModel
                                    {

                                        client_code = syncingClientCode,
                                        company_id = item.company_id,
                                        company_name = item.company_name,
                                        is_active = item.is_active
                                    };

                                    // Add to the result list
                                    RetLst.Add(RetObj);
                                }


                                try
                                {

                                    foreach (var sendItems in RetLst) // Iterate over each employee model in the list
                                    {
                                        // Array for parameters
                                        var parm = new SqlParameter[7];
                                        // Setting parameters value from the user input (getModel)
                                        parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = 50 };
                                        parm[1] = new SqlParameter("sr_id", SqlDbType.Int) { Value = sendItems.sr_id };
                                        parm[2] = new SqlParameter("sr_name", SqlDbType.VarChar) { Value = sendItems.sr_name };
                                        parm[3] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = sendItems.client_code };
                                        parm[4] = new SqlParameter("company_id", SqlDbType.VarChar) { Value = sendItems.company_id };
                                        parm[5] = new SqlParameter("company_name", SqlDbType.VarChar) { Value = sendItems.company_name };
                                        parm[6] = new SqlParameter("is_active", SqlDbType.Int) { Value = sendItems.is_active };


                                        // Call the stored procedure for each employee
                                        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Company_Processing]", parm);
                                        foreach (DataRow row in dsuser.Rows)
                                        {
                                            retstatus = Common.CheckIntegerNull(row["res_status"]);
                                            retmessage = Common.CheckStringNull(row["res_message"]);
                                        }
                                    }

                                    RObj.Status = retstatus; // Success status
                                    RObj.Message = retmessage;
                                    // Assign the list to the response object
                                    /*if (retstatus == 2)
                                    {
                                        RObj.CompaniesDataList = RetLst;
                                    }*/

                                }
                                // If an error occurs during API execution
                                catch (Exception ex)
                                {
                                    RObj.Status = 60;
                                    RObj.Message = "Error Found, " + ex.ToString();
                                    return RObj;
                                }

                            }


                        }
                        catch (Exception ex)
                        {
                            RObj.Status = 70;
                            RObj.Message = "No Data Found From Your Given Api Error Found: " + ex.Message;
                            return RObj;
                        }
                    }
                }
                return RObj;
            }
            // if api calling error
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }

        }

        public CompanyReturn GetCompaniesFromApi(CompaniesModel getModel)
        {
            CompanyReturn RObj = new CompanyReturn();
            int retstatus; string retmessage;
            try
            {
                var parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.Int) { Value = getModel.client_code };

                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Company_Processing]", parm);

                var RetObj = new CompaniesModel();
                List<CompaniesModel> RetLst = new List<CompaniesModel>();

                retstatus = 50;
                retmessage = "No Data Found!";

                foreach (DataRow row in dsuser.Rows)

                {
                    RetObj = new CompaniesModel();
                    retstatus = (int)row["res_status"];
                    retmessage = Common.CheckStringNull(row["res_message"]);

                    if (retstatus == 2)
                    {
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company_id = Common.CheckStringNull(row["company_id"]);
                        RetObj.company_name = Common.CheckStringNull(row["company_name"]);
                        RetObj.is_active = Common.CheckIntegerNull(row["is_active"]);

                    }

                    RetLst.Add(RetObj);


                }
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.CompaniesData = RetObj;
                // if want to show in list of object
                RObj.CompaniesDataList = RetLst;
                return RObj;
            }
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }
        }


        public async Task<CompanyReturn> GetCompForFilter(CompaniesModel getModel)
        {
            CompanyReturn RObj = new CompanyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new CompaniesModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                master_acc = getModel.master_acc
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/companies";


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
                        var apiResponse = JsonConvert.DeserializeObject<CompanyApiResponse>(jsonResponse);

                        List<CompaniesModel> RetLst = new List<CompaniesModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            if (retstatus == 2)
                            {
                                CompaniesModel RetObj = new CompaniesModel
                                {

                                    company_id_to_show = item.company_id_to_show,
                                    company_id = item.company_id,
                                    company_name = item.company_name,
                                    is_active = item.is_active


                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.CompaniesDataList = RetLst;


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

        public async Task<CompanyReturn> GetCompForClearance(CompaniesModel getModel)
        {
            CompanyReturn RObj = new CompanyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new CompaniesModel
            {
                Operation = 3,

                emp_id = getModel.emp_id
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/companies";


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
                        var apiResponse = JsonConvert.DeserializeObject<CompanyApiResponse>(jsonResponse);

                        List<CompaniesModel> RetLst = new List<CompaniesModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            if (retstatus == 2)
                            {
                                CompaniesModel RetObj = new CompaniesModel
                                {

                                    sr_id = item.sr_id,
                                    company_id_to_show = item.company_id_to_show,
                                    company_id = item.company_id,
                                    company_name = item.company_name,
                                    is_active = item.is_active


                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.CompaniesDataList = RetLst;


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

        public async Task<CompanyReturn> AUDSeekingCompanies(CompaniesModel getModel)
        {
            CompanyReturn RObj = new CompanyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";

            int operation = getModel.Operation;
            var attendanceRequest = new CompaniesModel
            {
                Operation = getModel.Operation,
                company_id = getModel.company_id,
                emp_id = getModel.emp_id,
                sr_id = getModel.sr_id,
               
            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/companies";

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
                        var apiResponse = JsonConvert.DeserializeObject<CompanyApiResponse>(jsonResponse);

                        List<CompaniesModel> RetLst = new List<CompaniesModel>();

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

            return RObj;
        }


        public async Task<CompanyReturn> GetSeeCompaniesByEmp(CompaniesModel getModel)
        {
            CompanyReturn RObj = new CompanyReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new CompaniesModel
            {
                Operation = 7,
                emp_id = getModel.emp_id,
               

            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/companies";


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
                        var apiResponse = JsonConvert.DeserializeObject<CompanyApiResponse>(jsonResponse);

                        List<CompaniesModel> RetLst = new List<CompaniesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            CompaniesModel RetObj = new CompaniesModel
                            {

                                Operation = item.Operation,
                                sr_id = item.sr_id,
                                emp_id = item.emp_id,
                                company_id = item.company_id,
                                company_id_to_show = item.company_id_to_show,
                                company_name = item.company_name,

                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.CompaniesDataList = RetLst;
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