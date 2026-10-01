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
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mysqlx.Crud;

namespace HrmApi.DAL
{
    public class OrderDAL
    {
        private string _ConnString; //variable for connection string
        public OrderDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region orders

        //AUD ORDERS
        public async Task<OrderMastReturn> AUDOrder(OrderMastModel getModel)
        {
            OrderMastReturn RObj = new OrderMastReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";

            int operation = getModel.Operation;

            var PostOrderModel = new OrderMastModel
            {

                Operation = getModel.Operation,
                company = getModel.company,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                order_id = getModel.order_id,
                order_no = getModel.order_no,
                order_date = getModel.order_date,
                order_status = getModel.order_status,
                delivery_date = getModel.delivery_date,
                total_qty = getModel.total_qty,
                total_kgs = getModel.total_kgs,
                discount = getModel.discount,
                tax = getModel.tax,
                total_amount = getModel.total_amount,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                OrderDetail = getModel.OrderDetail
            };


            try
            {
                string apiUrl = "https://hd.sabirserp.com:8443/ords/sabroso/hrm/orders";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(PostOrderModel);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        retstatus = 100;
                        retmessage = "Data fetched successfully";


                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OrderApiResponse>(jsonResponse);

                        List<OrderMastModel> RetLst = new List<OrderMastModel>();

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

        public async Task<OrderMastReturn> GetPendingOrders(OrderMastModel getModel)
        {
            OrderMastReturn RObj = new OrderMastReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OrderMastModel
            {
                Operation = 1,
                company = getModel.company,
                emp_id = getModel.emp_id
            };


            try
            {

                string apiUrl = "https://hd.sabirserp.com:8443/ords/sabroso/hrm/orders";


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
                        var apiResponse = JsonConvert.DeserializeObject<OrderApiResponse>(jsonResponse);

                        List<OrderMastModel> RetLst = new List<OrderMastModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            if (retstatus == 2)
                            {
                                OrderMastModel RetObj = new OrderMastModel
                                {

                                    Operation = item.Operation,
                                    company = item.company,
                                    emp_id = item.emp_id,
                                    order_id = item.order_id,
                                    order_no = item.order_no,
                                    order_date = item.order_date,
                                    str_order_date = item.str_order_date,
                                    order_status = item.order_status,
                                    delivery_date = item.delivery_date,
                                    str_delivery_date = item.str_delivery_date,
                                    total_qty = item.total_qty,
                                    total_kgs = item.total_kgs,
                                    discount = item.discount,
                                    tax = item.tax,
                                    total_amount = item.total_amount,
                                    created_by = item.created_by,
                                    created_date = item.created_date,
                                    str_created_date = item.str_created_date,
                                    updated_by = item.updated_by,
                                    updated_date = item.updated_date,
                                    str_updated_date = item.str_updated_date,
                                    emp_name = item.emp_name,
                                    designation = item.designation,
                                    OrderDetail = item.OrderDetail
                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        RObj.OrderDataList = RetLst;


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

        public async Task<OrderMastReturn> GetOrderById(OrderMastModel getModel)
        {
            OrderMastReturn RObj = new OrderMastReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OrderMastModel
            {
                Operation = 5,
                company = getModel.company,
                emp_id = getModel.emp_id,
                order_id =  getModel.order_id
            };


            try
            {

                string apiUrl = "https://hd.sabirserp.com:8443/ords/sabroso/hrm/orders";


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
                        var apiResponse = JsonConvert.DeserializeObject<OrderApiResponse>(jsonResponse);

                        List<OrderMastModel> RetLst = new List<OrderMastModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            if (retstatus == 2)
                            {
                                OrderMastModel RetObj = new OrderMastModel
                                {

                                    Operation = item.Operation,
                                    company = item.company,
                                    emp_id = item.emp_id,
                                    order_id = item.order_id,
                                    order_no = item.order_no,
                                    order_date = item.order_date,
                                    str_order_date = item.str_order_date,
                                    order_status = item.order_status,
                                    delivery_date = item.delivery_date,
                                    str_delivery_date = item.str_delivery_date,
                                    total_qty = item.total_qty,
                                    total_kgs = item.total_kgs,
                                    discount = item.discount,
                                    tax = item.tax,
                                    total_amount = item.total_amount,
                                    created_by = item.created_by,
                                    created_date = item.created_date,
                                    str_created_date = item.str_created_date,
                                    updated_by = item.updated_by,
                                    updated_date = item.updated_date,
                                    str_updated_date = item.str_updated_date,
                                    emp_name = item.emp_name,
                                    designation = item.designation,
                                    OrderDetail = item.OrderDetail
                                   
                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        RObj.OrderDataList = RetLst;


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

        public async Task<OrderMastReturn> GetOrderDetail(OrderMastModel getModel)
        {
            OrderMastReturn RObj = new OrderMastReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OrderMastModel
            {
                Operation = 7,
                company = getModel.company,
                emp_id = getModel.emp_id,
                order_id = getModel.order_id
            };


            try
            {

                string apiUrl = "https://hd.sabirserp.com:8443/ords/sabroso/hrm/orders";


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
                        var apiResponse = JsonConvert.DeserializeObject<OrderApiResponse>(jsonResponse);

                        List<OrderMastModel> RetLst = new List<OrderMastModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            if (retstatus == 2)
                            {
                                OrderMastModel RetObj = new OrderMastModel
                                {

                                    Operation = item.Operation,
                                    company = item.company,
                                    emp_id = item.emp_id,
                                    order_id = item.order_id,
                                    order_no = item.order_no,
                                    order_date = item.order_date,
                                    str_order_date = item.str_order_date,
                                    order_status = item.order_status,
                                    delivery_date = item.delivery_date,
                                    str_delivery_date = item.str_delivery_date,
                                    total_qty = item.total_qty,
                                    total_kgs = item.total_kgs,
                                    discount = item.discount,
                                    tax = item.tax,
                                    total_amount = item.total_amount,
                                    created_by = item.created_by,
                                    created_date = item.created_date,
                                    str_created_date = item.str_created_date,
                                    updated_by = item.updated_by,
                                    updated_date = item.updated_date,
                                    str_updated_date = item.str_updated_date,
                                    emp_name = item.emp_name,
                                    designation = item.designation,
                                    OrderDetail = item.OrderDetail
                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        RObj.OrderDataList = RetLst;


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

        public async Task<OrderMastReturn> StatusChanger(OrderMastModel getModel)
        {
            OrderMastReturn RObj = new OrderMastReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";

            int operation = getModel.Operation;

            var PostOrderModel = new OrderMastModel
            {
                Operation = 6,
                company = getModel.company,
                emp_id = getModel.emp_id,
                order_id = getModel.order_id                
            };


            try
            {
                string apiUrl = "https://hd.sabirserp.com:8443/ords/sabroso/hrm/orders";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(PostOrderModel);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        retstatus = 100;
                        retmessage = "Data fetched successfully";


                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OrderApiResponse>(jsonResponse);

                        List<OrderMastModel> RetLst = new List<OrderMastModel>();

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


        public async Task<OrderMastReturn> GetPostedOrders(OrderMastModel getModel)
        {
            OrderMastReturn RObj = new OrderMastReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OrderMastModel
            {
                Operation = 8,
                company = getModel.company,
                emp_id = getModel.emp_id
            };


            try
            {

                string apiUrl = "https://hd.sabirserp.com:8443/ords/sabroso/hrm/orders";


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
                        var apiResponse = JsonConvert.DeserializeObject<OrderApiResponse>(jsonResponse);

                        List<OrderMastModel> RetLst = new List<OrderMastModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            if (retstatus == 2)
                            {
                                OrderMastModel RetObj = new OrderMastModel
                                {

                                    Operation = item.Operation,
                                    company = item.company,
                                    emp_id = item.emp_id,
                                    order_id = item.order_id,
                                    order_no = item.order_no,
                                    order_date = item.order_date,
                                    str_order_date = item.str_order_date,
                                    order_status = item.order_status,
                                    delivery_date = item.delivery_date,
                                    str_delivery_date = item.str_delivery_date,
                                    total_qty = item.total_qty,
                                    total_kgs = item.total_kgs,
                                    discount = item.discount,
                                    tax = item.tax,
                                    total_amount = item.total_amount,
                                    created_by = item.created_by,
                                    created_date = item.created_date,
                                    str_created_date = item.str_created_date,
                                    updated_by = item.updated_by,
                                    updated_date = item.updated_date,
                                    str_updated_date = item.str_updated_date,
                                    emp_name = item.emp_name,
                                    designation = item.designation,
                                    OrderDetail = item.OrderDetail
                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        RObj.OrderDataList = RetLst;


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


        public async Task<OrderMastReturn> GetPostOrderById(OrderMastModel getModel)
        {
            OrderMastReturn RObj = new OrderMastReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OrderMastModel
            {
                Operation = 9,
                company = getModel.company,
                emp_id = getModel.emp_id,
                order_id = getModel.order_id
            };


            try
            {

                string apiUrl = "https://hd.sabirserp.com:8443/ords/sabroso/hrm/orders";


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
                        var apiResponse = JsonConvert.DeserializeObject<OrderApiResponse>(jsonResponse);

                        List<OrderMastModel> RetLst = new List<OrderMastModel>();

                        foreach (var item in apiResponse.Items)
                        {

                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            if (retstatus == 2)
                            {
                                OrderMastModel RetObj = new OrderMastModel
                                {

                                    Operation = item.Operation,
                                    company = item.company,
                                    emp_id = item.emp_id,
                                    order_id = item.order_id,
                                    order_no = item.order_no,
                                    order_date = item.order_date,
                                    str_order_date = item.str_order_date,
                                    order_status = item.order_status,
                                    delivery_date = item.delivery_date,
                                    str_delivery_date = item.str_delivery_date,
                                    total_qty = item.total_qty,
                                    total_kgs = item.total_kgs,
                                    discount = item.discount,
                                    tax = item.tax,
                                    total_amount = item.total_amount,
                                    created_by = item.created_by,
                                    created_date = item.created_date,
                                    str_created_date = item.str_created_date,
                                    updated_by = item.updated_by,
                                    updated_date = item.updated_date,
                                    str_updated_date = item.str_updated_date,
                                    emp_name = item.emp_name,
                                    designation = item.designation,
                                    OrderDetail = item.OrderDetail
                                };

                                RetLst.Add(RetObj);
                            }

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        RObj.OrderDataList = RetLst;


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


        ///////////////// GET ORDER LIST   //////////////////////////
        //public OrderMastReturn GetOrderList(OrderMastModel getModel) // get modal from user in getModel
        //{
        //    OrderMastReturn RObj = new OrderMastReturn(); // for result body object declaration
        //    int retstatus; string retmessage;

        //    try
        //    {
        //        //array for parameters
        //        var parm = new SqlParameter[5];
        //        //setting parameters value get from user
        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //        parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
        //        parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
        //        parm[4] = new SqlParameter("@order_id", SqlDbType.Int) { Value = getModel.order_id };

        //        //calling procedure
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_DO_Mast_Proc]", parm);
        //        retstatus = 50;
        //        retmessage = "No Data Found!";
        //        var strOrderDate = ""; // for string date
        //        var strDeliveryDate = "";
        //        var strCreatedDate = ""; // for string date
        //        var strUpdatedDate = ""; // for string date

        //        // for single object declaration
        //        var RetObj = new OrderMastModel();
        //        // for list object declaration
        //        List<OrderMastModel> RetLst = new List<OrderMastModel>();
        //        // getting all rows
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            RetObj = new OrderMastModel(); //this important for all rows

        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //            if (retstatus == 2)
        //            {
        //                //coloumn mapping

        //                var pMastID = Common.CheckIntegerNull(row["order_id"]);
        //                RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
        //                RetObj.order_id = Common.CheckIntegerNull(row["order_id"]);
        //                RetObj.order_no = Common.CheckIntegerNull(row["order_no"]);
        //                if (!string.IsNullOrEmpty(row["order_date"].ToString()) && row["order_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["order_date"]);
        //                    strOrderDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strOrderDate = "00-000-0000";
        //                }
        //                RetObj.str_order_date = strOrderDate;
        //                RetObj.order_date = Common.CheckDateTimeNull(row["order_date"]);
        //                RetObj.order_status = Common.CheckStringNull(row["order_status"]);
        //                //if (!string.IsNullOrEmpty(row["delivery_date"].ToString()) && row["delivery_date"].ToString() != "")
        //                //{
        //                //    DateTime ChequeOldDate = Convert.ToDateTime(row["delivery_date"]);
        //                //    strDeliveryDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                //}
        //                //else
        //                //{
        //                //    strDeliveryDate = "00-000-0000";
        //                //}
        //                //RetObj.str_delivery_date = strDeliveryDate;
        //                RetObj.delivery_date = Common.CheckDateTimeNull(row["delivery_date"]);

        //                RetObj.total_items = Common.CheckIntegerNull(row["total_items"]);
        //                RetObj.total_qty = Common.CheckIntegerNull(row["total_qty"]);
        //                RetObj.total_kgs = Common.CheckDecimalNull(row["total_kgs"]);
        //                RetObj.sub_total_amount = Common.CheckDecimalNull(row["sub_total_amount"]);
        //                RetObj.discount = Common.CheckDecimalNull(row["discount"]);
        //                RetObj.tax = Common.CheckDecimalNull(row["tax"]);
        //                RetObj.total_amount = Common.CheckDecimalNull(row["total_amount"]);
        //                RetObj.created_by = Common.CheckStringNull(row["created_by"]);
        //                RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
        //                //if (!string.IsNullOrEmpty(row["created_date"].ToString()) && row["created_date"].ToString() != "")
        //                //{
        //                //    DateTime ChequeOldDate = Convert.ToDateTime(row["created_date"]);
        //                //    strCreatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                //}
        //                //else
        //                //{
        //                //    strCreatedDate = "00-000-0000";
        //                //}
        //                //RetObj.str_created_date = strCreatedDate;
        //                RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
        //                RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
        //                //if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
        //                //{
        //                //    DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
        //                //    strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                //}
        //                //else
        //                //{
        //                //    strUpdatedDate = "00-000-0000";
        //                //}
        //                //RetObj.str_updated_date = strUpdatedDate;
        //                RetObj.client_code = Common.CheckStringNull(row["client_code"]);
        //                RetObj.company = Common.CheckStringNull(row["company"]);
        //                RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
        //                RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
        //                RetObj.designation = Common.CheckStringNull(row["designation"]);


        //            }
        //            //add all get object in list 
        //            RetLst.Add(RetObj);

        //        }

        //        // status code shown assigining
        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        // if want to show in single object
        //        //RObj.EmployeeData = RetObj; 
        //        // if want to show in list of object
        //        RObj.OrderDataList = RetLst;
        //        return RObj; //finaly return the object
        //    }
        //    // if api calling error
        //    catch (Exception ex)
        //    {
        //        RObj.Status = 5;
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }

        //}


        /////////////////// GET ORDER mast by id //////////////////////////
        //public OrderMastReturn GetOrderMastById(OrderMastModel getModel) // get modal from user in getModel
        //{
        //    OrderMastReturn RObj = new OrderMastReturn(); // for result body object declaration
        //    int retstatus; string retmessage;

        //    try
        //    {
        //        //array for parameters
        //        var parm = new SqlParameter[5];
        //        //setting parameters value get from user
        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //        parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
        //        parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
        //        parm[4] = new SqlParameter("@order_id", SqlDbType.Int) { Value = getModel.order_id };

        //        //calling procedure
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_DO_Mast_Proc]", parm);
        //        retstatus = 50;
        //        retmessage = "No Data Found!";
        //        var strOrderDate = ""; // for string date
        //        var strDeliveryDate = "";
        //        var strCreatedDate = ""; // for string date
        //        var strUpdatedDate = ""; // for string date

        //        // for single object declaration
        //        var RetObj = new OrderMastModel();
        //        // for list object declaration
        //        List<OrderMastModel> RetLst = new List<OrderMastModel>();
        //        // getting all rows
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            RetObj = new OrderMastModel(); //this important for all rows

        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //            if (retstatus == 2)
        //            {
        //                //coloumn mapping

        //                var pMastID = Common.CheckIntegerNull(row["order_id"]);
        //                RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
        //                RetObj.order_id = Common.CheckIntegerNull(row["order_id"]);
        //                RetObj.order_no = Common.CheckIntegerNull(row["order_no"]);
        //                if (!string.IsNullOrEmpty(row["order_date"].ToString()) && row["order_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["order_date"]);
        //                    strOrderDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strOrderDate = "00-000-0000";
        //                }
        //                RetObj.str_order_date = strOrderDate;
        //                RetObj.order_date = Common.CheckDateTimeNull(row["order_date"]);
        //                RetObj.order_status = Common.CheckStringNull(row["order_status"]);
        //                //if (!string.IsNullOrEmpty(row["delivery_date"].ToString()) && row["delivery_date"].ToString() != "")
        //                //{
        //                //    DateTime ChequeOldDate = Convert.ToDateTime(row["delivery_date"]);
        //                //    strDeliveryDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                //}
        //                //else
        //                //{
        //                //    strDeliveryDate = "00-000-0000";
        //                //}
        //                //RetObj.str_delivery_date = strDeliveryDate;
        //                RetObj.delivery_date = Common.CheckDateTimeNull(row["delivery_date"]);

        //                RetObj.total_items = Common.CheckIntegerNull(row["total_items"]);
        //                RetObj.total_qty = Common.CheckIntegerNull(row["total_qty"]);
        //                RetObj.total_kgs = Common.CheckDecimalNull(row["total_kgs"]);
        //                RetObj.sub_total_amount = Common.CheckDecimalNull(row["sub_total_amount"]);
        //                RetObj.discount = Common.CheckDecimalNull(row["discount"]);
        //                RetObj.tax = Common.CheckDecimalNull(row["tax"]);
        //                RetObj.total_amount = Common.CheckDecimalNull(row["total_amount"]);
        //                RetObj.created_by = Common.CheckStringNull(row["created_by"]);
        //                RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
        //                //if (!string.IsNullOrEmpty(row["created_date"].ToString()) && row["created_date"].ToString() != "")
        //                //{
        //                //    DateTime ChequeOldDate = Convert.ToDateTime(row["created_date"]);
        //                //    strCreatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                //}
        //                //else
        //                //{
        //                //    strCreatedDate = "00-000-0000";
        //                //}
        //                //RetObj.str_created_date = strCreatedDate;
        //                RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
        //                RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
        //                //if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
        //                //{
        //                //    DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
        //                //    strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                //}
        //                //else
        //                //{
        //                //    strUpdatedDate = "00-000-0000";
        //                //}
        //                //RetObj.str_updated_date = strUpdatedDate;
        //                RetObj.client_code = Common.CheckStringNull(row["client_code"]);
        //                RetObj.company = Common.CheckStringNull(row["company"]);
        //                RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
        //                RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
        //                RetObj.designation = Common.CheckStringNull(row["designation"]);


        //            }
        //            //add all get object in list 
        //            // RetLst.Add(RetObj);

        //        }

        //        // status code shown assigining
        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        // if want to show in single object
        //        //RObj.EmployeeData = RetObj; 
        //        // if want to show in list of object
        //        RObj.OrderData = RetObj;
        //        return RObj; //finaly return the object
        //    }
        //    // if api calling error
        //    catch (Exception ex)
        //    {
        //        RObj.Status = 5;
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }

        //}




        /////////////////// GET ORDER DET BY ID   //////////////////////////
        //public OrderDetReturn GetOrderDetById(OrderDetModel getModel) // get modal from user in getModel
        //{
        //    OrderDetReturn RObj = new OrderDetReturn(); // for result body object declaration
        //    int retstatus; string retmessage;

        //    try
        //    {
        //        //array for parameters
        //        var parm = new SqlParameter[2];
        //        //setting parameters value get from user
        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("@order_id", SqlDbType.Int) { Value = getModel.order_id };

        //        //calling procedure
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_DO_Det_Proc]", parm);
        //        retstatus = 50;
        //        retmessage = "No Data Found!";
        //        var strOrderDate = ""; // for string date
        //        var strDeliveryDate = "";
        //        var strCreatedDate = ""; // for string date
        //        var strUpdatedDate = ""; // for string date

        //        // for single object declaration
        //        var RetObj = new OrderDetModel();
        //        // for list object declaration
        //        List<OrderDetModel> RetLst = new List<OrderDetModel>();
        //        // getting all rows
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            RetObj = new OrderDetModel(); //this important for all rows

        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //            if (retstatus == 2)
        //            {
        //                //coloumn mapping


        //                RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
        //                RetObj.order_id = Common.CheckIntegerNull(row["order_id"]);
        //                RetObj.order_det_id = Common.CheckIntegerNull(row["order_det_id"]);
        //                RetObj.item_id = Common.CheckIntegerNull(row["item_id"]);
        //                RetObj.item_desc = Common.CheckStringNull(row["item_desc"]);
        //                RetObj.qty = Common.CheckIntegerNull(row["qty"]);
        //                RetObj.size = Common.CheckDecimalNull(row["size"]);
        //                RetObj.kgs = Common.CheckDecimalNull(row["kgs"]);
        //                RetObj.item_price = Common.CheckDecimalNull(row["item_price"]);
        //                RetObj.qty_amount = Common.CheckDecimalNull(row["qty_amount"]);



        //            }
        //            //add all get object in list 
        //            RetLst.Add(RetObj);

        //        }

        //        // status code shown assigining
        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        // if want to show in single object
        //        //RObj.EmployeeData = RetObj; 
        //        // if want to show in list of object
        //        RObj.OrderDetDataList = RetLst;
        //        return RObj; //finaly return the object
        //    }
        //    // if api calling error
        //    catch (Exception ex)
        //    {
        //        RObj.Status = 5;
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }

        //}




        /////////////////// GET ORDER DETAIL BY ID//////////////////////////
        //public OrderMastReturn GetOrderDetailById(OrderMastModel getModel) // get modal from user in getModel
        //{
        //    OrderMastReturn RObj = new OrderMastReturn(); // for result body object declaration
        //    int retstatus; string retmessage;

        //    try
        //    {
        //        //array for parameters
        //        var parm = new SqlParameter[5];
        //        //setting parameters value get from user
        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //        parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
        //        parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
        //        parm[4] = new SqlParameter("@order_id", SqlDbType.Int) { Value = getModel.order_id };


        //        //calling procedure
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_DO_Mast_proc]", parm);
        //        retstatus = 50;
        //        retmessage = "No Data Found!";
        //        var strOrderDate = ""; // for string date
        //        var strDeliverDate = ""; // for string date
        //        var strCreatedDate = ""; // for string date
        //        var strUpdatedDate = ""; // for string date

        //        // for single object declaration
        //        var RetObj = new OrderMastModel();
        //        // for list object declaration
        //        List<OrderMastModel> RetLst = new List<OrderMastModel>();
        //        // getting all rows
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            RetObj = new OrderMastModel(); //this important for all rows

        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //            if (retstatus == 2)
        //            {
        //                //coloumn mapping

        //                var pMastID = Common.CheckIntegerNull(row["order_id"]);



        //                RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
        //                RetObj.order_id = Common.CheckIntegerNull(row["order_id"]);
        //                RetObj.order_no = Common.CheckIntegerNull(row["order_no"]);
        //                if (!string.IsNullOrEmpty(row["order_date"].ToString()) && row["order_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["order_date"]);
        //                    strOrderDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strOrderDate = "00-000-0000";
        //                }
        //                RetObj.str_order_date = strOrderDate;
        //                RetObj.order_date = Common.CheckDateTimeNull(row["order_date"]);
        //                RetObj.order_status = Common.CheckStringNull(row["order_status"]);
        //                if (!string.IsNullOrEmpty(row["delivery_date"].ToString()) && row["delivery_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["delivery_date"]);
        //                    strDeliverDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strDeliverDate = "00-000-0000";
        //                }
        //                RetObj.str_delivery_date = strDeliverDate;
        //                RetObj.delivery_date = Common.CheckDateTimeNull(row["delivery_date"]);
        //                RetObj.total_items = Common.CheckIntegerNull(row["total_items"]);
        //                RetObj.total_qty = Common.CheckIntegerNull(row["total_qty"]);
        //                RetObj.total_kgs = Common.CheckDecimalNull(row["total_kgs"]);
        //                RetObj.sub_total_amount = Common.CheckDecimalNull(row["sub_total_amount"]);
        //                RetObj.discount = Common.CheckDecimalNull(row["discount"]);
        //                RetObj.tax = Common.CheckDecimalNull(row["tax"]);
        //                RetObj.total_amount = Common.CheckDecimalNull(row["total_amount"]);
        //                RetObj.created_by = Common.CheckStringNull(row["created_by"]);
        //                if (!string.IsNullOrEmpty(row["created_date"].ToString()) && row["created_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["created_date"]);
        //                    strCreatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strCreatedDate = "00-000-0000";
        //                }
        //                RetObj.str_created_date = strCreatedDate;
        //                RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
        //                RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
        //                if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
        //                    strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strUpdatedDate = "00-000-0000";
        //                }
        //                RetObj.str_updated_date = strUpdatedDate;
        //                RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
        //                RetObj.client_code = Common.CheckStringNull(row["client_code"]);
        //                RetObj.company = Common.CheckStringNull(row["company"]);
        //                RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);


        //                // START FOR DETAIL TABLE LIST

        //                List<OrderDetModel> DetLst = new List<OrderDetModel>();

        //                var detParm = new SqlParameter[5];
        //                //setting parameters value get from user

        //                detParm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //                detParm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //                detParm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
        //                detParm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
        //                detParm[4] = new SqlParameter("@order_id", SqlDbType.Int) { Value = pMastID };

        //                var detsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_DO_det_proc]", detParm);

        //                foreach (DataRow rows in detsuser.Rows)
        //                {
        //                    var detObj = new OrderDetModel();
        //                    detObj.sr_id = Common.CheckIntegerNull(rows["sr_id"]);
        //                    detObj.order_id = Common.CheckIntegerNull(rows["order_id"]);
        //                    detObj.order_det_id = Common.CheckIntegerNull(rows["order_det_id"]);
        //                    detObj.item_id = Common.CheckIntegerNull(rows["item_id"]);
        //                    detObj.item_desc = Common.CheckStringNull(rows["item_desc"]);
        //                    detObj.qty = Common.CheckIntegerNull(rows["qty"]);
        //                    detObj.size = Common.CheckDecimalNull(rows["size"]);
        //                    detObj.kgs = Common.CheckDecimalNull(rows["kgs"]);
        //                    detObj.item_price = Common.CheckDecimalNull(rows["item_price"]);
        //                    detObj.qty_amount = Common.CheckDecimalNull(rows["qty_amount"]);

        //                    DetLst.Add(detObj);
        //                }

        //                RetObj.OrderDetail = DetLst;

        //                // END FOR DETAIL TABLE LIST




        //            }


        //        }

        //        // status code shown assigining
        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        // if want to show in single object
        //        //RObj.EmployeeData = RetObj; 
        //        // if want to show in list of object
        //        RObj.OrderData = RetObj;
        //        return RObj; //finaly return the object
        //    }
        //    // if api calling error
        //    catch (Exception ex)
        //    {
        //        RObj.Status = 5;
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }

        //}



        /////////////////// GET ALL ORDER DETAIL //////////////////////////
        //public OrderMastReturn GetAllOrderDetail(OrderMastModel getModel) // get modal from user in getModel
        //{
        //    OrderMastReturn RObj = new OrderMastReturn(); // for result body object declaration
        //    int retstatus; string retmessage;

        //    try
        //    {
        //        //array for parameters
        //        var parm = new SqlParameter[4];
        //        //setting parameters value get from user
        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //        parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
        //        parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

        //        //calling procedure
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_DO_Mast_proc]", parm);
        //        retstatus = 50;
        //        retmessage = "No Data Found!";
        //        var strOrderDate = ""; // for string date
        //        var strDeliverDate = ""; // for string date
        //        var strCreatedDate = ""; // for string date
        //        var strUpdatedDate = ""; // for string date

        //        // for single object declaration
        //        var RetObj = new OrderMastModel();
        //        // for list object declaration
        //        List<OrderMastModel> RetLst = new List<OrderMastModel>();
        //        // getting all rows
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            RetObj = new OrderMastModel(); //this important for all rows

        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //            if (retstatus == 2)
        //            {
        //                //coloumn mapping

        //                var pMastID = Common.CheckIntegerNull(row["order_id"]);



        //                RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
        //                RetObj.order_id = Common.CheckIntegerNull(row["order_id"]);
        //                RetObj.order_no = Common.CheckIntegerNull(row["order_no"]);
        //                if (!string.IsNullOrEmpty(row["order_date"].ToString()) && row["order_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["order_date"]);
        //                    strOrderDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strOrderDate = "00-000-0000";
        //                }
        //                RetObj.str_order_date = strOrderDate;
        //                RetObj.order_date = Common.CheckDateTimeNull(row["order_date"]);
        //                RetObj.order_status = Common.CheckStringNull(row["order_status"]);
        //                if (!string.IsNullOrEmpty(row["delivery_date"].ToString()) && row["delivery_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["delivery_date"]);
        //                    strDeliverDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strDeliverDate = "00-000-0000";
        //                }
        //                RetObj.str_delivery_date = strDeliverDate;
        //                RetObj.delivery_date = Common.CheckDateTimeNull(row["delivery_date"]);
        //                RetObj.total_items = Common.CheckIntegerNull(row["total_items"]);
        //                RetObj.total_qty = Common.CheckIntegerNull(row["total_qty"]);
        //                RetObj.total_kgs = Common.CheckDecimalNull(row["total_kgs"]);
        //                RetObj.sub_total_amount = Common.CheckDecimalNull(row["sub_total_amount"]);
        //                RetObj.discount = Common.CheckDecimalNull(row["discount"]);
        //                RetObj.tax = Common.CheckDecimalNull(row["tax"]);
        //                RetObj.total_amount = Common.CheckDecimalNull(row["total_amount"]);
        //                RetObj.created_by = Common.CheckStringNull(row["created_by"]);
        //                if (!string.IsNullOrEmpty(row["created_date"].ToString()) && row["created_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["created_date"]);
        //                    strCreatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strCreatedDate = "00-000-0000";
        //                }
        //                RetObj.str_created_date = strCreatedDate;
        //                RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
        //                RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
        //                if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
        //                    strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
        //                }
        //                else
        //                {
        //                    strUpdatedDate = "00-000-0000";
        //                }
        //                RetObj.str_updated_date = strUpdatedDate;
        //                RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
        //                RetObj.client_code = Common.CheckStringNull(row["client_code"]);
        //                RetObj.company = Common.CheckStringNull(row["company"]);
        //                RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);


        //                // START FOR DETAIL TABLE LIST

        //                List<OrderDetModel> DetLst = new List<OrderDetModel>();

        //                var detParm = new SqlParameter[5];
        //                //setting parameters value get from user

        //                detParm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //                detParm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //                detParm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
        //                detParm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
        //                detParm[4] = new SqlParameter("@order_id", SqlDbType.Int) { Value = pMastID };

        //                var detsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_DO_det_proc]", detParm);

        //                foreach (DataRow rows in detsuser.Rows)
        //                {
        //                    var detObj = new OrderDetModel();
        //                    retstatus = Common.CheckIntegerNull(rows["res_status"]);
        //                    retmessage = Common.CheckStringNull(rows["res_message"]);
        //                    if (retstatus == 2)
        //                    {
        //                        detObj.sr_id = Common.CheckIntegerNull(rows["sr_id"]);
        //                        detObj.order_id = Common.CheckIntegerNull(rows["order_id"]);
        //                        detObj.order_det_id = Common.CheckIntegerNull(rows["order_det_id"]);
        //                        detObj.item_id = Common.CheckIntegerNull(rows["item_id"]);
        //                        detObj.item_desc = Common.CheckStringNull(rows["item_desc"]);
        //                        detObj.qty = Common.CheckIntegerNull(rows["qty"]);
        //                        detObj.size = Common.CheckDecimalNull(rows["size"]);
        //                        detObj.kgs = Common.CheckDecimalNull(rows["kgs"]);
        //                        detObj.item_price = Common.CheckDecimalNull(rows["item_price"]);
        //                        detObj.qty_amount = Common.CheckDecimalNull(rows["qty_amount"]);
        //                    }



        //                    DetLst.Add(detObj);
        //                }

        //                RetObj.OrderDetail = DetLst;

        //                // END FOR DETAIL TABLE LIST




        //            }
        //            //add all get object in list 
        //            RetLst.Add(RetObj);

        //        }

        //        // status code shown assigining
        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        // if want to show in single object
        //        //RObj.EmployeeData = RetObj; 
        //        // if want to show in list of object
        //        RObj.OrderDataList = RetLst;
        //        return RObj; //finaly return the object
        //    }
        //    // if api calling error
        //    catch (Exception ex)
        //    {
        //        RObj.Status = 5;
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }

        //}



        /////////////////// UPDATE LEAVES IN LIST //////////////////////////
        //public OrderMastReturn AUDOrders(OrderMastModel getModel) // Change parameter to accept a list of EmployeeModels
        //{
        //    OrderMastReturn RObj = new OrderMastReturn(); // For result body object declaration

        //    int retstatus; string retmessage; int OrderId;
        //    try
        //    {
        //        retstatus = 50;
        //        retmessage = "No Data Found!";
        //        OrderId = 0;

        //        // Array for parameters
        //        var parm = new SqlParameter[21];
        //        // Setting parameters value from the user input (getModel)
        //        parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
        //        parm[2] = new SqlParameter("order_id", SqlDbType.Int) { Value = getModel.order_id };
        //        parm[3] = new SqlParameter("order_no", SqlDbType.Int) { Value = getModel.order_no };
        //        parm[4] = new SqlParameter("order_date", SqlDbType.DateTime) { Value = getModel.order_date };
        //        parm[5] = new SqlParameter("order_status", SqlDbType.VarChar) { Value = getModel.order_status };
        //        parm[6] = new SqlParameter("delivery_date", SqlDbType.DateTime) { Value = getModel.delivery_date };
        //        parm[7] = new SqlParameter("total_items", SqlDbType.Int) { Value = getModel.total_items };
        //        parm[8] = new SqlParameter("total_qty", SqlDbType.Int) { Value = getModel.total_qty };
        //        parm[9] = new SqlParameter("total_kgs", SqlDbType.Decimal) { Value = getModel.total_kgs };
        //        parm[10] = new SqlParameter("sub_total_amount", SqlDbType.Decimal) { Value = getModel.sub_total_amount };
        //        parm[11] = new SqlParameter("discount", SqlDbType.Decimal) { Value = getModel.discount };
        //        parm[12] = new SqlParameter("tax", SqlDbType.Decimal) { Value = getModel.tax };
        //        parm[13] = new SqlParameter("total_amount", SqlDbType.Decimal) { Value = getModel.total_amount };
        //        parm[14] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
        //        parm[15] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date };
        //        parm[16] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
        //        parm[17] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date };
        //        parm[18] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //        parm[19] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
        //        parm[20] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };



        //        // Call the stored procedure for each employee
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_DO_Mast_Proc]", parm);
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //            OrderId = Common.CheckIntegerNull(row["new_order_id"]);

        //            if (retstatus == 2)
        //            {
        //                List<OrderDetModel> DObj = new List<OrderDetModel>();
        //                DObj = getModel.OrderDetail;

        //                OrderDetReturn RdObj = new OrderDetReturn(); // For result body object declaration

        //                int detRetStatus; string detRetRetmessage;
        //                try
        //                {
        //                    detRetStatus = 50;
        //                    detRetRetmessage = "No Data Found!";
        //                    foreach (var detgetModel in DObj) // Iterate over each employee model in the list
        //                    {
        //                        // Array for parameters
        //                        var detparm = new SqlParameter[11];
        //                        // Setting parameters value from the user input (getModel)
        //                        detparm[0] = new SqlParameter("sr_id", SqlDbType.Int) { Value = detgetModel.sr_id };
        //                        detparm[1] = new SqlParameter("order_id", SqlDbType.Int) { Value = detgetModel.order_id };
        //                        detparm[2] = new SqlParameter("order_det_id", SqlDbType.Int) { Value = detgetModel.order_det_id };
        //                        detparm[3] = new SqlParameter("item_id", SqlDbType.Int) { Value = detgetModel.item_id };
        //                        detparm[4] = new SqlParameter("qty", SqlDbType.Int) { Value = detgetModel.qty };
        //                        detparm[5] = new SqlParameter("size", SqlDbType.Decimal) { Value = detgetModel.size };
        //                        detparm[6] = new SqlParameter("kgs", SqlDbType.Decimal) { Value = detgetModel.kgs };
        //                        detparm[7] = new SqlParameter("item_price", SqlDbType.Decimal) { Value = detgetModel.item_price };
        //                        detparm[8] = new SqlParameter("qty_amount", SqlDbType.Decimal) { Value = detgetModel.qty_amount };
        //                        detparm[9] = new SqlParameter("Operation", SqlDbType.Int) { Value = detgetModel.Operation };
        //                        detparm[10] = new SqlParameter("new_order_id", SqlDbType.Int) { Value = OrderId };



        //                        // Call the stored procedure for each employee
        //                        var detdsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_DO_Det_proc]", detparm);

        //                    }


        //                }
        //                // If an error occurs during API execution
        //                catch (Exception ex)
        //                {
        //                    RdObj.Status = 5;
        //                    RdObj.Message = "Error Found, " + ex.ToString();
        //                }


        //            }
        //        }

        //        RObj.Status = retstatus; // Success status
        //        RObj.Message = retmessage;
        //        return RObj;
        //    }

        //    // If an error occurs during API execution
        //    catch (Exception ex)
        //    {
        //        RObj.Status = 5;
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }
        //}


        /////////////////// UPDATE ORDER STATUS IN LIST //////////////////////////
        //public OrderMastReturn UpdateOrdersStatus(OrderMastModel getModel) // Change parameter to accept a list of EmployeeModels
        //{
        //    OrderMastReturn RObj = new OrderMastReturn(); // For result body object declaration

        //    int retstatus; string retmessage;
        //    try
        //    {
        //        retstatus = 50;
        //        retmessage = "No Data Found!";

        //        // Array for parameters
        //        var parm = new SqlParameter[3];
        //        // Setting parameters value from the user input (getModel)
        //        parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("order_id", SqlDbType.Int) { Value = getModel.order_id };
        //        parm[2] = new SqlParameter("order_status", SqlDbType.VarChar) { Value = getModel.order_status };


        //        // Call the stored procedure for each employee
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_DO_Mast_proc]", parm);
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //        }


        //        RObj.Status = retstatus; // Success status
        //        RObj.Message = retmessage;
        //        return RObj;
        //    }
        //    // If an error occurs during API execution
        //    catch (Exception ex)
        //    {
        //        RObj.Status = 5;
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }
        //}


        #endregion  
    }
}