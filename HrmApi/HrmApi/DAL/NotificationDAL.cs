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
    public class NotificationDAL
    {
        private string _ConnString; //variable for connection string
        public NotificationDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Notifications
        ///////////////// GET ALL LEAVES DAL //////////////////////////

        public NotificationReturn GetAllNotifications(NotificationModel getModel) // get modal from user in getModel
        {
            NotificationReturn RObj = new NotificationReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[6];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[4] = new SqlParameter("@for_comp", SqlDbType.VarChar) { Value = getModel.for_comp };
                parm[5] = new SqlParameter("@for_emp", SqlDbType.VarChar) { Value = getModel.for_emp };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Notification_Proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                var strCreatedDate = "";
                var strUpdatedDate = "";

                // for single object declaration
                var RetObj = new NotificationModel();
                // for list object declaration
                List<NotificationModel> RetLst = new List<NotificationModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new NotificationModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.sr_id = Common.CheckIntegerNull(row["sr_id"]);
                        RetObj.notif_id = Common.CheckIntegerNull(row["notif_id"]);
                        RetObj.notif_title = Common.CheckStringNull(row["notif_title"]);
                        RetObj.notif_desc = Common.CheckStringNull(row["notif_desc"]);
                        RetObj.notif_type = Common.CheckStringNull(row["notif_type"]);
                        RetObj.notif_status = Common.CheckStringNull(row["notif_status"]);
                        RetObj.for_comp = Common.CheckStringNull(row["for_comp"]);
                        RetObj.for_emp = Common.CheckStringNull(row["for_emp"]);



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
                RObj.NotificationDataList = RetLst;
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
        public NotificationReturn AUDNotifications(NotificationModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            NotificationReturn RObj = new NotificationReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[16];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.VarChar) { Value = getModel.Operation };
                parm[1] = new SqlParameter("sr_id", SqlDbType.VarChar) { Value = getModel.sr_id };
                parm[2] = new SqlParameter("notif_id", SqlDbType.VarChar) { Value = getModel.notif_id };
                parm[3] = new SqlParameter("notif_title", SqlDbType.VarChar) { Value = getModel.notif_title };
                parm[4] = new SqlParameter("notif_desc", SqlDbType.VarChar) { Value = getModel.notif_desc };
                parm[5] = new SqlParameter("notif_type", SqlDbType.VarChar) { Value = getModel.notif_type };
                parm[6] = new SqlParameter("notif_status", SqlDbType.VarChar) { Value = getModel.notif_status };
                parm[7] = new SqlParameter("for_comp", SqlDbType.VarChar) { Value = getModel.for_comp };
                parm[8] = new SqlParameter("for_emp", SqlDbType.VarChar) { Value = getModel.for_emp };
                parm[9] = new SqlParameter("created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                parm[10] = new SqlParameter("created_date", SqlDbType.VarChar) { Value = getModel.created_date };
                parm[11] = new SqlParameter("updated_by", SqlDbType.VarChar) { Value = getModel.updated_by };
                parm[12] = new SqlParameter("updated_date", SqlDbType.VarChar) { Value = getModel.updated_date };
                parm[13] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[14] = new SqlParameter("company", SqlDbType.VarChar) { Value = getModel.company };
                parm[15] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Notification_Proc]", parm);
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