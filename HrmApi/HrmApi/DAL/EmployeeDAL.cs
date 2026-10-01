using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using HrmApi.Models;
using static HrmApi.DAL.SQLHelper;
using HrmApi.Views;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using System.IO;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace HrmApi.DAL
{
    public class EmployeeDAL
    {
        private string _ConnString; //variable for connection string
        public EmployeeDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }


        #region Employee 

        ///////////////// GET ALL EMPLOYEES DAL //////////////////////////

        public EmployeeReturn GetAllEmployee(EmployeeModel getModel) // get modal from user in getModel
        {
            EmployeeReturn RObj = new EmployeeReturn(); // for result body object declaration
            int retstatus; string retmessage; string strDOJ; string strDeactiveDate;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@department", SqlDbType.VarChar) { Value = getModel.department };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                strDOJ = "";
                strDeactiveDate = "";

                // for single object declaration
                var RetObj = new EmployeeModel();
                // for list object declaration
                List<EmployeeModel> RetLst = new List<EmployeeModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new EmployeeModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    //coloumn mapping
                    if (retstatus == 2)
                    {
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.company = Common.CheckStringNull(row["company_name"]);
                        RetObj.branch = Common.CheckStringNull(row["branch"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.creation_date = Common.CheckDateTimeNull(row["creation_date"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
                        RetObj.update_by = Common.CheckStringNull(row["update_by"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.emp_img = Common.CheckStringNull(row["emp_img"]);
                        RetObj.shift_start_time = Common.CheckStringNull(row["shift_start_time"]);
                        RetObj.shift_end_time = Common.CheckStringNull(row["shift_end_time"]);
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
                        RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
                        RetObj.ofc_lat = Common.CheckStringNull(row["ofc_lat"]);
                        RetObj.ofc_lon = Common.CheckStringNull(row["ofc_lon"]);
                        RetObj.image_extension = Common.CheckStringNull(row["image_extension"]);
                        RetObj.emp_image_url = Common.CheckStringNull(row["emp_image_url"]);
                        RetObj.task_type_id = Common.CheckIntegerNull(row["task_type_id"]);
                        RetObj.role_name = Common.CheckStringNull(row["role_name"]);
                        RetObj.is_active = Common.CheckIntegerNull(row["is_active"]);
                        RetObj.role_id = Common.CheckIntegerNull(row["role_id"]);
                        RetObj.office_id = Common.CheckIntegerNull(row["office_id"]);
                        RetObj.att_radius = Common.CheckIntegerNull(row["att_radius"]);
                        RetObj.device_id = Common.CheckStringNull(row["device_id"]);
                        RetObj.deactive_because = Common.CheckStringNull(row["deactive_because"]);
                        RetObj.deactive_date = Common.CheckDateTimeNull(row["deactive_date"]);
                        if (!string.IsNullOrEmpty(row["deactive_date"].ToString()) && row["deactive_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["deactive_date"]);
                            strDeactiveDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strDeactiveDate = "00-000-0000";
                        }
                        RetObj.str_deactive_date = strDeactiveDate;


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

                        //add all get object in list 
                        RetLst.Add(RetObj);
                    }
                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.EmployeeDataList = RetLst;
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

        /////////////////////// GET EMPLOYEE BY ID ////////////////////////////
        public EmployeeReturn GetEmployeeById(EmployeeModel getModel) // get modal from user in getModel
        {
            EmployeeReturn RObj = new EmployeeReturn(); // for result body object declaration
            int retstatus; string retmessage; string strDeactiveDate;
            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Operation };

                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };

                parm[2] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[3] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };


                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                strDeactiveDate = "";

                // for single object declaration
                var RetObj = new EmployeeModel();
                // for list object declaration
                //List<EmployeeModel> RetLst = new List<EmployeeModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new EmployeeModel(); //this important for all rows
                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    //coloumn mapping
                    if (retstatus == 2)
                    {
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.company = Common.CheckStringNull(row["company_name"]);
                        RetObj.branch = Common.CheckStringNull(row["branch"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.creation_date = Common.CheckDateTimeNull(row["creation_date"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
                        RetObj.update_by = Common.CheckStringNull(row["update_by"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.emp_img = Common.CheckStringNull(row["emp_img"]);
                        RetObj.update_date = Common.CheckDateTimeNull(row["shift_start_time"]);
                        RetObj.update_date = Common.CheckDateTimeNull(row["shift_end_time"]);
                        RetObj.leave_allow = Common.CheckDecimalNull(row["leave_allow"]);
                        RetObj.curr_salary = Common.CheckIntegerNull(row["curr_salary"]);
                        RetObj.pass = Common.CheckStringNull(row["pass"]);
                        RetObj.per_month_order_limit = Common.CheckIntegerNull(row["per_month_order_limit"]);
                        RetObj.this_month_order_limit = Common.CheckIntegerNull(row["this_month_order_limit"]);
                        RetObj.per_month_qty_limit = Common.CheckIntegerNull(row["per_month_qty_limit"]);
                        RetObj.this_month_qty_limit = Common.CheckIntegerNull(row["this_month_qty_limit"]);
                        RetObj.dicount_percetage = Common.CheckIntegerNull(row["dicount_percetage"]);
                        RetObj.ofc_lat = Common.CheckStringNull(row["ofc_lat"]);
                        RetObj.ofc_lon = Common.CheckStringNull(row["ofc_lon"]);
                        RetObj.image_extension = Common.CheckStringNull(row["image_extension"]);
                        RetObj.emp_image_url = Common.CheckStringNull(row["emp_image_url"]);
                        RetObj.task_type_id = Common.CheckIntegerNull(row["task_type_id"]);
                        RetObj.role_id = Common.CheckIntegerNull(row["role_id"]);
                        RetObj.office_id = Common.CheckIntegerNull(row["office_id"]);
                        RetObj.att_radius = Common.CheckIntegerNull(row["att_radius"]);
                        RetObj.device_id = Common.CheckStringNull(row["device_id"]);
                        RetObj.deactive_because = Common.CheckStringNull(row["deactive_because"]);
                        RetObj.deactive_date = Common.CheckDateTimeNull(row["deactive_date"]);
                        if (!string.IsNullOrEmpty(row["deactive_date"].ToString()) && row["deactive_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["deactive_date"]);
                            strDeactiveDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strDeactiveDate = "00-000-0000";
                        }
                        RetObj.str_deactive_date = strDeactiveDate;
                        RetObj.role_name = Common.CheckStringNull(row["role_name"]);
                        RetObj.is_active = Common.CheckIntegerNull(row["is_active"]);


                    }
                    //add all get object in list 
                    //RetLst.Add(RetObj);
                }
                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                RObj.EmployeeData = RetObj;
                // if want to show in list of object
                //RObj.EmployeeDataList = RetLst;
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

        ///////////////// AUD EMPLOYEES IN LIST //////////////////////////
        public EmployeeReturn AUDEmployees(List<EmployeeModel> getModels) // Change parameter to accept a list of EmployeeModels
        {
            EmployeeReturn RObj = new EmployeeReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";
                foreach (var getModel in getModels) // Iterate over each employee model in the list
                {
                    // Array for parameters
                    var parm = new SqlParameter[20];
                    // Setting parameters value from the user input (getModel)
                    parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 3 };
                    parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                    parm[2] = new SqlParameter("@emp_name", SqlDbType.VarChar) { Value = getModel.emp_name };
                    parm[3] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                    parm[4] = new SqlParameter("@branch", SqlDbType.VarChar) { Value = getModel.branch };
                    parm[5] = new SqlParameter("@department", SqlDbType.VarChar) { Value = getModel.department };
                    parm[6] = new SqlParameter("@created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                    parm[7] = new SqlParameter("@update_by", SqlDbType.VarChar) { Value = getModel.update_by };
                    parm[8] = new SqlParameter("@designation", SqlDbType.VarChar) { Value = getModel.designation };
                    parm[9] = new SqlParameter("@emp_img", SqlDbType.VarChar) { Value = getModel.emp_img };
                    parm[10] = new SqlParameter("@shift_start_time", SqlDbType.DateTime) { Value = getModel.shift_start_time };
                    parm[11] = new SqlParameter("@shift_end_time", SqlDbType.DateTime) { Value = getModel.shift_end_time };
                    parm[12] = new SqlParameter("@leave_allow", SqlDbType.Decimal) { Value = getModel.leave_allow };
                    parm[13] = new SqlParameter("@curr_salary", SqlDbType.Decimal) { Value = getModel.curr_salary };
                    parm[14] = new SqlParameter("@pass", SqlDbType.Decimal) { Value = getModel.pass };
                    parm[15] = new SqlParameter("@client_code", SqlDbType.Int) { Value = getModel.client_code };
                    parm[16] = new SqlParameter("@per_month_order_limit", SqlDbType.Int) { Value = getModel.per_month_order_limit };
                    parm[17] = new SqlParameter("@per_month_qty_limit", SqlDbType.Int) { Value = getModel.per_month_qty_limit };
                    parm[18] = new SqlParameter("@dicount_percetage", SqlDbType.Int) { Value = getModel.dicount_percetage };
                    parm[19] = new SqlParameter("@device_id", SqlDbType.VarChar) { Value = getModel.device_id };



                    // Call the stored procedure for each employee
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);

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


        ///////////////// AUD EMPLOYEES IN LIST //////////////////////////
        public EmployeeReturn EmpBulkEditing(List<EmployeeModel> getModels) // Change parameter to accept a list of EmployeeModels
        {
            EmployeeReturn RObj = new EmployeeReturn(); // For result body object declaration

            int retstatus; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";
                foreach (var getModel in getModels) // Iterate over each employee model in the list
                {
                    // Array for parameters
                    var parm = new SqlParameter[3];
                    // Setting parameters value from the user input (getModel)
                    parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 10 };
                    parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                    parm[2] = new SqlParameter("@is_active", SqlDbType.Int) { Value = getModel.is_active };


                    // Call the stored procedure for each employee
                    var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);

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


        ///////////////// AUD EMPLOYEES IN LIST //////////////////////////
        public EmployeeReturn AUDSingleEmployees(EmployeeModel getModel) // Change parameter to accept a list of EmployeeModels
        {
            EmployeeReturn RObj = new EmployeeReturn(); // For result body object declaration

            int retstatus; string empId; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[28];
                // Setting parameters value from the user input (getModel)

                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 3 };
                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[2] = new SqlParameter("@emp_name", SqlDbType.VarChar) { Value = getModel.emp_name };
                parm[3] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[4] = new SqlParameter("@branch", SqlDbType.VarChar) { Value = getModel.branch };
                parm[5] = new SqlParameter("@department", SqlDbType.VarChar) { Value = getModel.department };
                parm[6] = new SqlParameter("@created_by", SqlDbType.VarChar) { Value = getModel.created_by };
                parm[7] = new SqlParameter("@update_by", SqlDbType.VarChar) { Value = getModel.update_by };
                parm[8] = new SqlParameter("@designation", SqlDbType.VarChar) { Value = getModel.designation };
                parm[9] = new SqlParameter("@emp_img", SqlDbType.VarChar) { Value = getModel.emp_img };
                parm[10] = new SqlParameter("@shift_start_time", SqlDbType.DateTime) { Value = getModel.shift_start_time };
                parm[11] = new SqlParameter("@shift_end_time", SqlDbType.DateTime) { Value = getModel.shift_end_time };
                parm[12] = new SqlParameter("@leave_allow", SqlDbType.Decimal) { Value = getModel.leave_allow };
                parm[13] = new SqlParameter("@curr_salary", SqlDbType.Decimal) { Value = getModel.curr_salary };
                parm[14] = new SqlParameter("@pass", SqlDbType.VarChar) { Value = getModel.pass };
                parm[15] = new SqlParameter("@client_code", SqlDbType.Int) { Value = getModel.client_code };
                parm[16] = new SqlParameter("@per_month_order_limit", SqlDbType.Int) { Value = getModel.per_month_order_limit };
                parm[17] = new SqlParameter("@per_month_qty_limit", SqlDbType.Int) { Value = getModel.per_month_qty_limit };
                parm[18] = new SqlParameter("@dicount_percetage", SqlDbType.Int) { Value = getModel.dicount_percetage };
                parm[19] = new SqlParameter("@ofc_lat", SqlDbType.VarChar) { Value = getModel.ofc_lat };
                parm[20] = new SqlParameter("@ofc_lon", SqlDbType.VarChar) { Value = getModel.ofc_lon };
                parm[21] = new SqlParameter("@role_id", SqlDbType.Int) { Value = getModel.role_id };
                parm[22] = new SqlParameter("@task_type_id", SqlDbType.Int) { Value = getModel.task_type_id };
                parm[23] = new SqlParameter("@image_extension", SqlDbType.VarChar) { Value = getModel.image_extension };
                parm[24] = new SqlParameter("@is_active", SqlDbType.Int) { Value = getModel.is_active };
                parm[25] = new SqlParameter("@office_id", SqlDbType.Int) { Value = getModel.office_id };
                parm[26] = new SqlParameter("@att_radius", SqlDbType.Int) { Value = getModel.att_radius };
                parm[27] = new SqlParameter("@device_id", SqlDbType.VarChar) { Value = getModel.device_id };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);

                foreach (DataRow row in dsuser.Rows)
                {
                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);

                    if (retstatus == 2 && getModel.emp_img != null)
                    {
                        //empId = Common.CheckIntegerNull(row["emp_id"]);
                        empId = getModel.emp_id;
                        // Define the path where the image will be saved
                        string folderPath = HttpContext.Current.Server.MapPath("~/EmpImage/");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Generate a unique file name
                        string fileName = empId + "." + getModel.image_extension;
                        string empIdString = empId;
                        string fullPath = Path.Combine(folderPath, fileName);
                        string[] existingFiles = Directory.GetFiles(folderPath, empIdString + ".*");

                        // Check if file already exists, and delete it if so

                        foreach (string file in existingFiles)
                        {
                            File.Delete(file);  // Delete the existing file
                        }

                        if (!string.IsNullOrEmpty(getModel.image_extension))
                        {
                            // Convert the Base64 string to a byte array
                            byte[] imageBytes = Convert.FromBase64String(getModel.emp_img);

                            // Save the byte array to the specified path as a file
                            File.WriteAllBytes(fullPath, imageBytes);
                        }
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

        public async Task<EmployeeReturn> SyncEmployees(EmployeeModel getModel)
        {
            EmployeeReturn RObj = new EmployeeReturn();
            int retstatus;
            string retmessage;
            string syncingClientCode = getModel.client_code;
            retstatus = 80;
            retmessage = "No Employee Api's Url Find by this Client!";
            try
            {
                //array for parameters
                var ApiUrlGetparm = new SqlParameter[3];
                //setting parameters value get from user
                ApiUrlGetparm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 2 };
                ApiUrlGetparm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = syncingClientCode };
                ApiUrlGetparm[2] = new SqlParameter("@server", SqlDbType.VarChar) { Value = getModel.server };

                //calling procedure

                var ApiUrlGetdsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Hrm_Api_Url_proc]", ApiUrlGetparm);


                // for single object declaration
                var ApiUrlGetRetObj = new EmployeeModel();
                // for list object declaration
                List<EmployeeModel> ApiUrlGetRetLst = new List<EmployeeModel>();
                // getting all rows
                foreach (DataRow row in ApiUrlGetdsuser.Rows)
                {
                    ApiUrlGetRetObj = new EmployeeModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    if (retstatus == 2)
                    {
                        ApiUrlGetRetObj.emp_id = Common.CheckStringNull(row["api_url"]);
                        //add all get object in list 
                        ApiUrlGetRetLst.Add(ApiUrlGetRetObj);
                    }


                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                //RObj.CompaniesDataList = ApiUrlGetRetLst;

                if (retstatus == 2)
                {
                    foreach (var ApiUrlGetItems in ApiUrlGetRetLst) // Iterate over each employee model in the list
                    {
                        try
                        {
                            // Define the API URL
                            string apiUrl = ApiUrlGetItems.emp_id;

                            // Create a new HttpClient instance
                            using (HttpClient client = new HttpClient())
                            {
                                // Send GET request to the API
                                HttpResponseMessage response = await client.GetAsync(apiUrl);

                                // Ensure the response is successful
                                response.EnsureSuccessStatusCode();

                                // Read the JSON response body
                                string jsonResponse = await response.Content.ReadAsStringAsync();

                                // Deserialize the JSON into an object
                                var apiResponse = JsonConvert.DeserializeObject<EmployeeApiResponse>(jsonResponse);

                                // Initialize the list for storing complaint data
                                List<EmployeeModel> RetLst = new List<EmployeeModel>();

                                // Map the API response "items" to the ComplainModel
                                foreach (var item in apiResponse.Items)
                                {
                                    EmployeeModel RetObj = new EmployeeModel
                                    {
                                        client_code = syncingClientCode,
                                        emp_id = item.emp_id,
                                        emp_name = item.emp_name,
                                        company = item.company,
                                        emp_father_name = item.emp_father_name,
                                        city = item.city,
                                        mobile_phone = item.mobile_phone,
                                        sex = item.sex,
                                        branch = item.branch,
                                        department = item.department,
                                        designation = item.designation,
                                        shift_start_time = item.shift_start_time,
                                        shift_end_time = item.shift_end_time,
                                        curr_salary = item.curr_salary,
                                        cnic = item.cnic,
                                        str_dob = item.str_dob,
                                        str_doj = item.str_doj,
                                        str_resign_date = item.str_resign_date,
                                        leave_allow = item.leave_allow,
                                        per_month_order_limit = item.per_month_order_limit,
                                        per_month_qty_limit = item.per_month_qty_limit,
                                        dicount_percetage = item.dicount_percetage,
                                        pass = item.pass,
                                        org_id = item.org_id,

                                    };

                                    // Add to the result list
                                    RetLst.Add(RetObj);
                                }


                                try
                                {

                                    foreach (var sendItems in RetLst) // Iterate over each employee model in the list
                                    {
                                        // Array for parameters
                                        var bkpParm = new SqlParameter[0];
                                        // Array for parameters
                                        var parm = new SqlParameter[25];
                                        // Setting parameters value from the user input (getModel)
                                        parm[0] = new SqlParameter("client_code", SqlDbType.VarChar) { Value = sendItems.client_code };
                                        parm[1] = new SqlParameter("emp_id", SqlDbType.VarChar) { Value = sendItems.emp_id };
                                        parm[2] = new SqlParameter("emp_name", SqlDbType.VarChar) { Value = sendItems.emp_name };
                                        parm[3] = new SqlParameter("Operation", SqlDbType.VarChar) { Value = 50 };
                                        parm[4] = new SqlParameter("company", SqlDbType.VarChar) { Value = sendItems.company };
                                        parm[5] = new SqlParameter("city", SqlDbType.VarChar) { Value = sendItems.city };
                                        parm[6] = new SqlParameter("mobile_phone", SqlDbType.VarChar) { Value = sendItems.mobile_phone };
                                        parm[7] = new SqlParameter("sex", SqlDbType.VarChar) { Value = sendItems.sex };
                                        parm[8] = new SqlParameter("emp_father_name", SqlDbType.VarChar) { Value = sendItems.emp_father_name };
                                        parm[9] = new SqlParameter("branch", SqlDbType.VarChar) { Value = sendItems.branch };
                                        parm[10] = new SqlParameter("department", SqlDbType.VarChar) { Value = sendItems.department };
                                        parm[11] = new SqlParameter("designation", SqlDbType.VarChar) { Value = sendItems.designation };
                                        parm[12] = new SqlParameter("shift_start_time", SqlDbType.VarChar) { Value = sendItems.shift_start_time };
                                        parm[13] = new SqlParameter("shift_end_time", SqlDbType.VarChar) { Value = sendItems.shift_end_time };
                                        parm[14] = new SqlParameter("curr_salary", SqlDbType.Int) { Value = sendItems.curr_salary };
                                        parm[15] = new SqlParameter("cnic", SqlDbType.VarChar) { Value = sendItems.cnic };
                                        parm[16] = new SqlParameter("str_dob", SqlDbType.VarChar) { Value = sendItems.str_dob };
                                        parm[17] = new SqlParameter("str_doj", SqlDbType.VarChar) { Value = sendItems.str_doj };
                                        parm[18] = new SqlParameter("str_resign_date", SqlDbType.VarChar) { Value = sendItems.str_resign_date };
                                        parm[19] = new SqlParameter("leave_allow", SqlDbType.Int) { Value = sendItems.leave_allow };
                                        parm[20] = new SqlParameter("per_month_order_limit", SqlDbType.Decimal) { Value = sendItems.per_month_order_limit };
                                        parm[21] = new SqlParameter("per_month_qty_limit", SqlDbType.Decimal) { Value = sendItems.per_month_qty_limit };
                                        parm[22] = new SqlParameter("dicount_percetage", SqlDbType.Decimal) { Value = sendItems.dicount_percetage };
                                        parm[23] = new SqlParameter("pass", SqlDbType.VarChar) { Value = sendItems.pass };
                                        parm[24] = new SqlParameter("org_id", SqlDbType.VarChar) { Value = sendItems.org_id };




                                        var employeeBkp = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_bkp_proc]", bkpParm);
                                        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_aud]", parm);
                                        foreach (DataRow row in dsuser.Rows)
                                        {
                                            retstatus = Common.CheckIntegerNull(row["res_status"]);
                                            retmessage = Common.CheckStringNull(row["res_message"]);
                                        }
                                    }

                                    RObj.Status = retstatus; // Success status
                                    RObj.Message = retmessage;


                                }
                                // If an error occurs during API execution
                                catch (Exception ex)
                                {
                                    RObj.Status = 60;
                                    RObj.Message = "Error Found, " + ex.ToString();
                                    return RObj;
                                }

                            }


                        }
                        catch (Exception ex)
                        {
                            RObj.Status = 70;
                            RObj.Message = "No Data Found From Your Given Api Error Found: " + ex.Message;
                            return RObj;
                        }
                    }

                }
                return RObj;
            }
            // if api calling error
            catch (Exception ex)
            {
                RObj.Status = 5;
                RObj.Message = "Error Found, " + ex.ToString();
                return RObj;
            }

        }

        public EmployeeReturn GetAssignableEmployee(EmployeeModel getModel) // get modal from user in getModel
        {
            EmployeeReturn RObj = new EmployeeReturn(); // for result body object declaration
            int retstatus; string retmessage; string strDOJ; string strDeactiveDate;

            try
            {
                //array for parameters
                var parm = new SqlParameter[6];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 6 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@department", SqlDbType.VarChar) { Value = getModel.department };
                parm[4] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[5] = new SqlParameter("@task_type_id", SqlDbType.Int) { Value = getModel.task_type_id };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                strDOJ = "";
                strDeactiveDate = "";

                // for single object declaration
                var RetObj = new EmployeeModel();
                // for list object declaration
                List<EmployeeModel> RetLst = new List<EmployeeModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new EmployeeModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    //coloumn mapping
                    if (retstatus == 2)
                    {
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        RetObj.company = Common.CheckStringNull(row["company"]);
                        RetObj.branch = Common.CheckStringNull(row["branch"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.creation_date = Common.CheckDateTimeNull(row["creation_date"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
                        RetObj.update_by = Common.CheckStringNull(row["update_by"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.emp_img = Common.CheckStringNull(row["emp_img"]);
                        RetObj.shift_start_time = Common.CheckStringNull(row["shift_start_time"]);
                        RetObj.shift_end_time = Common.CheckStringNull(row["shift_end_time"]);
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
                        RetObj.deactive_because = Common.CheckStringNull(row["deactive_because"]);
                        RetObj.deactive_date = Common.CheckDateTimeNull(row["deactive_date"]);
                        if (!string.IsNullOrEmpty(row["deactive_date"].ToString()) && row["deactive_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["deactive_date"]);
                            strDeactiveDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strDeactiveDate = "00-000-0000";
                        }
                        RetObj.str_deactive_date = strDeactiveDate;

                        RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
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

                        //add all get object in list 
                        RetLst.Add(RetObj);
                    }
                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.EmployeeDataList = RetLst;
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

        public async Task<EmployeeReturn> GetFilterEmp(EmployeeModel getModel)
        {
            EmployeeReturn RObj = new EmployeeReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new EmployeeModel
            {
                Operation = 11,
                emp_id = getModel.emp_id,
                company = getModel.company,
                department = getModel.department,
                master_acc = getModel.master_acc


            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/employees";


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
                        var apiResponse = JsonConvert.DeserializeObject<EmployeeApiResponse>(jsonResponse);

                        List<EmployeeModel> RetLst = new List<EmployeeModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            EmployeeModel RetObj = new EmployeeModel
                            {

                                Operation = item.Operation,
                                emp_id = item.emp_id,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,


                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.EmployeeDataList = RetLst;
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


        public async Task<EmployeeReturn> GetClearanceEmp(EmployeeModel getModel)
        {
            EmployeeReturn RObj = new EmployeeReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";

            var attendanceRequest = new EmployeeModel
            {
                Operation = 12,
                emp_id = getModel.emp_id,
                company = getModel.company,
                department = getModel.department,


            };

            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/employees";


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
                        var apiResponse = JsonConvert.DeserializeObject<EmployeeApiResponse>(jsonResponse);

                        List<EmployeeModel> RetLst = new List<EmployeeModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            EmployeeModel RetObj = new EmployeeModel
                            {

                                Operation = item.Operation,
                                emp_id = item.emp_id,
                                emp_id_to_show = item.emp_id_to_show,
                                emp_name = item.emp_name,


                            };
                            RetLst.Add(RetObj);
                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        if (retstatus == 2)
                        {
                            RObj.EmployeeDataList = RetLst;
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


        public EmployeeReturn GetMasterAccess(EmployeeModel getModel) // get modal from user in getModel
        {
            EmployeeReturn RObj = new EmployeeReturn(); // for result body object declaration
            int retstatus; string retmessage; string strDOJ; string strDeactiveDate;

            try
            {
                //array for parameters
                var parm = new SqlParameter[4];
                //setting parameters value get from user
                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 21 };
                parm[1] = new SqlParameter("@client_code", SqlDbType.VarChar) { Value = getModel.client_code };
                parm[2] = new SqlParameter("@company", SqlDbType.VarChar) { Value = getModel.company };
                parm[3] = new SqlParameter("@department", SqlDbType.VarChar) { Value = getModel.department };

                //calling procedure
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);
                retstatus = 50;
                retmessage = "No Data Found!";
                strDOJ = "";
                strDeactiveDate = "";

                // for single object declaration
                var RetObj = new EmployeeModel();
                // for list object declaration
                List<EmployeeModel> RetLst = new List<EmployeeModel>();
                // getting all rows
                foreach (DataRow row in dsuser.Rows)
                {
                    RetObj = new EmployeeModel(); //this important for all rows

                    retstatus = Common.CheckIntegerNull(row["res_status"]);
                    retmessage = Common.CheckStringNull(row["res_message"]);
                    //coloumn mapping
                    if (retstatus == 2)
                    {
                        RetObj.emp_id = Common.CheckStringNull(row["emp_id"]);
                        RetObj.client_code = Common.CheckStringNull(row["client_code"]);
                        RetObj.emp_name = Common.CheckStringNull(row["emp_name"]);
                        //    RetObj.company = Common.CheckStringNull(row["company_name"]);
                        RetObj.branch = Common.CheckStringNull(row["branch"]);
                        RetObj.department = Common.CheckStringNull(row["department"]);
                        RetObj.creation_date = Common.CheckDateTimeNull(row["creation_date"]);
                        RetObj.created_by = Common.CheckStringNull(row["created_by"]);
                        RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
                        RetObj.update_by = Common.CheckStringNull(row["update_by"]);
                        RetObj.designation = Common.CheckStringNull(row["designation"]);
                        RetObj.emp_img = Common.CheckStringNull(row["emp_img"]);
                        RetObj.shift_start_time = Common.CheckStringNull(row["shift_start_time"]);
                        RetObj.shift_end_time = Common.CheckStringNull(row["shift_end_time"]);
                        RetObj.leave_allow = Common.CheckDecimalNull(row["leave_allow"]);
                        RetObj.curr_salary = Common.CheckIntegerNull(row["curr_salary"]);
                        RetObj.pass = Common.CheckStringNull(row["pass"]);
                        RetObj.loan_limit = Common.CheckDecimalNull(row["loan_limit"]);
                        RetObj.cnic = Common.CheckStringNull(row["cnic"]);
                        RetObj.per_month_order_limit = Common.CheckIntegerNull(row["per_month_order_limit"]);
                        //  RetObj.this_month_order_limit = Common.CheckIntegerNull(row["this_month_order_limit"]);
                        RetObj.per_month_qty_limit = Common.CheckIntegerNull(row["per_month_qty_limit"]);
                        // RetObj.this_month_qty_limit = Common.CheckIntegerNull(row["this_month_qty_limit"]);
                        RetObj.dicount_percetage = Common.CheckIntegerNull(row["dicount_percetage"]);
                        RetObj.update_date = Common.CheckDateTimeNull(row["update_date"]);
                        RetObj.ofc_lat = Common.CheckStringNull(row["ofc_lat"]);
                        RetObj.ofc_lon = Common.CheckStringNull(row["ofc_lon"]);
                        RetObj.image_extension = Common.CheckStringNull(row["image_extension"]);
                        // RetObj.emp_image_url = Common.CheckStringNull(row["emp_image_url"]);
                        RetObj.task_type_id = Common.CheckIntegerNull(row["task_type_id"]);
                        // RetObj.role_name = Common.CheckStringNull(row["role_name"]);
                        RetObj.is_active = Common.CheckIntegerNull(row["is_active"]);
                        RetObj.role_id = Common.CheckIntegerNull(row["role_id"]);
                        RetObj.office_id = Common.CheckIntegerNull(row["office_id"]);
                        RetObj.att_radius = Common.CheckIntegerNull(row["att_radius"]);
                        RetObj.device_id = Common.CheckStringNull(row["device_id"]);
                        RetObj.deactive_because = Common.CheckStringNull(row["deactive_because"]);
                        RetObj.deactive_date = Common.CheckDateTimeNull(row["deactive_date"]);
                        if (!string.IsNullOrEmpty(row["deactive_date"].ToString()) && row["deactive_date"].ToString() != "")
                        {
                            DateTime ChequeOldDate = Convert.ToDateTime(row["deactive_date"]);
                            strDeactiveDate = ChequeOldDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            strDeactiveDate = "00-000-0000";
                        }
                        RetObj.str_deactive_date = strDeactiveDate;
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

                        //add all get object in list 
                        RetLst.Add(RetObj);
                    }
                }

                // status code shown assigining
                RObj.Status = retstatus;
                RObj.Message = retmessage;
                // if want to show in single object
                //RObj.EmployeeData = RetObj; 
                // if want to show in list of object
                RObj.EmployeeDataList = RetLst;
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

        public EmployeeReturn DeactiveEmployee(EmployeeModel getModel) // Deactive Employee
        {
            EmployeeReturn RObj = new EmployeeReturn(); // For result body object declaration

            int retstatus; string empId; string retmessage;
            try
            {
                retstatus = 50;
                retmessage = "No Data Found!";

                // Array for parameters
                var parm = new SqlParameter[3];
                // Setting parameters value from the user input (getModel)

                parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = 22 };
                parm[1] = new SqlParameter("@emp_id", SqlDbType.VarChar) { Value = getModel.emp_id };
                parm[2] = new SqlParameter("@deactive_because", SqlDbType.VarChar) { Value = getModel.deactive_because };


                // Call the stored procedure for each employee
                var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Employee_AUD]", parm);

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