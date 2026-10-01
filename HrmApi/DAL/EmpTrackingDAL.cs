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
    public class EmpTrackingDAL
    {
        private string _ConnString; //variable for connection string
        public EmpTrackingDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Tracking
      

       

        public EmpTrackingReturn AUDSingleTracking(EmployeeTrackingModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            EmpTrackingReturn RObj = new EmpTrackingReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[5];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("lat", SqlDbType.VarChar) { Value = getModel.lat };
                parm[2] = new SqlParameter("lon", SqlDbType.VarChar) { Value = getModel.lon };
                parm[3] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[4] = new SqlParameter("track_time", SqlDbType.DateTime) { Value = getModel.track_time };
              

                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[HRM_Emp_Tracking_Proc]", parm);
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
