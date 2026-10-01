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
        ///////////////// GET WHOLE NOTIFICATIONS //////////////////////////
        public async Task<NotifReturn> GetWholeNotif(NotifModel getModel)
        {
            NotifReturn RObj = new NotifReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new NotifModel
            {
                Operation = 1,
                emp_id = getModel.emp_id,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                notif_id = getModel.notif_id,
                notif_type = getModel.notif_type,
                notif_module = getModel.notif_module,
                notif_emp = getModel.notif_emp,
                notif_tran_status = getModel.notif_tran_status,
                notif_tran_no = getModel.notif_tran_no,
                notif_tran_date = getModel.notif_tran_date,
                str_notif_tran_date = getModel.str_notif_tran_date,
                notif_read = getModel.notif_read,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                created_by = getModel.created_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                updated_by = getModel.updated_by,
                screen = getModel.screen,
                notif_desc = getModel.notif_desc,
                leave_my_scr = getModel.leave_my_scr,
                leave_hod_scr = getModel.leave_hod_scr,
                leave_hr_scr = getModel.leave_hr_scr,
                od_hr_scr = getModel.od_hr_scr,
                od_hod_scr = getModel.od_hod_scr,
                od_my_scr = getModel.od_my_scr,
                loan_my_scr = getModel.loan_my_scr,
                loan_hod_scr = getModel.loan_hod_scr,
                loan_hr_scr = getModel.loan_hr_scr,
                loan_gm_scr = getModel.loan_gm_scr,
                loan_ed_scr = getModel.loan_ed_scr,
                clear_hr_scr = getModel.clear_hr_scr,
                clear_hod_scr = getModel.clear_hod_scr,
                clear_departments_scr = getModel.clear_departments_scr,
                task_add_scr = getModel.task_add_scr,
                task_assigner_scr = getModel.task_assigner_scr,
                task_assigned_scr = getModel.task_assigned_scr,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/notifications";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<NotifApiResponse>(jsonResponse);

                        List<NotifModel> RetLst = new List<NotifModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            NotifModel RetObj = new NotifModel
                            {

                                Operation = item.Operation,
                                emp_id = item.emp_id,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                notif_id = item.notif_id,
                                notif_type = item.notif_type,
                                notif_module = item.notif_module,
                                notif_emp = item.notif_emp,
                                notif_tran_status = item.notif_tran_status,
                                notif_tran_no = item.notif_tran_no,
                                notif_tran_date = item.notif_tran_date,
                                str_notif_tran_date = item.str_notif_tran_date,
                                notif_read = item.notif_read,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                created_by = item.created_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                updated_by = item.updated_by,
                                screen = item.screen,
                                notif_desc = item.notif_desc,
                                leave_my_scr = item.leave_my_scr,
                                leave_hod_scr = item.leave_hod_scr,
                                leave_hr_scr = item.leave_hr_scr,
                                od_hr_scr = item.od_hr_scr,
                                od_hod_scr = item.od_hod_scr,
                                od_my_scr = item.od_my_scr,
                                loan_my_scr = item.loan_my_scr,
                                loan_hod_scr = item.loan_hod_scr,
                                loan_hr_scr = item.loan_hr_scr,
                                loan_gm_scr = item.loan_gm_scr,
                                loan_ed_scr = item.loan_ed_scr,
                                clear_hr_scr = item.clear_hr_scr,
                                clear_hod_scr = item.clear_hod_scr,
                                clear_departments_scr = item.clear_departments_scr,
                                task_add_scr = item.task_add_scr,
                                task_assigner_scr = item.task_assigner_scr,
                                task_assigned_scr = item.task_assigned_scr,
                                cmplt_my_screen = item.cmplt_my_screen,
                                cmplt_hr_screen = item.cmplt_hr_screen,
                                veh_my_scr = item.veh_my_scr,
                                veh_hod_scr = item.veh_hod_scr,
                                veh_hr_scr = item.veh_hr_scr,
                                veh_gm_scr = item.veh_gm_scr,
                                veh_admin_scr = item.veh_admin_scr,
                                veh_ed_scr = item.veh_ed_scr,
                                hc_invidual_scr = item.hc_invidual_scr,
                                hc_dep_scr = item.hc_dep_scr,
                                hc_board_scr = item.hc_board_scr,
                                sal_my_scr = item.sal_my_scr,
                                sal_hod_scr = item.sal_hod_scr,
                                sal_hr_scr = item.sal_hr_scr,
                                wp_create_scr = item.wp_create_scr,
                                wp_inbox_scr = item.wp_inbox_scr,
                                order_scr = item.order_scr,
                                task_hod = item.task_hod,
                                task_ed = item.task_ed,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.NotifDataList = RetLst;
                        }
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
        public async Task<NotifReturn> GetMarkReadWholeNotif(NotifModel getModel)
        {
            NotifReturn RObj = new NotifReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new NotifModel
            {
                Operation = 2,
                emp_id = getModel.emp_id,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                notif_id = getModel.notif_id,
                notif_type = getModel.notif_type,
                notif_module = getModel.notif_module,
                notif_emp = getModel.notif_emp,
                notif_tran_status = getModel.notif_tran_status,
                notif_tran_no = getModel.notif_tran_no,
                notif_tran_date = getModel.notif_tran_date,
                str_notif_tran_date = getModel.str_notif_tran_date,
                notif_read = getModel.notif_read,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                created_by = getModel.created_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                updated_by = getModel.updated_by,
                screen = getModel.screen,
                notif_desc = getModel.notif_desc,
                leave_my_scr = getModel.leave_my_scr,
                leave_hod_scr = getModel.leave_hod_scr,
                leave_hr_scr = getModel.leave_hr_scr,
                od_hr_scr = getModel.od_hr_scr,
                od_hod_scr = getModel.od_hod_scr,
                od_my_scr = getModel.od_my_scr,
                loan_my_scr = getModel.loan_my_scr,
                loan_hod_scr = getModel.loan_hod_scr,
                loan_hr_scr = getModel.loan_hr_scr,
                loan_gm_scr = getModel.loan_gm_scr,
                loan_ed_scr = getModel.loan_ed_scr,
                clear_hr_scr = getModel.clear_hr_scr,
                clear_hod_scr = getModel.clear_hod_scr,
                clear_departments_scr = getModel.clear_departments_scr,
                task_add_scr = getModel.task_add_scr,
                task_assigner_scr = getModel.task_assigner_scr,
                task_assigned_scr = getModel.task_assigned_scr,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/notifications";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<NotifApiResponse>(jsonResponse);

                        List<NotifModel> RetLst = new List<NotifModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;                                                                       

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                       
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
        public async Task<NotifReturn> GetMyNotif(NotifModel getModel)
        {
            NotifReturn RObj = new NotifReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new NotifModel
            {
                Operation = 3,
                emp_id = getModel.emp_id,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                notif_id = getModel.notif_id,
                notif_type = getModel.notif_type,
                notif_module = getModel.notif_module,
                notif_emp = getModel.notif_emp,
                notif_tran_status = getModel.notif_tran_status,
                notif_tran_no = getModel.notif_tran_no,
                notif_tran_date = getModel.notif_tran_date,
                str_notif_tran_date = getModel.str_notif_tran_date,
                notif_read = getModel.notif_read,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                created_by = getModel.created_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                updated_by = getModel.updated_by,
                screen = getModel.screen,
                notif_desc = getModel.notif_desc,
                leave_my_scr = getModel.leave_my_scr,
                leave_hod_scr = getModel.leave_hod_scr,
                leave_hr_scr = getModel.leave_hr_scr,
                od_hr_scr = getModel.od_hr_scr,
                od_hod_scr = getModel.od_hod_scr,
                od_my_scr = getModel.od_my_scr,
                loan_my_scr = getModel.loan_my_scr,
                loan_hod_scr = getModel.loan_hod_scr,
                loan_hr_scr = getModel.loan_hr_scr,
                loan_gm_scr = getModel.loan_gm_scr,
                loan_ed_scr = getModel.loan_ed_scr,
                clear_hr_scr = getModel.clear_hr_scr,
                clear_hod_scr = getModel.clear_hod_scr,
                clear_departments_scr = getModel.clear_departments_scr,
                task_add_scr = getModel.task_add_scr,
                task_assigner_scr = getModel.task_assigner_scr,
                task_assigned_scr = getModel.task_assigned_scr,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/notifications";


                using (HttpClient client = new HttpClient())
                {
                    // Serialize the request model to JSON
                    string jsonBody = JsonConvert.SerializeObject(attendanceRequest);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send POST request with the JSON body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        retstatus = 100;
                        retmessage = "Data fetched successfully";

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<NotifApiResponse>(jsonResponse);

                        List<NotifModel> RetLst = new List<NotifModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            NotifModel RetObj = new NotifModel
                            {

                                Operation = item.Operation,
                                emp_id = item.emp_id,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                notif_id = item.notif_id,
                                notif_type = item.notif_type,
                                notif_module = item.notif_module,
                                notif_emp = item.notif_emp,
                                notif_tran_status = item.notif_tran_status,
                                notif_tran_no = item.notif_tran_no,
                                notif_tran_date = item.notif_tran_date,
                                str_notif_tran_date = item.str_notif_tran_date,
                                notif_read = item.notif_read,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                created_by = item.created_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                updated_by = item.updated_by,
                                screen = item.screen,
                                notif_desc = item.notif_desc,
                                leave_my_scr = item.leave_my_scr,
                                leave_hod_scr = item.leave_hod_scr,
                                leave_hr_scr = item.leave_hr_scr,
                                od_hr_scr = item.od_hr_scr,
                                od_hod_scr = item.od_hod_scr,
                                od_my_scr = item.od_my_scr,
                                loan_my_scr = item.loan_my_scr,
                                loan_hod_scr = item.loan_hod_scr,
                                loan_hr_scr = item.loan_hr_scr,
                                loan_gm_scr = item.loan_gm_scr,
                                loan_ed_scr = item.loan_ed_scr,
                                clear_hr_scr = item.clear_hr_scr,
                                clear_hod_scr = item.clear_hod_scr,
                                clear_departments_scr = item.clear_departments_scr,
                                task_add_scr = item.task_add_scr,
                                task_assigner_scr = item.task_assigner_scr,
                                task_assigned_scr = item.task_assigned_scr,

                                task_hod_scr = item.task_hod_scr,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.NotifDataList = RetLst;
                        }
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