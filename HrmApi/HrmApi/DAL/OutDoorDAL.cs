using HrmApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HrmApi.DAL
{
    public class OutDoorDAL
    {
        private string _ConnString; //variable for connection string
        public OutDoorDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region OutDoor

        public async Task<OutDoorReturn> AUDOutDoor(OutDoorModel getModel)
        {
            OutDoorReturn RObj = new OutDoorReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var outdoorRequest = new OutDoorModel
            {
                Operation = getModel.Operation,
                client_code = getModel.client_code,
                company = getModel.company,
                emp_id = getModel.emp_id,
                od_id = getModel.od_id,
                from_date = getModel.from_date,
                str_from_date = getModel.str_from_date,
                to_date = getModel.to_date,
                str_to_date = getModel.str_to_date,
                reason = getModel.reason,
                remark_by_hod = getModel.remark_by_hod,
                remark_by_hr = getModel.remark_by_hr,
                status = getModel.status,
                created_date = getModel.created_date,
                updated_date = getModel.updated_date,
                created_by = getModel.created_by,
                updated_by = getModel.updated_by,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                hod_app_date = getModel.hod_app_date,
                hr_app_date = getModel.hr_app_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                rej_remarks = getModel.rej_remarks,
                duty_duration = getModel.duty_duration,

            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/out-door-duty";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(outdoorRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OutdoorApiResponse>(jsonResponse);

                        List<OutDoorModel> RetLst = new List<OutDoorModel>();

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

        public async Task<OutDoorReturn> GetMyOD(OutDoorModel getModel)
        {
            OutDoorReturn RObj = new OutDoorReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OutDoorModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                company = getModel.company,
                client_code = getModel.client_code,
                str_from_date = getModel.str_from_date, 
                str_to_date = getModel.str_to_date,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/out-door-duty";


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
                        retmessage = "Api Run But Data not fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OutdoorApiResponse>(jsonResponse);

                        List<OutDoorModel> RetLst = new List<OutDoorModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            OutDoorModel RetObj = new OutDoorModel
                            {

                                Operation = item.Operation,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                od_id = item.od_id,
                                from_date = item.from_date,
                                str_from_date = item.str_from_date,
                                to_date = item.to_date,
                                str_to_date = item.str_to_date,
                                reason = item.reason,
                                remark_by_hod = item.remark_by_hod,
                                remark_by_hr = item.remark_by_hr,
                                status = item.status,
                                created_date = item.created_date,
                                updated_date = item.updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                hod_app_date = item.hod_app_date,
                                hr_app_date = item.hr_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                rej_remarks = item.rej_remarks,
                                str_created_date = item.str_created_date,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                duty_duration = item.duty_duration,


                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.OutDoorDataList = RetLst;
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

        public async Task<OutDoorReturn> GetODById(OutDoorModel getModel)
        {
            OutDoorReturn RObj = new OutDoorReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OutDoorModel
            {
                Operation = 5,
                emp_id = getModel.emp_id,
                company = getModel.company,
                client_code = getModel.client_code,
                od_id = getModel.od_id
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/out-door-duty";


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
                        var apiResponse = JsonConvert.DeserializeObject<OutdoorApiResponse>(jsonResponse);

                        List<OutDoorModel> RetLst = new List<OutDoorModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            OutDoorModel RetObj = new OutDoorModel
                            {

                                Operation = item.Operation,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                od_id = item.od_id,
                                from_date = item.from_date,
                                str_from_date = item.str_from_date,
                                to_date = item.to_date,
                                str_to_date = item.str_to_date,
                                reason = item.reason,
                                remark_by_hod = item.remark_by_hod,
                                remark_by_hr = item.remark_by_hr,
                                status = item.status,
                                created_date = item.created_date,
                                updated_date = item.updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                hod_app_date = item.hod_app_date,
                                hr_app_date = item.hr_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                rej_remarks = item.rej_remarks,
                                str_created_date = item.str_created_date,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                duty_duration = item.duty_duration,

                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.OutDoorDataList = RetLst;
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

        public async Task<OutDoorReturn> GetHodOD(OutDoorModel getModel)
        {
            OutDoorReturn RObj = new OutDoorReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OutDoorModel
            {
                Operation = 7,
                emp_id = getModel.emp_id,
                company = getModel.company,
                client_code = getModel.client_code,
                org_id = getModel.org_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/out-door-duty";


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
                        var apiResponse = JsonConvert.DeserializeObject<OutdoorApiResponse>(jsonResponse);

                        List<OutDoorModel> RetLst = new List<OutDoorModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            OutDoorModel RetObj = new OutDoorModel
                            {

                                Operation = item.Operation,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                od_id = item.od_id,
                                from_date = item.from_date,
                                str_from_date = item.str_from_date,
                                to_date = item.to_date,
                                str_to_date = item.str_to_date,
                                reason = item.reason,
                                remark_by_hod = item.remark_by_hod,
                                remark_by_hr = item.remark_by_hr,
                                status = item.status,
                                created_date = item.created_date,
                                updated_date = item.updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                hod_app_date = item.hod_app_date,
                                hr_app_date = item.hr_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                rej_remarks = item.rej_remarks,
                                str_created_date = item.str_created_date,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                duty_duration = item.duty_duration,


                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.OutDoorDataList = RetLst;
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

        public async Task<OutDoorReturn> GetHrOD(OutDoorModel getModel)
        {
            OutDoorReturn RObj = new OutDoorReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OutDoorModel
            {
                Operation = 8,
                emp_id = getModel.emp_id,
                company = getModel.company,
                client_code = getModel.client_code,
                org_id = getModel.org_id,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                user_emp_id = getModel.user_emp_id,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/out-door-duty";


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
                        var apiResponse = JsonConvert.DeserializeObject<OutdoorApiResponse>(jsonResponse);

                        List<OutDoorModel> RetLst = new List<OutDoorModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            OutDoorModel RetObj = new OutDoorModel
                            {

                                Operation = item.Operation,
                                client_code = item.client_code,
                                company = item.company,
                                emp_id = item.emp_id,
                                od_id = item.od_id,
                                from_date = item.from_date,
                                str_from_date = item.str_from_date,
                                to_date = item.to_date,
                                str_to_date = item.str_to_date,
                                reason = item.reason,
                                remark_by_hod = item.remark_by_hod,
                                remark_by_hr = item.remark_by_hr,
                                status = item.status,
                                created_date = item.created_date,
                                updated_date = item.updated_date,
                                created_by = item.created_by,
                                updated_by = item.updated_by,
                                hod_app_by = item.hod_app_by,
                                hr_app_by = item.hr_app_by,
                                hod_app_date = item.hod_app_date,
                                hr_app_date = item.hr_app_date,
                                rej_by = item.rej_by,
                                rej_date = item.rej_date,
                                rej_remarks = item.rej_remarks,
                                str_created_date = item.str_created_date,
                                emp_name = item.emp_name,
                                department = item.department,
                                designation = item.designation,
                                duty_duration = item.duty_duration,
                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.OutDoorDataList = RetLst;
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

        public async Task<OutDoorReturn> ODStatusChanger(OutDoorModel getModel)
        {
            OutDoorReturn RObj = new OutDoorReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var outdoorRequest = new OutDoorModel
            {
                Operation = 6,
                client_code = getModel.client_code,
                company = getModel.company,
                emp_id = getModel.emp_id,
                od_id = getModel.od_id,
                from_date = getModel.from_date,
                str_from_date = getModel.str_from_date,
                to_date = getModel.to_date,
                str_to_date = getModel.str_to_date,
                reason = getModel.reason,
                remark_by_hod = getModel.remark_by_hod,
                remark_by_hr = getModel.remark_by_hr,
                status = getModel.status,
                created_date = getModel.created_date,
                updated_date = getModel.updated_date,
                created_by = getModel.created_by,
                updated_by = getModel.updated_by,
                hod_app_by = getModel.hod_app_by,
                hr_app_by = getModel.hr_app_by,
                hod_app_date = getModel.hod_app_date,
                hr_app_date = getModel.hr_app_date,
                rej_by = getModel.rej_by,
                rej_date = getModel.rej_date,
                rej_remarks = getModel.rej_remarks

            };


            try
            {
                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/out-door-duty";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(outdoorRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OutdoorApiResponse>(jsonResponse);

                        List<OutDoorModel> RetLst = new List<OutDoorModel>();

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

        public async Task<OutDoorReturn> GetDutyDuration(OutDoorModel getModel)
        {
            OutDoorReturn RObj = new OutDoorReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OutDoorModel
            {
                Operation = 9,
                emp_id = getModel.emp_id,
                company = getModel.company,
                client_code = getModel.client_code,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/out-door-duty";


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
                        var apiResponse = JsonConvert.DeserializeObject<OutdoorApiResponse>(jsonResponse);

                        List<OutDoorModel> RetLst = new List<OutDoorModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            if (retstatus == 2)
                            {
                                OutDoorModel RetObj = new OutDoorModel
                                {


                                    duty_duration = item.duty_duration,

                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.OutDoorDataList = RetLst;
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