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
    public class ComplainDAL
    {
        private string _ConnString; //variable for connection string
        public ComplainDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Complain
        ///////////////// GET ALL LEAVES DAL //////////////////////////

        public ComplainReturn GetAllComplains(ComplainModel getModel) // get modal from user in getModel
        {
            ComplainReturn RObj = new ComplainReturn(); // for result body object declaration
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
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_complaint_proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strfrom_Date = "";
                var strCreatedDate = "";

                // for single object declaration
                var RetObj = new ComplainModel();
                // for list object declaration
                List<ComplainModel> RetLst = new List<ComplainModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new ComplainModel(); //this important for all rows

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
                            strfrom_Date = "00-000-0000";
                        }
                        RetObj.str_created_date = strCreatedDate;
                        //RetObj.Operation = Common.CheckIntegerNull(row["Operation"]);
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.cmplnt_id = Common.CheckIntegerNull(row["cmplnt_id"]);
                        RetObj.complainee = Common.CheckStringNull(row["complainee"]);
                        RetObj.complnt_no = Common.CheckStringNull(row["complnt_no"]);
                        RetObj.cmplnt_name = Common.CheckStringNull(row["cmplnt_name"]);
                        RetObj.complainee_dep = Common.CheckStringNull(row["complainee_dep"]);
                        RetObj.complainee_designation = Common.CheckStringNull(row["complainee_designation"]);
                        RetObj.overview = "overview";
                        RetObj.resolution_step = "resolution_step";
                        RetObj.actions = "actions";
                        RetObj.cmplnt_sub = Common.CheckStringNull(row["cmplnt_sub"]);
                        RetObj.description = Common.CheckStringNull(row["description"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.created_date = Common.CheckDateTimeNull(row["created_date"]);
                        RetObj.updated_by = Common.CheckStringNull(row["updated_by"]);
                        RetObj.updated_date = Common.CheckDateTimeNull(row["updated_date"]);
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.act_first_head = Common.CheckStringNull(row["act_first_head"]);
                        RetObj.act_first_desc = Common.CheckStringNull(row["act_first_desc"]);
                        RetObj.act_second_head = Common.CheckStringNull(row["act_second_head"]);
                        RetObj.act_second_desc = Common.CheckStringNull(row["act_second_desc"]);
                        RetObj.act_third_head = Common.CheckStringNull(row["act_third_head"]);
                        RetObj.act_third_desc = Common.CheckStringNull(row["act_third_desc"]);
                        RetObj.complain_from_to_show = Common.CheckStringNull(row["complain_from_to_show"]);
                        RetObj.complain_from_dep_to_show = Common.CheckStringNull(row["complain_from_dep_to_show"]);
                        RetObj.complain_to_to_show = Common.CheckStringNull(row["complain_to_to_show"]);
                        RetObj.complain_to_dep_to_show = Common.CheckStringNull(row["complain_to_dep_to_show"]);



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
                RObj.LeaveDataList = RetLst;
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
        public ComplainReturn AUDComplains(List<ComplainModel> getModels) // Change parameter to accept a list of EmployeeModels
        {
            ComplainReturn RObj = new ComplainReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";
                foreach (var getModel in getModels) // Iterate over each employee model in the list
                {
                    // Array for parameters
                    var parm = new SqlParameter[23];
                    // Setting parameters value from the user input (getModel)
                    parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                    parm[1] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                    parm[2] = new SqlParameter("cmplnt_id", SqlDbType.Int) { Value = getModel.cmplnt_id };
                    parm[3] = new SqlParameter("complainee", SqlDbType.VarChar, 255) { Value = getModel.complainee ?? (object)DBNull.Value };
                    parm[4] = new SqlParameter("complainee_dep", SqlDbType.VarChar, 255) { Value = getModel.complainee_dep ?? (object)DBNull.Value };
                    parm[5] = new SqlParameter("complainee_designation", SqlDbType.VarChar, 255) { Value = getModel.complainee_designation ?? (object)DBNull.Value };
                    parm[6] = new SqlParameter("cmplnt_sub", SqlDbType.VarChar, 255) { Value = getModel.cmplnt_sub ?? (object)DBNull.Value };
                    parm[7] = new SqlParameter("description", SqlDbType.VarChar, 1000) { Value = getModel.description ?? (object)DBNull.Value };
                    parm[8] = new SqlParameter("created_by", SqlDbType.VarChar, 255) { Value = getModel.created_by ?? (object)DBNull.Value };
                    parm[9] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date ?? (object)DBNull.Value };
                    parm[10] = new SqlParameter("updated_by", SqlDbType.VarChar, 255) { Value = getModel.updated_by ?? (object)DBNull.Value };
                    parm[11] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date ?? (object)DBNull.Value };
                    parm[12] = new SqlParameter("client_code", SqlDbType.VarChar, 50) { Value = getModel.client_code ?? (object)DBNull.Value };
                    parm[13] = new SqlParameter("company", SqlDbType.VarChar, 255) { Value = getModel.company ?? (object)DBNull.Value };
                    parm[14] = new SqlParameter("emp_id", SqlDbType.VarChar, 50) { Value = getModel.emp_id ?? (object)DBNull.Value };
                    parm[15] = new SqlParameter("department", SqlDbType.VarChar, 255) { Value = getModel.department ?? (object)DBNull.Value };
                    parm[16] = new SqlParameter("cmplnt_name", SqlDbType.VarChar, 255) { Value = getModel.cmplnt_name ?? (object)DBNull.Value };
                    parm[17] = new SqlParameter("act_first_head", SqlDbType.VarChar) { Value = getModel.act_first_head };
                    parm[18] = new SqlParameter("act_first_desc", SqlDbType.VarChar) { Value = getModel.act_first_desc };
                    parm[19] = new SqlParameter("act_second_head", SqlDbType.VarChar) { Value = getModel.act_second_head };
                    parm[20] = new SqlParameter("act_second_desc", SqlDbType.VarChar) { Value = getModel.act_second_desc };
                    parm[21] = new SqlParameter("act_third_head", SqlDbType.VarChar) { Value = getModel.act_third_head };
                    parm[22] = new SqlParameter("act_third_desc", SqlDbType.VarChar) { Value = getModel.act_third_desc };


                    // Call the stored procedure for each employee
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_complaint_proc]", parm);
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


        public ComplainReturn AUDSingleComplain(ComplainModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            ComplainReturn RObj = new ComplainReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[23];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("sr_id", SqlDbType.Int) { Value = getModel.sr_id };
                parm[2] = new SqlParameter("cmplnt_id", SqlDbType.Int) { Value = getModel.cmplnt_id };
                parm[3] = new SqlParameter("complainee", SqlDbType.VarChar, 255) { Value = getModel.complainee ?? (object)DBNull.Value };
                parm[4] = new SqlParameter("complainee_dep", SqlDbType.VarChar, 255) { Value = getModel.complainee_dep ?? (object)DBNull.Value };
                parm[5] = new SqlParameter("complainee_designation", SqlDbType.VarChar, 255) { Value = getModel.complainee_designation ?? (object)DBNull.Value };
                parm[6] = new SqlParameter("cmplnt_sub", SqlDbType.VarChar, 255) { Value = getModel.cmplnt_sub ?? (object)DBNull.Value };
                parm[7] = new SqlParameter("description", SqlDbType.VarChar, 1000) { Value = getModel.description ?? (object)DBNull.Value };
                parm[8] = new SqlParameter("created_by", SqlDbType.VarChar, 255) { Value = getModel.created_by ?? (object)DBNull.Value };
                parm[9] = new SqlParameter("created_date", SqlDbType.DateTime) { Value = getModel.created_date ?? (object)DBNull.Value };
                parm[10] = new SqlParameter("updated_by", SqlDbType.VarChar, 255) { Value = getModel.updated_by ?? (object)DBNull.Value };
                parm[11] = new SqlParameter("updated_date", SqlDbType.DateTime) { Value = getModel.updated_date ?? (object)DBNull.Value };
                parm[12] = new SqlParameter("client_code", SqlDbType.VarChar, 50) { Value = getModel.client_code ?? (object)DBNull.Value };
                parm[13] = new SqlParameter("company", SqlDbType.VarChar, 255) { Value = getModel.company ?? (object)DBNull.Value };
                parm[14] = new SqlParameter("emp_id", SqlDbType.VarChar, 50) { Value = getModel.emp_id ?? (object)DBNull.Value };
                parm[15] = new SqlParameter("department", SqlDbType.VarChar, 255) { Value = getModel.department ?? (object)DBNull.Value };
                parm[16] = new SqlParameter("cmplnt_name", SqlDbType.VarChar, 255) { Value = getModel.cmplnt_name ?? (object)DBNull.Value };
                parm[17] = new SqlParameter("act_first_head", SqlDbType.VarChar) { Value = getModel.act_first_head };
                parm[18] = new SqlParameter("act_first_desc", SqlDbType.VarChar) { Value = getModel.act_first_desc };
                parm[19] = new SqlParameter("act_second_head", SqlDbType.VarChar) { Value = getModel.act_second_head };
                parm[20] = new SqlParameter("act_second_desc", SqlDbType.VarChar) { Value = getModel.act_second_desc };
                parm[21] = new SqlParameter("act_third_head", SqlDbType.VarChar) { Value = getModel.act_third_head };
                parm[22] = new SqlParameter("act_third_desc", SqlDbType.VarChar) { Value = getModel.act_third_desc };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_complaint_proc]", parm);
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
