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

namespace HrmApi.DAL
{
    public class VehicleDAL
    {
        private string _ConnString; //variable for connection string
        public VehicleDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Vehicle
        ///////////////// GET ALL LEAVES DAL //////////////////////////

        public VehicleReturn GetAllVehicle(VehicleModel getModel) // get modal from user in getModel
        {
            VehicleReturn RObj = new VehicleReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                
                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_vehicle_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";
                var strPurchaseDate = "";

                // for single object declaration
                var RetObj = new VehicleModel();
                // for list object declaration
                List<VehicleModel> RetLst = new List<VehicleModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new VehicleModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.vehicle_id = Common.CheckIntegerNull(row["vehicle_id"]);
                        RetObj.vehicle_no = Common.CheckStringNull(row["vehicle_no"]);
                        RetObj.own_company = Common.CheckStringNull(row["own_company"]);
                        RetObj.first_tag_comp = Common.CheckStringNull(row["first_tag_comp"]);
                        RetObj.first_tag_emp = Common.CheckStringNull(row["first_tag_emp"]);
                        RetObj.tag_company = Common.CheckStringNull(row["tag_company"]);
                        RetObj.tag_person = Common.CheckStringNull(row["tag_person"]);
                        RetObj.challan_no = Common.CheckIntegerNull(row["challan_no"]);
                        RetObj.reg_no = Common.CheckStringNull(row["reg_no"]);
                        RetObj.chasis_no = Common.CheckIntegerNull(row["chasis_no"]);
                        RetObj.engine_no = Common.CheckIntegerNull(row["engine_no"]);
                        RetObj.brand = Common.CheckStringNull(row["brand"]);
                        RetObj.model = Common.CheckStringNull(row["model"]);
                        RetObj.model_year = Common.CheckIntegerNull(row["model_year"]);
                        RetObj.owner_no = Common.CheckIntegerNull(row["owner_no"]);
                        RetObj.pso_card_no = Common.CheckIntegerNull(row["pso_card_no"]);
                        RetObj.fuel_limit = Common.CheckIntegerNull(row["fuel_limit"]);
                        RetObj.purchase_date = Common.CheckDateTimeNull(row["purchase_date"]);
                        if (!string.IsNullOrEmpty(row["purchase_date"].ToString()) && row["purchase_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["purchase_date"]);
                            strPurchaseDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strPurchaseDate = "00-000-0000";
                        }
                        RetObj.str_purchase_date = strPurchaseDate;
                        RetObj.color = Common.CheckStringNull(row["color"]);
                        RetObj.registration_fee = Common.CheckIntegerNull(row["registration_fee"]);
                        RetObj.number_plate_price = Common.CheckIntegerNull(row["number_plate_price"]);
                        RetObj.reg_card_fee = Common.CheckIntegerNull(row["reg_card_fee"]);
                        RetObj.token = Common.CheckStringNull(row["token"]);
                        RetObj.misc = Common.CheckStringNull(row["misc"]);
                        RetObj.status = Common.CheckStringNull(row["status"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
                        if (!string.IsNullOrEmpty(row["created_date"].ToString()) && row["created_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["created_date"]);
                            strCreatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strCreatedDate = "00-000-0000";
                        }
                        RetObj.str_created_date = strCreatedDate;
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdatedDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdatedDate;
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);





                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.VehicleDataList = RetLst;
                return RObj; //finaly return the object
            }
            // if api calling error
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }

        }

        ///////////////// UPDATE LEAVES IN LIST //////////////////////////
        public VehicleReturn AUDVehicle(VehicleModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            VehicleReturn RObj = new VehicleReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";
             
                    // Array for parameters
                    var parm = new SqlParameter[33];
                    // Setting parameters value from the user input (getModel)
                    parm[0] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                    parm[1] = new SqlParameter("vehicle_id", SqlDbType.Int) { Value = getModel.vehicle_id };
                    parm[2] = new SqlParameter("vehicle_no", SqlDbType.VarChar, 50) { Value = getModel.vehicle_no };
                    parm[3] = new SqlParameter("own_company", SqlDbType.VarChar, 50) { Value = getModel.own_company };
                    parm[4] = new SqlParameter("first_tag_comp", SqlDbType.VarChar, 50) { Value = getModel.first_tag_comp };
                    parm[5] = new SqlParameter("first_tag_emp", SqlDbType.VarChar, 50) { Value = getModel.first_tag_emp };
                    parm[6] = new SqlParameter("tag_company", SqlDbType.VarChar, 50) { Value = getModel.tag_company };
                    parm[7] = new SqlParameter("tag_person", SqlDbType.VarChar, 50) { Value = getModel.tag_person };
                    parm[8] = new SqlParameter("challan_no", SqlDbType.Int) { Value = getModel.challan_no };
                    parm[9] = new SqlParameter("reg_no", SqlDbType.VarChar) { Value = getModel.reg_no }; // -1 for varchar(MAX)
                    parm[10] = new SqlParameter("chasis_no", SqlDbType.Int) { Value = getModel.chasis_no };
                    parm[11] = new SqlParameter("engine_no", SqlDbType.Int) { Value = getModel.engine_no };
                    parm[12] = new SqlParameter("brand", SqlDbType.VarChar, 50) { Value = getModel.brand };
                    parm[13] = new SqlParameter("model", SqlDbType.VarChar, 50) { Value = getModel.model };
                    parm[14] = new SqlParameter("model_year", SqlDbType.Int) { Value = getModel.model_year };
                    parm[15] = new SqlParameter("owner_no", SqlDbType.Int) { Value = getModel.owner_no };
                    parm[16] = new SqlParameter("pso_card_no", SqlDbType.Int) { Value = getModel.pso_card_no };
                    parm[17] = new SqlParameter("fuel_limit", SqlDbType.Int) { Value = getModel.fuel_limit };
                    parm[18] = new SqlParameter("purchase_date", SqlDbType.DateTime) { Value = getModel.purchase_date };
                    parm[19] = new SqlParameter("color", SqlDbType.VarChar, 50) { Value = getModel.color };
                    parm[20] = new SqlParameter("registration_fee", SqlDbType.Int) { Value = getModel.registration_fee };
                    parm[21] = new SqlParameter("number_plate_price", SqlDbType.Int) { Value = getModel.number_plate_price };
                    parm[22] = new SqlParameter("reg_card_fee", SqlDbType.Int) { Value = getModel.reg_card_fee };
                    parm[23] = new SqlParameter("token", SqlDbType.VarChar, 50) { Value = getModel.token };
                    parm[24] = new SqlParameter("misc", SqlDbType.VarChar, 50) { Value = getModel.misc };
                    parm[25] = new SqlParameter("created_by", SqlDbType.VarChar, 50) { Value = getModel.created_by };
                    parm[26] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date };
                    parm[27] = new SqlParameter("updated_by", SqlDbType.VarChar, 50) { Value = getModel.updated_by };
                    parm[28] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date };
                    parm[29] = new SqlParameter("client_code", SqlDbType.VarChar, 50) { Value = getModel.client_code };
                    parm[30] = new SqlParameter("company", SqlDbType.VarChar, 50) { Value = getModel.company };
                    parm[31] = new SqlParameter("emp_id", SqlDbType.VarChar, 50) { Value = getModel.emp_id };
                    parm[32] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };



                    // Call the stored procedure for each employee
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_vehicle_proc]", parm);
                    foreach (DataRow row in dsuser.Rows)
                    {
                        retstatus = Common.CheckIntegerNull(row["res_status"]);
                        retmessage = Common.CheckStringNull(row["res_message"]);
                    }
                

                RObj.Status = retstatus; // Success status
                RObj.Message = retmessage;
                return RObj;
            }
            // If an error occurs during API execution
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }
        }

        #endregion
    }
}