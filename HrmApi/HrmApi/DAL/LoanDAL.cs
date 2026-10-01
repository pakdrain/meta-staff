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
    public class LoanDAL
    {
        private string _ConnString; //variable for connection string
        public LoanDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Loans
        ///////////////// GET ALL LEAVES DAL //////////////////////////

        public LoanReturn GetAllLoans(LoanModel getModel) // get modal from user in getModel
        {
            LoanReturn RObj = new LoanReturn(); // for result body object declaration
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
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_loan_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strLoanStartDate = ""; // for string date
                var strCreatedDate = ""; // for string date
                var strUpdatedDate = ""; // for string date
                var avail_limit = 0;
                var prev_loan = 0;
                var no_of_loan = 0;
                var balance = 0;

                // for single object declaration
                var RetObj = new LoanModel();
                // for list object declaration
                List<LoanModel> RetLst = new List<LoanModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new LoanModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    avail_limit = Common.CheckIntegerNull(row["avail_limit"]);
                    prev_loan = Common.CheckIntegerNull(row["prev_loan"]);
                    no_of_loan = Common.CheckIntegerNull(row["no_of_loan"]);
                    balance = Common.CheckIntegerNull(row["balance"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                       

                        var pLoanID  = Common.CheckIntegerNull(row["loan_id"]);
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.loan_id = Common.CheckIntegerNull(row["loan_id"]);
                        RetObj.loan_type = Common.CheckStringNull(row["loan_type"]);
                        RetObj.loan_start_date = Common.CheckDateTimeNull(row["loan_start_date"]);
                        if (!string.IsNullOrEmpty(row["loan_start_date"].ToString()) && row["loan_start_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["loan_start_date"]);
                            strLoanStartDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strLoanStartDate = "00-000-0000";
                        }
                        RetObj.str_loan_start_date = strLoanStartDate;
                        RetObj.loan_amount = Common.CheckIntegerNull(row["loan_amount"]);
                        RetObj.no_of_loan_install = Common.CheckIntegerNull(row["no_of_loan_install"]);
                        RetObj.loan_install_amount = Common.CheckIntegerNull(row["loan_install_amount"]);
                        RetObj.loan_status = Common.CheckStringNull(row["loan_status"]);
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
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.remaining_bal = Common.CheckStringNull(row["remaining_bal"]);
                     
                        // START FOR DETAIL TABLE LIST

                        List<LoanDetailModel> DetLst = new List<LoanDetailModel>();

                        var detParm = new SqlParameter[5];
                        //setting parameters value get from user
                      
                        detParm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                        detParm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                        detParm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                        detParm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                        detParm[4] = new SqlParameter("@loan_id", SqlDbType.Int) { Value = pLoanID };

                        var detsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Loan_det_proc]", detParm);
                        var detStrLoanPayDate = ""; // for string date
                        var detStrDuePayDate = ""; // for string date
                        var detStrCreatedDate = ""; // for string date
                        var detStrUpdatedDate = ""; // for string date

                        foreach (DataRow rows in detsuser.Rows)
                        {
                            var detObj = new LoanDetailModel();
                            detObj.sr_id = Common.CheckIntegerNull(rows["sr_id"]);
                            detObj.loan_id = Common.CheckIntegerNull(rows["loan_id"]);
                            detObj.loan_det_id = Common.CheckIntegerNull(rows["loan_det_id"]);
                            detObj.due_date = Common.CheckDateTimeNull(rows["due_date"]);
                            if (!string.IsNullOrEmpty(rows["due_date"].ToString()) && rows["due_date"].ToString() != "")
                            {
                                DateTime ChequeOldDate = Convert.ToDateTime(rows["due_date"]);
                                detStrDuePayDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                detStrDuePayDate = "00-000-0000";
                            }
                            detObj.str_due_date = detStrDuePayDate;
                            detObj.loan_pay_date = Common.CheckDateTimeNull(rows["loan_pay_date"]);
                            if (!string.IsNullOrEmpty(rows["loan_pay_date"].ToString()) && rows["loan_pay_date"].ToString() != "")
                            {
                                DateTime ChequeOldDate = Convert.ToDateTime(rows["loan_pay_date"]);
                                detStrLoanPayDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                detStrLoanPayDate = "00-000-0000";
                            }
                            detObj.str_loan_pay_date = detStrLoanPayDate;
                            detObj.loan_amount = Common.CheckIntegerNull(rows["loan_amount"]);
                            detObj.loan_payment = Common.CheckIntegerNull(rows["loan_payment"]);
                            detObj.loan_balance = Common.CheckIntegerNull(rows["loan_balance"]);
                            detObj.installment_no = Common.CheckIntegerNull(rows["installment_no"]);
                            detObj.instal_status = Common.CheckStringNull(rows["instal_status"]);
                            detObj.client_code = Common.CheckStringNull(rows["client_code"]);
                            detObj.company = Common.CheckStringNull(rows["company"]);
                            detObj.emp_id = Common.CheckStringNull(rows["emp_id"]);
                            detObj.created_by = Common.CheckStringNull(rows["created_by"]);
                            detObj.created_date = Common.CheckDateTimeNull(rows["created_date"]);
                            if (!string.IsNullOrEmpty(rows["created_date"].ToString()) && rows["created_date"].ToString() != "")
                            {
                                DateTime ChequeOldDate = Convert.ToDateTime(rows["created_date"]);
                                detStrCreatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                detStrCreatedDate = "00-000-0000";
                            }
                            detObj.str_created_date = detStrCreatedDate;
                           
                            detObj.updated_by = Common.CheckStringNull(rows["updated_by"]);
                            detObj.updated_date = Common.CheckDateTimeNull(rows["updated_date"]);
                            if (!string.IsNullOrEmpty(rows["updated_date"].ToString()) && rows["updated_date"].ToString() != "")
                            {
                                DateTime ChequeOldDate = Convert.ToDateTime(rows["updated_date"]);
                                detStrUpdatedDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                detStrUpdatedDate = "00-000-0000";
                            }
                            detObj.str_updated_date = detStrUpdatedDate;
                            DetLst.Add(detObj);
                        }

                        RetObj.LoanDtl = DetLst;

                        // END FOR DETAIL TABLE LIST




                    }
                    //add all get object in list 
                    RetLst.Add(RetObj);

                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                RObj.avail_limit = avail_limit;
                RObj.prev_loan = prev_loan;
                RObj.no_of_loan = no_of_loan;
                RObj.balance = balance;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.LoanDataList = RetLst;
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


        public LoanDetReturn GetLoanDetById(LoanDetailModel getModel) // get modal from user in getModel
        {
            LoanDetReturn RObj = new LoanDetReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[2];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@loan_id", SqlDbType.VarChar) { Value = getModel.loan_id };
               
                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Loan_det_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strOrderDate = ""; // for string date
                var strDeliveryDate = "";
                var strCreatedDate = ""; // for string date
                var strUpdatedDate = ""; // for string date

                // for single object declaration
                var RetObj = new LoanDetailModel();
                // for list object declaration
                List<LoanDetailModel> RetLst = new List<LoanDetailModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new LoanDetailModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping

                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.loan_id = Common.CheckIntegerNull(row["loan_id"]);
                        RetObj.loan_det_id = Common.CheckIntegerNull(row["loan_det_id"]);
                        if (!string.IsNullOrEmpty(row["due_date"].ToString()) && row["due_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["due_date"]);
                            strOrderDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strOrderDate = "00-000-0000";
                        }
                        RetObj.str_due_date = strOrderDate;
                        RetObj.due_date = Common.CheckDateTimeNull(row["due_date"]);
                        if (!string.IsNullOrEmpty(row["loan_pay_date"].ToString()) && row["loan_pay_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["loan_pay_date"]);
                            strDeliveryDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strDeliveryDate = "00-000-0000";
                        }
                        RetObj.str_loan_pay_date = strDeliveryDate;
                        RetObj.loan_pay_date = Common.CheckDateTimeNull(row["loan_pay_date"]);

                        RetObj.loan_amount = Common.CheckIntegerNull(row["loan_amount"]);
                        RetObj.loan_payment = Common.CheckIntegerNull(row["loan_payment"]);
                        RetObj.loan_balance = Common.CheckIntegerNull(row["loan_balance"]);
                        RetObj.installment_no = Common.CheckIntegerNull(row["installment_no"]);
                        RetObj.instal_status = Common.CheckStringNull(row["instal_status"]);
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
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
                RObj.loanDetDataList = RetLst;
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

        public LoanDetReturn LoanDetStatusUpdater(LoanDetailModel getModel)
        {
            LoanDetReturn RObj = new LoanDetReturn(); // For result body object declaration
            int retstatus = 50;
            string retmessage = "No Data Found!";

            try
            {

                // Parameters array for the stored procedure
                var parm = new SqlParameter[3];
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("loan_det_id", SqlDbType.Int) { Value = getModel.loan_det_id };
                parm[2] = new SqlParameter("instal_status", SqlDbType.VarChar) { Value = getModel.instal_status };

                // Execute the stored procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Loan_det_proc]", parm);
                foreach (DataRow row in dsuser.Rows)
                {
                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);


                }


                RObj.Status = retstatus;
                RObj.Message = retmessage;
                return RObj;
            }
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found: " + ex.Message;
                return RObj;
            }
        }


        ///////////////// UPDATE LEAVES IN LIST //////////////////////////
        public LoanReturn AUDLoans(List<LoanModel> getModels) // Change parameter to accept a list of EmployeeModels
        {
            LoanReturn RObj = new LoanReturn(); // For result body object declaration

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
                    parm[2] = new SqlParameter("loan_id", SqlDbType.Int, 255) { Value = getModel.loan_id  };
                    parm[3] = new SqlParameter("loan_type", SqlDbType.VarChar, 50) { Value = getModel.loan_type };
                    parm[4] = new SqlParameter("loan_start_date", SqlDbType.DateTime) { Value = getModel.loan_start_date };
                    parm[5] = new SqlParameter("loan_amount", SqlDbType.Int, 100) { Value = getModel.loan_amount  };
                    parm[6] = new SqlParameter("no_of_loan_install", SqlDbType.Int, 100) { Value = getModel.no_of_loan_install  };
                    parm[7] = new SqlParameter("loan_install_amount", SqlDbType.Int) { Value = getModel.loan_install_amount   };
                    parm[8] = new SqlParameter("loan_status", SqlDbType.VarChar) { Value = getModel.loan_status ?? (object)DBNull.Value };
                    parm[9] = new SqlParameter("created_by", SqlDbType.VarChar, 50) { Value = getModel.created_by ?? (object)DBNull.Value };
                    parm[10] = new SqlParameter("updated_by", SqlDbType.VarChar, 50) { Value = getModel.updated_by ?? (object)DBNull.Value };
                    parm[11] = new SqlParameter("client_code", SqlDbType.VarChar, 50) { Value = getModel.client_code ?? (object)DBNull.Value };
                    parm[12] = new SqlParameter("company", SqlDbType.VarChar, 100) { Value = getModel.company ?? (object)DBNull.Value };
                    parm[13] = new SqlParameter("emp_id", SqlDbType.VarChar, 50) { Value = getModel.emp_id ?? (object)DBNull.Value };
                    parm[14] = new SqlParameter("updated_date", SqlDbType.DateTime, 50) { Value = getModel.updated_date ?? (object)DBNull.Value };


                    // Call the stored procedure for each employee
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_loan_proc]", parm);
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


        public LoanDetReturn UpdateLoanStatus(LoanDetailModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            LoanDetReturn RObj = new LoanDetReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[4];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("loan_det_id", SqlDbType.Int) { Value = getModel.loan_det_id };
                parm[2] = new SqlParameter("instal_status", SqlDbType.VarChar) { Value = getModel.instal_status };
                parm[3] = new SqlParameter("loan_id", SqlDbType.Int) { Value = getModel.loan_id };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Loan_det_proc]", parm);
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

        ///////////////// GET ALL LEAVES DAL //////////////////////////

        public LoanTypeReturn GetAllLoanTypes(LoanTypeModel getModel) // get modal from user in getModel
        {
            LoanTypeReturn RObj = new LoanTypeReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 6 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Loan_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
       
                var strCreatedDate = "";
                var strUpdateDate = "";


                // for single object declaration
                var RetObj = new LoanTypeModel();
                // for list object declaration
                List<LoanTypeModel> RetLst = new List<LoanTypeModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new LoanTypeModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping

                      
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
                        //RetObj.Operation = Common.CheckIntegerNull(row["Operation"]);
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.loan_type_id = Common.CheckIntegerNull(row["loan_type_id"]);
                        RetObj.loan_type_name = Common.CheckStringNull(row["loan_type_name"]);
                        RetObj.no_of_install_allow = Common.CheckIntegerNull(row["no_of_install_allow"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
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
                RObj.LoanTypeDataList = RetLst;
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


        #endregion
    }
}