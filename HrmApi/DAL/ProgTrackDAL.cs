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
using System.IO;
using Google.Protobuf;

namespace HrmApi.DAL
{
    public class ProgTrackDAL
    {
        private string _ConnString; //variable for connection string
        public ProgTrackDAL() //constructor call for connection 
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        //////////////// CONNECTION STRING DAL //////////////////
        public string GetConnectionString()
        {
            return _ConnString;
        }

        #region ProgTrack

        public async Task<ProgTrackReturn> GetProjects(ProgTrack getModel)
        {
            ProgTrackReturn RObj = new ProgTrackReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ProgTrack
            {
                Operation = 1,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_track_id = getModel.new_track_id,
                track_id = getModel.track_id,
                proj_id = getModel.proj_id,
                proj_name = getModel.proj_name,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                status = getModel.status,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                comp_id = getModel.comp_id,
                dept_id = getModel.dept_id,
                remarks = getModel.remarks,
                task_type = getModel.task_type,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/prog-track";


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
                        var apiResponse = JsonConvert.DeserializeObject<ProgTrackApiResponse>(jsonResponse);

                        List<ProgTrack> RetLst = new List<ProgTrack>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ProgTrack RetObj = new ProgTrack
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                new_track_id = item.new_track_id,
                                track_id = item.track_id,
                                proj_id = item.proj_id,
                                proj_name = item.proj_name,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                status = item.status,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                comp_id = item.comp_id,
                                dept_id = item.dept_id,
                                remarks = item.remarks,
                                task_type = item.task_type,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ProgTrackDataList = RetLst;
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
        public async Task<ProgTrackReturn> GetTrackReport(ProgTrack getModel)
        {
            ProgTrackReturn RObj = new ProgTrackReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ProgTrack
            {
                Operation = 6,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_track_id = getModel.new_track_id,
                track_id = getModel.track_id,
                proj_id = getModel.proj_id,
                proj_name = getModel.proj_name,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                status = getModel.status,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                comp_id = getModel.comp_id,
                dept_id = getModel.dept_id,
                remarks = getModel.remarks,
                task_type = getModel.task_type,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/prog-track";


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
                        var apiResponse = JsonConvert.DeserializeObject<ProgTrackApiResponse>(jsonResponse);

                        List<ProgTrack> RetLst = new List<ProgTrack>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ProgTrack RetObj = new ProgTrack
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                new_track_id = item.new_track_id,
                                track_id = item.track_id,
                                proj_id = item.proj_id,
                                proj_name = item.proj_name,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                status = item.status,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                comp_id = item.comp_id,
                                dept_id = item.dept_id,
                                remarks = item.remarks,
                                task_type = item.task_type,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ProgTrackDataList = RetLst;
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
        public async Task<ProgTrackReturn> GetTrackStatus(ProgTrack getModel)
        {
            ProgTrackReturn RObj = new ProgTrackReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            var attendanceRequest = new ProgTrack
            {
                Operation = 7,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_track_id = getModel.new_track_id,
                track_id = getModel.track_id,
                proj_id = getModel.proj_id,
                proj_name = getModel.proj_name,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                status = getModel.status,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                comp_id = getModel.comp_id,
                dept_id = getModel.dept_id,
                remarks = getModel.remarks,
                task_type = getModel.task_type,
                app_user = getModel.app_user
            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/prog-track";


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
                        var apiResponse = JsonConvert.DeserializeObject<ProgTrackApiResponse>(jsonResponse);

                        List<ProgTrack> RetLst = new List<ProgTrack>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;

                            ProgTrack RetObj = new ProgTrack
                            {
                                Operation = item.Operation,
                                res_status = item.res_status,
                                res_message = item.res_message,
                                new_track_id = item.new_track_id,
                                track_id = item.track_id,
                                proj_id = item.proj_id,
                                proj_name = item.proj_name,
                                emp_id = item.emp_id,
                                emp_name = item.emp_name,
                                status = item.status,
                                start_date = item.start_date,
                                str_start_date = item.str_start_date,
                                end_date = item.end_date,
                                str_end_date = item.str_end_date,
                                created_by = item.created_by,
                                created_date = item.created_date,
                                str_created_date = item.str_created_date,
                                updated_by = item.updated_by,
                                updated_date = item.updated_date,
                                str_updated_date = item.str_updated_date,
                                comp_id = item.comp_id,
                                dept_id = item.dept_id,
                                remarks = item.remarks,
                                task_type = item.task_type,


                            };
                            RObj.Status = retstatus;
                            RObj.Message = retmessage;
                            RetLst.Add(RetObj);

                        }
                        RObj.Status = retstatus;
                        RObj.Message = retmessage;

                        if (retstatus == 2)
                        {
                            RObj.ProgTrackDataList = RetLst;
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
        public async Task<ProgTrackReturn> AUDProgTrack(ProgTrack getModel)
        {
            ProgTrackReturn RObj = new ProgTrackReturn();
            int retstatus = 80;
            string retmessage = "Api is not Fetching!";
            int newWpId = 0;
            int operation = getModel.Operation;

            var attendanceRequest = new ProgTrack
            {

                Operation = getModel.Operation,
                res_status = getModel.res_status,
                res_message = getModel.res_message,
                new_track_id = getModel.new_track_id,
                track_id = getModel.track_id,
                proj_id = getModel.proj_id,
                proj_name = getModel.proj_name,
                emp_id = getModel.emp_id,
                emp_name = getModel.emp_name,
                status = getModel.status,
                start_date = getModel.start_date,
                str_start_date = getModel.str_start_date,
                end_date = getModel.end_date,
                str_end_date = getModel.str_end_date,
                created_by = getModel.created_by,
                created_date = getModel.created_date,
                str_created_date = getModel.str_created_date,
                updated_by = getModel.updated_by,
                updated_date = getModel.updated_date,
                str_updated_date = getModel.str_updated_date,
                comp_id = getModel.comp_id,
                dept_id = getModel.dept_id,
                remarks = getModel.remarks,
                task_type = getModel.task_type,

            };


            try
            {

                string apiUrl = "http://portal.sabirsgroup.com:8184/ords/sabroso_ords/hrms/prog-track";


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
                        var apiResponse = JsonConvert.DeserializeObject<ProgTrackApiResponse>(jsonResponse);

                        List<ProgTrack> RetLst = new List<ProgTrack>();

                        foreach (var item in apiResponse.Items)
                        {
                            retstatus = item.res_status;
                            retmessage = item.res_message;
                            newWpId = item.new_track_id;

                        }
                        


                        RObj.Status = retstatus;
                        RObj.Message = retmessage;
                        RObj.new_proj_id = newWpId;
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