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

namespace HrmApi.DAL
{
    public class DepartmentDAL
    {
        private string _ConnString; //variable for connection string
        public DepartmentDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region departments

        public DepartmentReturn DepartmentsDetail(DepartmentModel getModel)
        {
            DepartmentReturn RObj = new DepartmentReturn();
            int retstatus; string retmessage;
            try
            {
                var parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };

                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Department_Proc]", parm);

                var RetObj = new DepartmentModel();
                List<DepartmentModel> RetLst = new List<DepartmentModel>();

                retstatus = 50;
                retmessage = "No Data Found!";

                foreach (DataRow row in dsuser.Rows)

                {
                    RetObj = new DepartmentModel();
                    retstatus = (int)row["res_status"];
                    retmessage = Common.CheckStringNull(row["res_message"]);

                    if (retstatus == 2)
                    {
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.dep_id = Common.CheckStringNull(row["dep_id"]);
                        RetObj.dep_name = Common.CheckStringNull(row["dep_name"]);
                        RetObj.is_active = Common.CheckIntegerNull(row["is_active"]);

                    }

                    RetLst.Add(RetObj);


                }
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.CompaniesData = RetObj;
                // if want to show in list of object
                RObj.DepartmentDataList = RetLst;
                return RObj;
            }
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }
        }
        
        public async Task<DepartmentReturn> GetDepartments(DepartmentModel getModel)
        {
            DepartmentReturn RObj = new DepartmentReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new DepartmentModel
            {
                Operation = 1,
                company = getModel.company,
                region_id = getModel.region_id
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/departments";


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
                        var apiResponse = JsonConvert.DeserializeObject<DepartmentApiResponse>(jsonResponse);

                        List<DepartmentModel> RetLst = new List<DepartmentModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            if (retstatus == 2)
                            {
                                DepartmentModel RetObj = new DepartmentModel
                                {

                                    dep_id = item.dep_id,
                                    dep_name = item.dep_name

                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.DepartmentDataList = RetLst;
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

        public async Task<DepartmentReturn> GetDivisons(DepartmentModel getModel)
        {
            DepartmentReturn RObj = new DepartmentReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new DepartmentModel
            {
                Operation = 2,
                company = getModel.company,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/departments";


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
                        var apiResponse = JsonConvert.DeserializeObject<DepartmentApiResponse>(jsonResponse);

                        List<DepartmentModel> RetLst = new List<DepartmentModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            DepartmentModel RetObj = new DepartmentModel
                            {

                                division_id = item.division_id,
                                division_name = item.division_name


                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.DepartmentDataList = RetLst;
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


        public async Task<DepartmentReturn> AUDSeekingDepartment(DepartmentModel getModel)
        {
            DepartmentReturn RObj = new DepartmentReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";

            int operation = getModel.Operation;
            var attendanceRequest = new DepartmentModel
            {
                Operation = getModel.Operation,
                dep_id = getModel.dep_id,
                company = getModel.company,
                emp_id = getModel.emp_id,
                sr_id = getModel.sr_id,
                region_id = getModel.region_id,

            };

            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/departments";

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
                        var apiResponse = JsonConvert.DeserializeObject<DepartmentApiResponse>(jsonResponse);

                        List<DepartmentModel> RetLst = new List<DepartmentModel>();

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

        public async Task<DepartmentReturn> GetSeeDepartments(DepartmentModel getModel)
        {
            DepartmentReturn RObj = new DepartmentReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new DepartmentModel
            {
                Operation = 7,
                emp_id = getModel.emp_id,
                company = getModel.company,
            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/departments";


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
                        var apiResponse = JsonConvert.DeserializeObject<DepartmentApiResponse>(jsonResponse);

                        List<DepartmentModel> RetLst = new List<DepartmentModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            DepartmentModel RetObj = new DepartmentModel
                            {

                                Operation = item.Operation,
                                sr_id = item.sr_id,
                                emp_id = item.emp_id,
                                company = item.company,
                                company_name = item.company_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name

                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.DepartmentDataList = RetLst;
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


        public async Task<DepartmentReturn> GetFilterDepartments(DepartmentModel getModel)
        {
            DepartmentReturn RObj = new DepartmentReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new DepartmentModel
            {
                Operation = 8,
                emp_id = getModel.emp_id,
                company = getModel.company,
                master_acc = getModel.master_acc,
            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/departments";


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
                        var apiResponse = JsonConvert.DeserializeObject<DepartmentApiResponse>(jsonResponse);

                        List<DepartmentModel> RetLst = new List<DepartmentModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            DepartmentModel RetObj = new DepartmentModel
                            {

                                Operation = item.Operation,
                                emp_id = item.emp_id,
                                company = item.company,
                                company_name = item.company_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name

                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.DepartmentDataList = RetLst;
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