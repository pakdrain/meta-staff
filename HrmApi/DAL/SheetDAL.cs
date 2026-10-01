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
    public class SheetDAL
    {
        private string _ConnString;

        public SheetDAL()
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Sheet

        public async Task<SheetReturn> GetSheets(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 1;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    //if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    //{
                    //    result.Status = 70;
                    //    result.Message = "No Data Found";
                    //    return result;
                    //}

                    //var first = apiResponse.Items[0];

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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
        public async Task<SheetReturn> AUDSheets(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                //getModel.Operation = 3;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    //if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    //{
                    //    result.Status = 70;
                    //    result.Message = "No Data Found";
                    //    return result;
                    //}

                    //var first = apiResponse.ResStatus;

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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
        public async Task<SheetReturn> GetSheetById(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 5;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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
        public async Task<SheetReturn> StatusChanger(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 6;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    //if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    //{
                    //    result.Status = 70;
                    //    result.Message = "No Data Found";
                    //    return result;
                    //}

                    //var first = apiResponse.ResStatus;

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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
        public async Task<SheetReturn> HRSheets(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 10;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    //if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    //{
                    //    result.Status = 70;
                    //    result.Message = "No Data Found";
                    //    return result;
                    //}

                    //var first = apiResponse.Items[0];

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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
        public async Task<SheetReturn> EDSheets(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 11;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    //if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    //{
                    //    result.Status = 70;
                    //    result.Message = "No Data Found";
                    //    return result;
                    //}

                    //var first = apiResponse.Items[0];

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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
        public async Task<SheetReturn> HODSheets(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 9;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    //if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    //{
                    //    result.Status = 70;
                    //    result.Message = "No Data Found";
                    //    return result;
                    //}

                    //var first = apiResponse.Items[0];

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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
        public async Task<SheetReturn> GMSheets(SheetModel getModel)
        {
            var result = new SheetReturn
            {
                Status = 80,
                Message = "Api is not Fetching!"
            };

            try
            {
                getModel.Operation = 8;

                string apiUrl =
                    "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/sheets";

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
                        JsonConvert.DeserializeObject<SheetApiResponse>(jsonResponse);

                    //if (apiResponse?.Items == null || apiResponse.Items.Count == 0)
                    //{
                    //    result.Status = 70;
                    //    result.Message = "No Data Found";
                    //    return result;
                    //}

                    //var first = apiResponse.Items[0];

                    result.Status = apiResponse.ResStatus;
                    result.Message = apiResponse.ResMessage;

                    if (apiResponse.ResStatus == 2)
                    {
                        result.SheetDataList = apiResponse.Items;
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