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
using System.Security.AccessControl;

namespace HrmApi.DAL
{
    public class ExtensionDAL
    {
        private string _ConnString; //variable for connection string
        public ExtensionDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region  Extensions
        ///////////////// GET ALL Extension DAL //////////////////////////

        public ExtensionReturn GetAllExtension(ExtensionModel getModel) // get modal from user in getModel
        {
            ExtensionReturn RObj = new ExtensionReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 1 };
                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@department", SqlDbType.VarChar) { Value = getModel.department };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Ext_Extension_Proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";

                var strCreatedDate = "";
                var strUpdateDate = "";


                // for single object declaration
                var RetObj = new ExtensionModel();
                // for list object declaration
                List<ExtensionModel> RetLst = new List<ExtensionModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new ExtensionModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.extension_id = Common.CheckIntegerNull(row["extension_id"]);
                        RetObj.extension_no = Common.CheckIntegerNull(row["extension_no"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.extension = Common.CheckStringNull(row["extension"]);
                        RetObj.ip_phone = Common.CheckStringNull(row["ip_phone"]);
                        RetObj.extension_type = Common.CheckStringNull(row["extension_type"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);

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
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdateDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdateDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdateDate;
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
                RObj.ExtensionDataList = RetLst;
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

        ///////////////// GET ALL Extension DAL //////////////////////////

        public ExtensionReturn GetSingleExtension(ExtensionModel getModel) // get modal from user in getModel
        {
            ExtensionReturn RObj = new ExtensionReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[5];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 5 };
                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@department", SqlDbType.VarChar) { Value = getModel.department };
                parm[4] = new SqlParameter("@extension_id", SqlDbType.Int) { Value = getModel.extension_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Ext_Extension_Proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";

                var strCreatedDate = "";
                var strUpdateDate = "";


                // for single object declaration
                var RetObj = new ExtensionModel();
                // for list object declaration
                List<ExtensionModel> RetLst = new List<ExtensionModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new ExtensionModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.extension_id = Common.CheckIntegerNull(row["extension_id"]);
                        RetObj.extension_no = Common.CheckIntegerNull(row["extension_no"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.extension = Common.CheckStringNull(row["extension"]);
                        RetObj.ip_phone = Common.CheckStringNull(row["ip_phone"]);
                        RetObj.extension_type = Common.CheckStringNull(row["extension_type"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
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
                        if (!string.IsNullOrEmpty(row["updated_date"].ToString()) && row["updated_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["updated_date"]);
                            strUpdateDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strUpdateDate = "00-000-0000";
                        }
                        RetObj.str_updated_date = strUpdateDate;
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
                RObj.ExtensionDataList = RetLst;
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


        ///////////////// add UPDATE delete Extension  //////////////////////////
        public ExtensionReturn AUDSingleExtension(ExtensionModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            ExtensionReturn RObj = new ExtensionReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[14];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("extension_id", SqlDbType.Int) { Value = getModel.extension_id };
                parm[2] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[3] = new SqlParameter("emp_name", SqlDbType.VarChar) { Value = getModel.emp_name };
                parm[4] = new SqlParameter("designation", SqlDbType.VarChar) { Value = getModel.designation };
                parm[5] = new SqlParameter("department", SqlDbType.VarChar) { Value = getModel.department };
                parm[6] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                parm[7] = new SqlParameter("extension", SqlDbType.VarChar) { Value = getModel.extension };
                parm[8] = new SqlParameter("ip_phone", SqlDbType.VarChar) { Value = getModel.ip_phone };
                parm[9] = new SqlParameter("extension_type", SqlDbType.VarChar) { Value = getModel.extension_type };
                parm[10] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                parm[11] = new SqlParameter("str_created_date", SqlDbType.VarChar) { Value = getModel.str_created_date };
                parm[12] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
                parm[13] = new SqlParameter("str_updated_date", SqlDbType.VarChar) { Value = getModel.str_updated_date };

                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Ext_Extension_Proc]", parm);
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