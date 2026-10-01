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
    public class ItemsDAL
    {
        private string _ConnString; //variable for connection string
        public ItemsDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Items
        ///////////////// GET ALL ITEMS //////////////////////////

        //public ItemReturn GetAllItemm(ItemModel getModel) // get modal from user in getModel
        //{
        //    ItemReturn RObj = new ItemReturn(); // for result body object declaration
        //    int retstatus; string retmessage;

        //    try
        //    {
        //        //array for parameters
        //        var parm = new SqlParameter[4];
        //        //setting parameters value get from user
        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
        //        parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
        //        parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
        //        parm[3] = new SqlParameter("@emp_id", SqlDbType.Int) { Value = getModel.emp_id };

        //        //calling procedure
        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_items_proc]", parm);
        //        retstatus = 50;
        //        retmessage = "No Data Found!";
        //        var strCreatedDate = ""; // for string date
        //        var strUpdatedDate = ""; // for string date

        //        // for single object declaration
        //        var RetObj = new ItemModel();
        //        // for list object declaration
        //        List<ItemModel> RetLst = new List<ItemModel>();
        //        // getting all rows
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            RetObj = new ItemModel(); //this important for all rows

        //            retstatus = Common.CheckIntegerNull(row["res_status"]);
        //            retmessage = Common.CheckStringNull(row["res_message"]);
        //            if (retstatus == 2)
        //            {
        //                //coloumn mapping

        //                RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
        //                RetObj.item_id = Common.CheckIntegerNull(row["item_id"]);
        //                RetObj.item_code = Common.CheckStringNull(row["item_code"]);
        //                RetObj.item_desc = Common.CheckStringNull(row["item_desc"]);
        //                RetObj.uom = Common.CheckStringNull(row["uom"]);
        //                RetObj.is_active = Common.CheckIntegerNull(row["is_active"]);
        //                RetObj.weight_in_kg = Common.CheckIntegerNull(row["weight_in_kg"]);
        //                RetObj.rate = Common.CheckDecimalNull(row["rate"]);
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
        //        RObj.ItemsDataList = RetLst;
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

        public async Task<ItemReturn> GetAllItems(ItemModel getModel)
        {
            ItemReturn RObj = new ItemReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var itemRequest = new ItemModel
            {
                Operation = 1,
                company = getModel.company,
                emp_id = getModel.emp_id,
                item_id = getModel.item_id,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/inv-items";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(itemRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ItemApiResponse>(jsonResponse);

                        List<ItemModel> RetLst = new List<ItemModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            ItemModel RetObj = new ItemModel
                            {
                                item_id = item.item_id,
                                item_code = item.item_code,
                                item_desc = item.item_desc,
                                weight_in_kg = item.weight_in_kg,
                                uom = item.uom,
                                rate = item.rate,
                                discount = item.discount,
                                tax = item.tax,
                                discount_per = item.discount_per


                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.ItemsDataList = RetLst;
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

        public async Task<ItemReturn> GetItemById(ItemModel getModel)
        {
            ItemReturn RObj = new ItemReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var itemRequest = new ItemModel
            {
                Operation = 5,
                company = getModel.company,
                emp_id = getModel.emp_id,
                item_id = getModel.item_id,
                qty =   getModel.qty

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/inv-items";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(itemRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ItemApiResponse>(jsonResponse);

                        List<ItemModel> RetLst = new List<ItemModel>();

                        foreach (var item in apiResponse.Items)
                        {


                            ItemModel RetObj = new ItemModel
                            {
                                item_id = item.item_id,
                                item_code = item.item_code,
                                item_desc = item.item_desc,
                                weight_in_kg = item.weight_in_kg,
                                uom = item.uom,
                                rate = item.rate,
                                amount = item.amount,
                                tax = item.tax,
                                discount = item.discount,
                                discount_per = item.discount_per,
                                total_amount = item.total_amount,

                            };

                            RetLst.Add(RetObj);
                            retstatus = 2;
                            retmessage = "Data fetched successfully !";
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.ItemsDataList = RetLst;
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