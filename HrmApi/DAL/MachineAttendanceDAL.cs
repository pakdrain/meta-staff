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
    public class MachineAttendanceDAL
    {
        private string _ConnString; //variable for connection string
        public MachineAttendanceDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region  MachineAttendance

        public MachineAttendanceReturn GetAllMachineAtt(MachineAttendance getModel) // get modal from user in getModel
        {
            MachineAttendanceReturn RObj = new MachineAttendanceReturn(); // for result body object declaration
            int retstatus; string retmessage;

            try
            {
                //array for parameters
                var parm = new SqlParameter[1];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 1 };
              

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Machine_Att_Proc]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";

                var strCreatedDate = "";
                var strUpdateDate = "";


                // for single object declaration
                var RetObj = new MachineAttendance();
                // for list object declaration
                List<MachineAttendance> RetLst = new List<MachineAttendance>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new MachineAttendance(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        //coloumn mapping
                        RetObj.Id = Common.CheckIntegerNull(row["Id"]);
                        RetObj.EmpCode = Common.CheckStringNull(row["EmpCode"]);
                        RetObj.AttendanceTime = Common.CheckDateTimeNull(row["AttendanceTime"]);
                        RetObj.DeviceId = Common.CheckStringNull(row["DeviceId"]);
                        RetObj.Status = Common.CheckStringNull(row["Status"]);
                        RetObj.CreatedAt = Common.CheckDateTimeNull(row["CreatedAt"]);
                        RetObj.Remarks = Common.CheckStringNull(row["Remarks"]);

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
                RObj.MachineAttDataList = RetLst;
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


        public MachineAttendanceReturn AUDMachineAtt(MachineAttendance getModel) // Change parameter to accept a list of EmployeeModels
        {
            MachineAttendanceReturn RObj = new MachineAttendanceReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[8];
                // Setting parameters value from the user input (getModel)
                parm[0] = new SqlParameter("Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("Id", SqlDbType.Int) { Value = getModel.Id };
                parm[2] = new SqlParameter("EmpCode", SqlDbType.VarChar) { Value = getModel.EmpCode  };
                parm[3] = new SqlParameter("AttendanceTime", SqlDbType.DateTime) { Value = getModel.AttendanceTime };
                parm[4] = new SqlParameter("DeviceId", SqlDbType.VarChar) { Value = getModel.DeviceId  };
                parm[5] = new SqlParameter("Status", SqlDbType.VarChar) { Value = getModel.Status  };
                parm[6] = new SqlParameter("CreatedAt", SqlDbType.DateTime) { Value = getModel.CreatedAt };
                parm[7] = new SqlParameter("Remarks", SqlDbType.VarChar) { Value = getModel.Remarks  };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Machine_Att_Proc]", parm);
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