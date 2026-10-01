using HrmApi.Models;
using HrmApi.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static HrmApi.DAL.SQLHelper;

namespace HrmApi.DAL
{
    public class AuthenticationDAL
    {
        private string _ConnString; //variable for connection string
        public AuthenticationDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Client

        public ClientReturn ClientVerification(ClientModel getModel)
        {
            ClientReturn RObj = new ClientReturn();
            int retstatus; string retmessage;
            try
            {
                var parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Client_Ver]", parm);
                var RetObj = new ClientModel();
                retstatus = 50;
                retmessage = "No Data Found!";
                foreach (DataRow row in dsuser.Rows)

                {
                    RetObj = new ClientModel();
                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        RetObj.client_id = Common.CheckIntegerNull(row["client_id"]);
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.client_name = Common.CheckStringNull(row["client_name"]);
                        RetObj.is_active = Common.CheckIntegerNull(row["is_active"]);

                    }
                }
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                RObj.ClientData = RetObj;
                // if want to show in list of object
                //RObj.EmployeeDataList = RetLst;
                return RObj;
            }
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;


            }
        }

        #endregion

        #region Authentication
        public EmployeeReturn MobileAuthentication(EmployeeModel getModel)
        {
            EmployeeReturn RObj = new EmployeeReturn();
            int retstatus; string retmessage; string strDOJ;
            try
            {
                var parm = new SqlParameter[7];
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[3] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[2] = new SqlParameter("@pass", SqlDbType.VarChar) { Value = getModel.pass };
                parm[4] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[5] = new SqlParameter("@device_id", SqlDbType.VarChar) { Value = getModel.device_id };
                parm[6] = new SqlParameter("@login_from", SqlDbType.VarChar) { Value = getModel.login_from };

                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Mobile_Auth]", parm);

                var RetObj = new EmployeeModel();
                retstatus = 5;
                retmessage = "No Data Found!";
                strDOJ = "";
                foreach (DataRow row in dsuser.Rows)

                {
                    RetObj = new EmployeeModel();
                    retstatus = (int)row["res_status"];
                    retmessage = Common.CheckStringNull(row["res_message"]);

                    if (retstatus == 2)
                    {
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.emp_id_to_show = Common.CheckStringNull(row["emp_id_to_show"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.branch = Common.CheckStringNull(row["branch"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        // RetObj.creation_date = Common.CheckDateTimeNull(row["creation_date"]);
                        // RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        //  RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
                        //  RetObj.update_by = Common.CheckStringNull(row["update_by"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.emp_img = Common.CheckStringNull(row["emp_img"]);
                        //   RetObj.update_date = Common.CheckDateTimeNull(row["shift_start_time"]);
                        //  RetObj.update_date = Common.CheckDateTimeNull(row["shift_end_time"]);
                        RetObj.leave_allow = Common.CheckDecimalNull(row["leave_allow"]);
                        RetObj.curr_salary = Common.CheckIntegerNull(row["curr_salary"]);
                        RetObj.pass = Common.CheckStringNull(row["pass"]);
                        RetObj.loan_limit = Common.CheckDecimalNull(row["loan_limit"]);
                        RetObj.cnic = Common.CheckStringNull(row["cnic"]);
                        RetObj.per_month_order_limit = Common.CheckIntegerNull(row["per_month_order_limit"]);
                        RetObj.this_month_order_limit = Common.CheckIntegerNull(row["this_month_order_limit"]);
                        RetObj.per_month_qty_limit = Common.CheckIntegerNull(row["per_month_qty_limit"]);
                        RetObj.this_month_qty_limit = Common.CheckIntegerNull(row["this_month_qty_limit"]);
                        RetObj.dicount_percetage = Common.CheckIntegerNull(row["dicount_percetage"]);
                        RetObj.doj = Common.CheckDateTimeNull(row["doj"]);
                        if (!string.IsNullOrEmpty(row["doj"].ToString()) && row["doj"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["doj"]);
                            strDOJ = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strDOJ = "00-000-0000";
                        }
                        RetObj.str_doj = strDOJ;
                        RetObj.ofc_lat = Common.CheckStringNull(row["ofc_lat"]);
                        RetObj.ofc_lon = Common.CheckStringNull(row["ofc_lon"]);
                        RetObj.mobile_phone = Common.CheckStringNull(row["mobile_phone"]);
                        RetObj.first_log_in = Common.CheckIntegerNull(row["first_log_in"]);
                        RetObj.att_radius = Common.CheckIntegerNull(row["att_radius"]);
                        RetObj.auto_logout = Common.CheckIntegerNull(row["auto_logout"]);

                    }



                }
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                RObj.EmployeeData = RetObj;
                // if want to show in list of object
                //RObj.EmployeeDataList = RetLst;
                return RObj;
            }
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }
        }


        public EmployeeReturn ChangePaswword(EmployeeModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            EmployeeReturn RObj = new EmployeeReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[8];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 6 };
                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[2] = new SqlParameter("@pass", SqlDbType.VarChar) { Value = getModel.pass };
                parm[3] = new SqlParameter("@current_pass", SqlDbType.VarChar) { Value = getModel.current_pass };
                parm[4] = new SqlParameter("@new_pass", SqlDbType.VarChar) { Value = getModel.new_pass };
                parm[5] = new SqlParameter("@confirm_new_pass", SqlDbType.VarChar) { Value = getModel.confirm_new_pass };
                parm[6] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[7] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Mobile_Auth]", parm);


                var RetObj = new EmployeeModel();
                retstatus = 5;
                retmessage = "No Data Found!";

                foreach (DataRow row in dsuser.Rows)

                {
                    RetObj = new EmployeeModel();
                    retstatus = (int)row["res_status"];
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

        public EmployeeReturn AutoLogut(EmployeeModel getModel)
        {
            EmployeeReturn RObj = new EmployeeReturn();
            int retstatus; string retmessage;
            try
            {
                var parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 10 };
                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Mobile_Auth]", parm);

                var RetObj = new EmployeeModel();
                retstatus = 5;
                retmessage = "No Data Found!";
            
                foreach (DataRow row in dsuser.Rows)

                {
                    RetObj = new EmployeeModel();
                    retstatus = (int)row["res_status"];
                    retmessage = Common.CheckStringNull(row["res_message"]);

                    if (retstatus == 2)
                    {

                        RetObj.auto_logout = Common.CheckIntegerNull(row["auto_logout"]);

                    }



                }
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                RObj.EmployeeData = RetObj;
                // if want to show in list of object
                //RObj.EmployeeDataList = RetLst;
                return RObj;
            }
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