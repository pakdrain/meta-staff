using HrmApi.Models;
using HrmApi.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HrmApi.DAL
{
    public class ApprEmpDAL
    {
        private string _ConnString;

        public ApprEmpDAL()
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region ApprEmp

        public async Task<ApprEmpReturn> GetApprEmp(ApprEmpModel getModel)
        {
            ApprEmpReturn result = new ApprEmpReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };
            try
            {
                getModel.Operation = 1;
                string requestUri = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/appr-emp";
                using (HttpClient client = new HttpClient()) { 
                string content = JsonConvert.SerializeObject(getModel);
                StringContent content2 = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await client.PostAsync(requestUri, content2);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    result.Status = 70;
                    result.Message = "API Error: " + httpResponseMessage.StatusCode;
                    return result;
                }

                ApprEmpApiResponse apprEmpApiResponse = JsonConvert.DeserializeObject<ApprEmpApiResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
                if (apprEmpApiResponse?.Items == null || apprEmpApiResponse.Items.Count == 0)
                {
                    result.Status = 70;
                    result.Message = "No Data Found";
                    return result;
                }

                ApprEmpModel apprEmpModel = apprEmpApiResponse.Items[0];
                result.Status = apprEmpModel.res_status;
                result.Message = apprEmpModel.res_message;
                if (apprEmpModel.res_status == 2)
                {
                    result.ApprEmpDataList = apprEmpApiResponse.Items;
                }
            }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
        }
        public async Task<ApprEmpReturn> UpdateApprEmp(ApprEmpRequest getModel)
        {
            ApprEmpReturn RObj = new ApprEmpReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ApprEmpRequest
            {
                Operation = 3,
                app_user = getModel.app_user,
                items = getModel.items,           
            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/appr-emp";


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
                        retmessage = "Api Call successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApprEmpApiResponse>(jsonResponse);
                        retstatus = apiResponse.Items[0].res_status;
                        retmessage = apiResponse.Items[0].res_message;
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
        public async Task<ApprEmpReturn> StatusChangerApprEmp(ApprEmpModel getModel)
        {
            var result = new ApprEmpReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 6;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/appr-emp";

                using (HttpClient client = new HttpClient())
                {
                    string jsonBody = JsonConvert.SerializeObject(getModel);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        result.Status = 70;
                        result.Message = "API Error: " + response.StatusCode;
                        return result;
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var apiResponse =
                        JsonConvert.DeserializeObject<ApprEmpApiResponse>(jsonResponse);

                    if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    {
                        result.Status = 70;
                        result.Message = "No Data Found";
                        return result;
                    }

                    var first = apiResponse.Items[0];

                    result.Status = first.res_status;
                    result.Message = first.res_message;

                    
                }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
        }
        public async Task<ApprSummariesReturn> GetSummaryOne(ApprSummariesModel getModel)
        {
            var result = new ApprSummariesReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 1;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/appr-summaries";

                using (HttpClient client = new HttpClient())
                {
                    string jsonBody = JsonConvert.SerializeObject(getModel);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        result.Status = 70;
                        result.Message = "API Error: " + response.StatusCode;
                        return result;
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var apiResponse =
                        JsonConvert.DeserializeObject<ApprSummariesApiResponse>(jsonResponse);

                    if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    {
                        result.Status = 70;
                        result.Message = "No Data Found";
                        return result;
                    }

                    var first = apiResponse.Items[0];

                    result.Status = first.res_status;
                    result.Message = first.res_message;

                    if (first.res_status == 2)
                    {
                        result.ApprSummariesDataList = apiResponse.Items;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
        }
        public async Task<ApprSummariesReturn> GetSummaryTwo(ApprSummariesModel getModel)
        {
            var result = new ApprSummariesReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 2;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/appr-summaries";

                using (HttpClient client = new HttpClient())
                {
                    string jsonBody = JsonConvert.SerializeObject(getModel);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        result.Status = 70;
                        result.Message = "API Error: " + response.StatusCode;
                        return result;
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var apiResponse =
                        JsonConvert.DeserializeObject<ApprSummariesApiResponse>(jsonResponse);

                    if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    {
                        result.Status = 70;
                        result.Message = "No Data Found";
                        return result;
                    }

                    var first = apiResponse.Items[0];

                    result.Status = first.res_status;
                    result.Message = first.res_message;

                    if (first.res_status == 2)
                    {
                        result.ApprSummariesDataList = apiResponse.Items;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
        }
        public async Task<ApprSummariesReturn> GetSummaryThree(ApprSummariesModel getModel)
        {
            var result = new ApprSummariesReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 3;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/appr-summaries";

                using (HttpClient client = new HttpClient())
                {
                    string jsonBody = JsonConvert.SerializeObject(getModel);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        result.Status = 70;
                        result.Message = "API Error: " + response.StatusCode;
                        return result;
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var apiResponse =
                        JsonConvert.DeserializeObject<ApprSummariesApiResponse>(jsonResponse);

                    if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    {
                        result.Status = 70;
                        result.Message = "No Data Found";
                        return result;
                    }

                    var first = apiResponse.Items[0];

                    result.Status = first.res_status;
                    result.Message = first.res_message;

                    if (first.res_status == 2)
                    {
                        result.ApprSummariesDataList = apiResponse.Items;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
        }
        public async Task<ApprEmpReturn> TNAReport(ApprEmpModel getModel)
        {
            ApprEmpReturn result = new ApprEmpReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };
            try
            {
                getModel.Operation = 6;
                string requestUri = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/appr-emp";
                using (HttpClient client = new HttpClient()) { 
                string content = JsonConvert.SerializeObject(getModel);
                StringContent content2 = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await client.PostAsync(requestUri, content2);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    result.Status = 70;
                    result.Message = "API Error: " + httpResponseMessage.StatusCode;
                    return result;
                }

                ApprEmpApiResponse apprEmpApiResponse = JsonConvert.DeserializeObject<ApprEmpApiResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
                if (apprEmpApiResponse?.Items == null || apprEmpApiResponse.Items.Count == 0)
                {
                    result.Status = 70;
                    result.Message = "No Data Found";
                    return result;
                }

                ApprEmpModel apprEmpModel = apprEmpApiResponse.Items[0];
                result.Status = apprEmpModel.res_status;
                result.Message = apprEmpModel.res_message;
                if (apprEmpModel.res_status == 2)
                {
                    result.ApprEmpDataList = apprEmpApiResponse.Items;
                }
                }
            }
            catch (Exception ex)
            {
                result.Status = 70;
                result.Message = "Error: " + ex.Message;
            }

            return result;
        }


        #endregion
    }
}