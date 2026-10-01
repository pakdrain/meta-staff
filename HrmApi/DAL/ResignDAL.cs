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
    public class ResignDAL
    {
        private string _ConnString; //variable for connection string
        public ResignDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Resigns
        ///////////////// GET ALL LEAVES DAL //////////////////////////

        public ResignReturn GetAllResigns(ResignModel getModel) // get modal from user in getModel
        {
            ResignReturn RObj = new ResignReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.Int) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_resignation_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";

                // for single object declaration
                var RetObj = new ResignModel();
                // for list object declaration
                List<ResignModel> RetLst = new List<ResignModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new ResignModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping

                       
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.resig_id = Common.CheckIntegerNull(row["resig_id"]);
                        RetObj.reason = Common.CheckStringNull(row["reason"]);
                        RetObj.resig_emp_id = Common.CheckStringNull(row["resig_emp_id"]);
                        RetObj.resig_status = Common.CheckStringNull(row["resig_status"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
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
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
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
                RObj.ResignDataList = RetLst;
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
        public ResignReturn AUDResigns(List<ResignModel> getModels) // Change parameter to accept a list of EmployeeModels
        {
            ResignReturn RObj = new ResignReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";
                foreach (var getModel in getModels) // Iterate over each employee model in the list
                {
                    // Array for parameters
                    var parm = new SqlParameter[15];
                    // Setting parameters value from the user input (getModel)
                    parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                    parm[1] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                    parm[2] = new SqlParameter("reason", SqlDbType.VarChar, 255) { Value = getModel.reason ?? (object)DBNull.Value };
                    parm[3] = new SqlParameter("resig_emp_id", SqlDbType.VarChar, 50) { Value = getModel.resig_emp_id ?? (object)DBNull.Value };
                    parm[4] = new SqlParameter("resig_status", SqlDbType.VarChar, 50) { Value = getModel.resig_status ?? (object)DBNull.Value };
                    parm[5] = new SqlParameter("department", SqlDbType.VarChar, 100) { Value = getModel.department ?? (object)DBNull.Value };
                    parm[6] = new SqlParameter("designation", SqlDbType.VarChar, 100) { Value = getModel.designation ?? (object)DBNull.Value };
                    parm[7] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date ?? (object)DBNull.Value };
                    parm[8] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date ?? (object)DBNull.Value };
                    parm[9] = new SqlParameter("created_by", SqlDbType.VarChar, 50) { Value = getModel.created_by ?? (object)DBNull.Value };
                    parm[10] = new SqlParameter("updated_by", SqlDbType.VarChar, 50) { Value = getModel.updated_by ?? (object)DBNull.Value };
                    parm[11] = new SqlParameter("client_code", SqlDbType.VarChar, 50) { Value = getModel.client_code ?? (object)DBNull.Value };
                    parm[12] = new SqlParameter("company", SqlDbType.VarChar, 100) { Value = getModel.company ?? (object)DBNull.Value };
                    parm[13] = new SqlParameter("emp_id", SqlDbType.VarChar, 50) { Value = getModel.emp_id ?? (object)DBNull.Value };
                    parm[14] = new SqlParameter("resig_id", SqlDbType.Int, 50) { Value = getModel.resig_id  };


                    // Call the stored procedure for each employee
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_resignation_proc]", parm);
                    foreach (DataRow row in dsuser.Rows)
                    {
                        retstatus = Common.CheckIntegerNull(row["res_status"]);
                        retmessage = Common.CheckStringNull(row["res_message"]);
                    }
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


        public ResignReturn AUDSingleResigns(ResignModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            ResignReturn RObj = new ResignReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";
                
                    // Array for parameters
                    var parm = new SqlParameter[15];
                    // Setting parameters value from the user input (getModel)
                    parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                    parm[1] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                    parm[2] = new SqlParameter("reason", SqlDbType.VarChar, 255) { Value = getModel.reason ?? (object)DBNull.Value };
                    parm[3] = new SqlParameter("resig_emp_id", SqlDbType.VarChar, 50) { Value = getModel.resig_emp_id ?? (object)DBNull.Value };
                    parm[4] = new SqlParameter("resig_status", SqlDbType.VarChar, 50) { Value = getModel.resig_status ?? (object)DBNull.Value };
                    parm[5] = new SqlParameter("department", SqlDbType.VarChar, 100) { Value = getModel.department ?? (object)DBNull.Value };
                    parm[6] = new SqlParameter("designation", SqlDbType.VarChar, 100) { Value = getModel.designation ?? (object)DBNull.Value };
                    parm[7] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date ?? (object)DBNull.Value };
                    parm[8] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date ?? (object)DBNull.Value };
                    parm[9] = new SqlParameter("created_by", SqlDbType.VarChar, 50) { Value = getModel.created_by ?? (object)DBNull.Value };
                    parm[10] = new SqlParameter("updated_by", SqlDbType.VarChar, 50) { Value = getModel.updated_by ?? (object)DBNull.Value };
                    parm[11] = new SqlParameter("client_code", SqlDbType.VarChar, 50) { Value = getModel.client_code ?? (object)DBNull.Value };
                    parm[12] = new SqlParameter("company", SqlDbType.VarChar, 100) { Value = getModel.company ?? (object)DBNull.Value };
                    parm[13] = new SqlParameter("emp_id", SqlDbType.VarChar, 50) { Value = getModel.emp_id ?? (object)DBNull.Value };
                    parm[14] = new SqlParameter("resig_id", SqlDbType.Int, 50) { Value = getModel.resig_id };


                    // Call the stored procedure for each employee
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_resignation_proc]", parm);
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