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
    public class VehicleDAL
    {
        private string _ConnString; //variable for connection string
        public VehicleDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region Vehicle

        public async Task<VehiclesReturn> GetMyBuyPack(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                buy_back_amt = item.buy_back_amt,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> GetAppliedBuyPack(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 12,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                buy_back_amt = item.buy_back_amt,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> GetBuyPackById(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 5,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                Installments = item.Installments,
                                
                                buy_back_amt = item.buy_back_amt,
                                yearly_inss = item.yearly_inss,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> GetHodBuyPack(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 7,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                buy_back_amt = item.buy_back_amt,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> GetGmBuyPack(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 8,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                buy_back_amt = item.buy_back_amt,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> GetAdminBuyPack(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 9,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                buy_back_amt = item.buy_back_amt,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,
                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> GetEdBuyPack(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 10,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                buy_back_amt = item.buy_back_amt,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> GetHrBuyPack(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 11,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date =getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,


            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesModel RetObj = new VehiclesModel
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                company = item.company,
                                department = item.department,
                                emp_id = item.emp_id,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp,
                                status = item.status,

                                hod_app_by = item.hod_app_by,
                                gm_app_by = item.gm_app_by,
                                hr_first_app_by = item.hr_first_app_by,
                                hr_second_app_by = item.hr_second_app_by,
                                admin_app_by = item.admin_app_by,
                                ed_app_by = item.ed_app_by,
                                close_by = item.close_by,
                                rej_by = item.rej_by,

                                hod_remarks = item.hod_remarks,
                                gm_remarks = item.gm_remarks,
                                hr_first_remarks = item.hr_first_remarks,
                                hr_second_remarks = item.hr_second_remarks,
                                admin_remarks = item.admin_remarks,
                                ed_remarks = item.ed_remarks,
                                close_remarks = item.close_remarks,
                                rej_remarks = item.rej_remarks,

                                hod_app_date = item.hod_app_date,
                                str_hod_app_date = item.str_hod_app_date,
                                gm_app_date = item.gm_app_date,
                                str_gm_app_date = item.str_gm_app_date,
                                hr_first_app_date = item.hr_first_app_date,
                                str_hr_first_app_date = item.str_hr_first_app_date,
                                hr_second_app_date = item.hr_second_app_date,
                                str_hr_second_app_date = item.str_hr_second_app_date,
                                admin_app_date = item.admin_app_date,
                                str_admin_app_date = item.str_admin_app_date,
                                ed_app_date = item.ed_app_date,
                                str_ed_app_date = item.str_ed_app_date,
                                close_date = item.close_date,
                                str_close_date = item.str_close_date,
                                rej_date = item.rej_date,
                                str_rej_date = item.str_rej_date,

                                action_page = item.action_page,
                                created_by = item.created_by,
                                creation_date = item.creation_date,
                                str_creation_date = item.str_creation_date,
                                last_updated_by = item.last_updated_by,
                                last_update_date = item.last_update_date,
                                str_last_update_date = item.str_last_update_date,

                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                buy_back_amt = item.buy_back_amt,
                                market_value = item.market_value,
                                reg_amount = item.reg_amount,
                                first_year_inss_amt = item.first_year_inss_amt,
                                no_of_installment = item.no_of_installment,
                                per_mon_installment = item.per_mon_installment,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDataList = RetLst;
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

        public async Task<VehiclesReturn> AudVehicles(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            int newVehicleId = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newVehicleId = item.new_vehicle_id;

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_vehicle_id = newVehicleId;


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

        public async Task<VehiclesReturn> BuyPackStatusChanger(VehiclesModel getModel)
        {
            VehiclesReturn RObj = new VehiclesReturn();
            int retstatus = 80;
            int newVehicleId = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesModel
            {
                Operation = 6,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                company = getModel.company,
                department = getModel.department,
                str_from_date = getModel.str_from_date,
                str_to_date = getModel.str_to_date,
                emp_id = getModel.emp_id,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp,
                status = getModel.status,

                hod_app_by = getModel.hod_app_by,
                gm_app_by = getModel.gm_app_by,
                hr_first_app_by = getModel.hr_first_app_by,
                hr_second_app_by = getModel.hr_second_app_by,
                admin_app_by = getModel.admin_app_by,
                ed_app_by = getModel.ed_app_by,
                close_by = getModel.close_by,
                rej_by = getModel.rej_by,

                hod_remarks = getModel.hod_remarks,
                gm_remarks = getModel.gm_remarks,
                hr_first_remarks = getModel.hr_first_remarks,
                hr_second_remarks = getModel.hr_second_remarks,
                admin_remarks = getModel.admin_remarks,
                ed_remarks = getModel.ed_remarks,
                close_remarks = getModel.close_remarks,
                rej_remarks = getModel.rej_remarks,

                hod_app_date = getModel.hod_app_date,
                str_hod_app_date = getModel.str_hod_app_date,
                gm_app_date = getModel.gm_app_date,
                str_gm_app_date = getModel.str_gm_app_date,
                hr_first_app_date = getModel.hr_first_app_date,
                str_hr_first_app_date = getModel.str_hr_first_app_date,
                hr_second_app_date = getModel.hr_second_app_date,
                str_hr_second_app_date = getModel.str_hr_second_app_date,
                admin_app_date = getModel.admin_app_date,
                str_admin_app_date = getModel.str_admin_app_date,
                ed_app_date = getModel.ed_app_date,
                str_ed_app_date = getModel.str_ed_app_date,
                close_date = getModel.close_date,
                str_close_date = getModel.str_close_date,
                rej_date = getModel.rej_date,
                str_rej_date = getModel.str_rej_date,

                action_page = getModel.action_page,
                created_by = getModel.created_by,
                creation_date = getModel.creation_date,
                str_creation_date = getModel.str_creation_date,
                last_updated_by = getModel.last_updated_by,
                last_update_date = getModel.last_update_date,
                str_last_update_date = getModel.str_last_update_date,

                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                buy_back_amt = getModel.buy_back_amt,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/vehicle-buy-pack";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesApiResponse>(jsonResponse);

                        List<VehiclesModel> RetLst = new List<VehiclesModel>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newVehicleId = item.new_vehicle_id;

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_vehicle_id = newVehicleId;


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

        public async Task<OwnedVehicleReturn> GetOwnedVehicle(OwnedVehicle getModel)
        {
            OwnedVehicleReturn RObj = new OwnedVehicleReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new OwnedVehicle
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                desig_id = getModel.desig_id,
                desig_name = getModel.desig_name,
                dep_id = getModel.dep_id,
                dep_name = getModel.dep_name,
                doj = getModel.doj,
                str_doj = getModel.str_doj,
                employment_type = getModel.employment_type,
                division_id = getModel.division_id,
                current_function_type = getModel.current_function_type,
                buy_pack_elig = getModel.buy_pack_elig,
                vehicle_type = getModel.vehicle_type,
                vehicle_category = getModel.vehicle_category,
                reg_no = getModel.reg_no,
                registration_date = getModel.registration_date,
                str_registration_date = getModel.str_registration_date,
                owner_company = getModel.owner_company,
                maker = getModel.maker,
                yom = getModel.yom,
                engine_no = getModel.engine_no,
                chassis = getModel.chassis,
                vehicle_cost = getModel.vehicle_cost,
                hand_over_date = getModel.hand_over_date,
                str_hand_over_date = getModel.str_hand_over_date,
                desig_eligible = getModel.desig_eligible,
                min_sal_req = getModel.min_sal_req,
                app_in_two_mon = getModel.app_in_two_mon,
                tenure_limit_comp = getModel.tenure_limit_comp

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/owned-vehicle";


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
                        var apiResponse = JsonConvert.DeserializeObject<OwnedVehicleApiResponse>(jsonResponse);

                        List<OwnedVehicle> RetLst = new List<OwnedVehicle>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            OwnedVehicle RetObj = new OwnedVehicle
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                desig_id = item.desig_id,
                                desig_name = item.desig_name,
                                dep_id = item.dep_id,
                                dep_name = item.dep_name,
                                doj = item.doj,
                                str_doj = item.str_doj,
                                employment_type = item.employment_type,
                                division_id = item.division_id,
                                current_function_type = item.current_function_type,
                                buy_pack_elig = item.buy_pack_elig,
                                vehicle_type = item.vehicle_type,
                                vehicle_category = item.vehicle_category,
                                reg_no = item.reg_no,
                                registration_date = item.registration_date,
                                str_registration_date = item.str_registration_date,
                                owner_company = item.owner_company,
                                maker = item.maker,
                                yom = item.yom,
                                engine_no = item.engine_no,
                                chassis = item.chassis,
                                vehicle_cost = item.vehicle_cost,
                                hand_over_date = item.hand_over_date,
                                str_hand_over_date = item.str_hand_over_date,
                                desig_eligible = item.desig_eligible,
                                min_sal_req = item.min_sal_req,
                                app_in_two_mon = item.app_in_two_mon,
                                tenure_limit_comp = item.tenure_limit_comp

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.OwnedVehicleDataList = RetLst;
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

        public async Task<VehiclesDetReturn> GenerateInstallment(VehiclesDet getModel)
        {
            VehiclesDetReturn RObj = new VehiclesDetReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new VehiclesDet
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                company = getModel.company,
                emp_id = getModel.emp_id,
                det_id = getModel.det_id,
                hrm_vehicle_id = getModel.hrm_vehicle_id,
                instal_no = getModel.instal_no,
                instal_date = getModel.instal_date,
                str_instal_date = getModel.str_instal_date,
                instal_amount = getModel.instal_amount,
                inss_amount = getModel.inss_amount,
                reg_amount = getModel.reg_amount,
                total_amount = getModel.total_amount,
                no_of_installment = getModel.no_of_installment,
                market_value = getModel.market_value,
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/gen-buy-pack-instal";


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
                        var apiResponse = JsonConvert.DeserializeObject<VehiclesDetApiResponse>(jsonResponse);

                        List<VehiclesDet> RetLst = new List<VehiclesDet>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            VehiclesDet RetObj = new VehiclesDet
                            {
                                res_status = item.res_status,
                                res_message = item.res_message,
                                company = item.company,
                                emp_id = item.emp_id,
                                det_id = item.det_id,
                                hrm_vehicle_id = item.hrm_vehicle_id,
                                instal_no = item.instal_no,
                                instal_date = item.instal_date,
                                str_instal_date = item.str_instal_date,
                                instal_amount = item.instal_amount,
                                inss_amount = item.inss_amount,
                                reg_amount = item.reg_amount,
                                total_amount = item.total_amount,
                                no_of_installment = item.no_of_installment,

                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.VehiclesDetDataList = RetLst;
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


      
       
        #endregion
    }
}