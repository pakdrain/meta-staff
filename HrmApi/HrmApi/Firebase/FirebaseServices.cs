using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using HrmApi;
using HrmApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


using System.Configuration;
using static HrmApi.DAL.SQLHelper;
using HrmApi.DAL;
using System.Web.UI.WebControls;
using HrmApi.Views;
namespace HrmApi.FirbaseServices.Services
{
    public class FirbaseService
    {
        private readonly GoogleCredential _credential;
        private string _ConnString;
        //public BookingsMasterDAL()
        //{
        //    _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        //}


        public FirbaseService()
        {
            //string pathToConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Firbase/FirebaseService/firebase-configurations.json");
            //string pathToConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Firebase/FirebaseService/firebase-configurations.json");

            //using (var stream = new FileStream(pathToConfigFile, FileMode.Open, FileAccess.Read))
            //{
            //    _credential = GoogleCredential.FromStream(stream)
            //                  .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
            //}
            string pathToConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Firebase", "firebase-configurations.json");

            if (File.Exists(pathToConfigFile))
            {
                using (var stream = new FileStream(pathToConfigFile, FileMode.Open, FileAccess.Read))
                {
                    _credential = GoogleCredential.FromStream(stream)
                        .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
                }
            }
            else
            {
                // Log the error or throw an exception
                Console.WriteLine("The firebase-configurations.json file was not found at the expected location: " + pathToConfigFile);
            }

        }
        //public async Task<bool> SendBookingNotification(string token, string title, string body, )
        //{
        //    try
        //    {
        //        var httpClient = new HttpClient();

        //        // Debug _credential
        //        if (_credential == null || _credential.UnderlyingCredential == null)
        //        {
        //            throw new Exception("_credential or UnderlyingCredential is null");
        //        }

        //        var accessToken = await _credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        //        if (string.IsNullOrEmpty(accessToken))
        //        {
        //            throw new Exception("AccessToken is null or empty");
        //        }

        //        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        //        var message = new
        //        {
        //            message = new
        //            {
        //                token = token,
        //                notification = new
        //                {
        //                    title = title,
        //                    body = body
        //                },
        //                data = new Dictionary<string, string>
        //             {
        //                 { "ScreenNo", body },
        //                 { "title", title },
        //                 { "buttonType", ButtonType.ToString() },
        //                 //{ "studentId", StudentId },
        //                 //{ "position", Position.ToString() }
        //             },
        //                android = new { priority = "high" },
        //                apns = new
        //                {
        //                    headers = new Dictionary<string, string>
        //            {
        //                { "apns-priority", "10" }
        //            },
        //                    payload = new
        //                    {
        //                        aps = new
        //                        {
        //                            alert = new
        //                            {
        //                                title = title,
        //                                body = body
        //                            },
        //                            sound = "default"
        //                        }
        //                    }
        //                }
        //            }
        //        };

        //        var jsonMessage = JsonConvert.SerializeObject(message);
        //        var httpContent = new StringContent(jsonMessage, System.Text.Encoding.UTF8, "application/json");

        //        var response = await httpClient.PostAsync("https://fcm.googleapis.com/v1/projects/metasalon-33c58/messages:send", httpContent);
        //        return response.IsSuccessStatusCode;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return false;
        //    }
        //}

        public async Task<bool> SendBookingNotification(string token, string title, string body)
        {
            var isSent = false;
            var httpClient = new HttpClient();
            var accessToken = await _credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var message = new
            {
                message = new
                {
                    token = token,
                    notification = new
                    {
                        title = title,
                        body = body
                    },
                    //        data = new Dictionary<string, string>
                    //{
                    //    { "Booking_Code", booking.Booking_Code },
                    //    { "Customer_Name", booking.Customer_Name },
                    //    { "Booking_Date", booking.strBooking_Date },
                    //    { "Pkg_Name", booking.Pkg_Name },
                    //    { "Booking_Amount", booking.Booking_Total_Amount.ToString() }
                    //},
                    android = new { priority = "high" },
                    apns = new
                    {
                        headers = new Dictionary<string, string>
                {
                    { "apns-priority", "10" }
                },
                        payload = new
                        {
                            aps = new
                            {
                                alert = new
                                {
                                    title = title,
                                    body = body
                                },
                                sound = "default"
                            }
                        }
                    }
                }
            };

            var jsonMessage = JsonConvert.SerializeObject(message);
            var httpContent = new StringContent(jsonMessage, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://fcm.googleapis.com/v1/projects/employeeportal-be2b4/messages:send", httpContent);

            isSent = response.IsSuccessStatusCode;

            return isSent;
        }
        //public async Task<Bookings_Return> SendPushNotifier(string fcmtoken, string title, string body, Bookings_Model getModel)
        //{
        //    var notificatinos = new FirbaseService();
        //    Bookings_Return RObj = new Bookings_Return();
        //    BookingsMasterDAL RObj1 = new BookingsMasterDAL();
        //    string retstatus, retmessage;

        //    try
        //    {
        //        var parm = new SqlParameter[2];
        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Coperation };
        //        parm[1] = new SqlParameter("@Branch_Code", SqlDbType.VarChar) { Value = getModel.Branch_Code };

        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Usp_Salon_Bookings]", parm);

        //        var RetObj = new Bookings_Model();
        //        List<Bookings_Model> RetLst = new List<Bookings_Model>();

        //        retstatus = "2";
        //        retmessage = "No Data Found!";

        //        var StrBkg_Date = "";
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            retstatus = Common.CheckStringNull(row["Message_Code"]);
        //            retmessage = Common.CheckStringNull(row["Message_Title"]);

        //            if (retstatus == "1")
        //            {
        //                RetObj = new Bookings_Model();
        //                RetObj.Booking_Code = Common.CheckStringNull(row["Booking_Code"]);
        //                RetObj.Branch_Code = Common.CheckStringNull(row["Branch_Code"]);
        //                RetObj.Customer_Code = Common.CheckStringNull(row["Customer_Code"]);
        //                RetObj.Customer_Name = Common.CheckStringNull(row["Customer_Name"]);
        //                RetObj.Customer_Mobile_No = Common.CheckStringNull(row["Customer_Mobile_No"]);
        //                RetObj.Seat_Code = Common.CheckStringNull(row["Seat_Code"]);
        //                RetObj.Booking_Date = Common.CheckDateTimeNull(row["Booking_Date"]);
        //                RetObj.strBooking_Date = RetObj.Booking_Date.ToString("dd-MM-yyyy") ?? "00-00-0000";
        //                RetObj.Booking_Time = Common.CheckDateTimeNull(row["Booking_Time"]);
        //                RetObj.Pkg_Code = Common.CheckStringNull(row["Pkg_Code"]);
        //                RetObj.Pkg_Name = Common.CheckStringNull(row["Pkg_Name"]);
        //                RetObj.Booking_Description = Common.CheckStringNull(row["Booking_Description"]);
        //                RetObj.Booking_Total_Amount = Common.CheckDecimalNull(row["Booking_Total_Amount"]);
        //                RetObj.Booking_Net_Amount = Common.CheckDecimalNull(row["Booking_Net_Amount"]);
        //                RetObj.Tax_Amount = Common.CheckDecimalNull(row["Tax_Amount"]);
        //                RetObj.Working_Status = Common.CheckIntegerNull(row["Working_Status"]);

        //                BookingsDetail_Model pdm = new BookingsDetail_Model
        //                {
        //                    Coperation = 1,
        //                    Branch_Code = RetObj.Branch_Code,
        //                    Booking_Code = RetObj.Booking_Code
        //                };

        //                BookingsDetail_Return glist = RObj1.GetBookingsDetailList(pdm);
        //                if (glist.Status == "1")
        //                {
        //                    RetObj.BkgDetails = glist.BookingsDetails_List;
        //                }

        //                RetLst.Add(RetObj);
        //            }
        //        }

        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        RObj.Bookings_List = RetLst;

        //        // Send notification with booking details
        //        foreach (var booking in RetLst)
        //        {
        //            var notificationBody = new
        //            {
        //                Booking_Code = booking.Booking_Code,
        //                Customer_Name = booking.Customer_Name,
        //                Booking_Date = booking.strBooking_Date,
        //                Pkg_Name = booking.Pkg_Name,
        //                Booking_Amount = booking.Booking_Total_Amount.ToString()
        //            };

        //            var notificationJson = JsonConvert.SerializeObject(notificationBody);
        //            await notificatinos.SendBookingNotification(fcmtoken, title, notificationJson);
        //        }

        //        return RObj;
        //    }
        //    catch (Exception ex)
        //    {
        //        RObj.Status = "5";
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }
        //}


        //public async Task<Bookings_Return> SendPushNotifier(string fcmtoken, string title, string body, Bookings_Model getModel)
        //{
        //    var notificatinos = new FirbaseService();
        //    Bookings_Return RObj = new Bookings_Return();
        //    BookingsMasterDAL RObj1 = new BookingsMasterDAL();
        //    string retstatus, retmessage;
        //    try
        //    {
        //        var parm = new SqlParameter[2];

        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Coperation };
        //        parm[1] = new SqlParameter("@Branch_Code", SqlDbType.VarChar) { Value = getModel.Branch_Code };

        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Usp_Salon_Bookings]", parm);

        //        var RetObj = new Bookings_Model();
        //        List<Bookings_Model> RetLst = new List<Bookings_Model>();

        //        retstatus = "2";
        //        retmessage = "No Data Found!";

        //        var StrBkg_Date = "";
        //        foreach (DataRow row in dsuser.Rows)
        //        {
        //            retstatus = Common.CheckStringNull(row["Message_Code"]);
        //            retmessage = Common.CheckStringNull(row["Message_Title"]);

        //            if (retstatus == "1")
        //            {
        //                RetObj = new Bookings_Model();
        //                RetObj.Booking_Code = Common.CheckStringNull(row["Booking_Code"]);
        //                RetObj.Branch_Code = Common.CheckStringNull(row["Branch_Code"]);
        //                RetObj.Customer_Code = Common.CheckStringNull(row["Customer_Code"]);
        //                RetObj.Customer_Name = Common.CheckStringNull(row["Customer_Name"]);
        //                RetObj.Customer_Mobile_No = Common.CheckStringNull(row["Customer_Mobile_No"]);
        //                RetObj.Seat_Code = Common.CheckStringNull(row["Seat_Code"]);
        //                RetObj.Booking_Date = Common.CheckDateTimeNull(row["Booking_Date"]);
        //                if (!string.IsNullOrEmpty(row["Booking_Date"].ToString()) && row["Booking_Date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["Booking_Date"]);
        //                    StrBkg_Date = ChequeOldDate.ToString("dd-MM-yyyy");
        //                }
        //                else
        //                {
        //                    StrBkg_Date = "00-00-0000";
        //                }
        //                RetObj.strBooking_Date = StrBkg_Date;
        //                RetObj.Booking_Time = Common.CheckDateTimeNull(row["Booking_Time"]);
        //                RetObj.Pkg_Code = Common.CheckStringNull(row["Pkg_Code"]);
        //                RetObj.Pkg_Name = Common.CheckStringNull(row["Pkg_Name"]);
        //                RetObj.Booking_Description = Common.CheckStringNull(row["Booking_Description"]);
        //                RetObj.Booking_Total_Amount = Common.CheckDecimalNull(row["Booking_Total_Amount"]);
        //                RetObj.Booking_Net_Amount = Common.CheckDecimalNull(row["Booking_Net_Amount"]);
        //                RetObj.Tax_Amount = Common.CheckDecimalNull(row["Tax_Amount"]);
        //                RetObj.Working_Status = Common.CheckIntegerNull(row["Working_Status"]);
        //                body= Common.CheckStringNull(row["body"]);
        //                title = Common.CheckStringNull(row["title"]);
        //                fcmtoken= Common.CheckStringNull(row["fcmtoken"]);
        //                RetObj.Working_Status = Common.CheckIntegerNull(row["Working_Status"]);

        //                BookingsDetail_Model pdm = new BookingsDetail_Model();
        //                pdm.Coperation = 1;
        //                pdm.Branch_Code = RetObj.Branch_Code;
        //                pdm.Booking_Code = RetObj.Booking_Code;

        //                BookingsDetail_Return glist = RObj1.GetBookingsDetailList(pdm);

        //                if (glist.Status == "1")
        //                {
        //                    RetObj.BkgDetails = glist.BookingsDetails_List;
        //                }

        //                RetLst.Add(RetObj);
        //            }
        //        }

        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        RObj.Bookings_List = RetLst;

        //        await notificatinos.SendBookingNotification(fcmtoken, title, body);

        //        return RObj;
        //    }
        //    catch (Exception ex)
        //    {
        //        RObj.Status = "5";
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }
        //}

        //public async Bookings_Return SendPushNotifier(string fcmtoken, string title, string body, Bookings_Model getModel)
        //{
        //    var notificatinos = new FirbaseService();
        //    Bookings_Return RObj = new Bookings_Return();
        //    BookingsMasterDAL RObj1 = new BookingsMasterDAL();
        //    string retstatus, retmessage;
        //    try
        //    {
        //        var parm = new SqlParameter[2];

        //        parm[0] = new SqlParameter("@Operation", SqlDbType.Int) { Value = getModel.Coperation };
        //        parm[1] = new SqlParameter("@Branch_Code", SqlDbType.VarChar) { Value = getModel.Branch_Code };

        //        var dsuser = SqlHelper.ExecuteTable(_ConnString, CommandType.StoredProcedure, "[Usp_Salon_Bookings]", parm);

        //        var RetObj = new Bookings_Model();
        //        List<Bookings_Model> RetLst = new List<Bookings_Model>();

        //        retstatus = "2";
        //        retmessage = "No Data Found!";

        //        var StrBkg_Date = "";
        //        foreach (DataRow row in dsuser.Rows)
        //        {

        //            retstatus = Common.CheckStringNull(row["Message_Code"]);
        //            retmessage = Common.CheckStringNull(row["Message_Title"]);

        //            if (retstatus == "1")
        //            {
        //                RetObj = new Bookings_Model();
        //                RetObj.Booking_Code = Common.CheckStringNull(row["Booking_Code"]);
        //                RetObj.Branch_Code = Common.CheckStringNull(row["Branch_Code"]);
        //                RetObj.Customer_Code = Common.CheckStringNull(row["Customer_Code"]);
        //                RetObj.Customer_Name = Common.CheckStringNull(row["Customer_Name"]);
        //                RetObj.Customer_Mobile_No = Common.CheckStringNull(row["Customer_Mobile_No"]);
        //                RetObj.Seat_Code = Common.CheckStringNull(row["Seat_Code"]);
        //                RetObj.Booking_Date = Common.CheckDateTimeNull(row["Booking_Date"]);
        //                if (!string.IsNullOrEmpty(row["Booking_Date"].ToString()) && row["Booking_Date"].ToString() != "")
        //                {
        //                    DateTime ChequeOldDate = Convert.ToDateTime(row["Booking_Date"]);
        //                    StrBkg_Date = ChequeOldDate.ToString("dd-MM-yyyy");
        //                }
        //                else
        //                {
        //                    StrBkg_Date = "00-00-0000";
        //                }
        //                RetObj.strBooking_Date = StrBkg_Date;
        //                RetObj.Booking_Time = Common.CheckDateTimeNull(row["Booking_Time"]);
        //                RetObj.Pkg_Code = Common.CheckStringNull(row["Pkg_Code"]);
        //                RetObj.Pkg_Name = Common.CheckStringNull(row["Pkg_Name"]);
        //                RetObj.Booking_Description = Common.CheckStringNull(row["Booking_Description"]);
        //                RetObj.Booking_Total_Amount = Common.CheckDecimalNull(row["Booking_Total_Amount"]);
        //                RetObj.Booking_Net_Amount = Common.CheckDecimalNull(row["Booking_Net_Amount"]);
        //                RetObj.Tax_Amount = Common.CheckDecimalNull(row["Tax_Amount"]);
        //                RetObj.Working_Status = Common.CheckIntegerNull(row["Working_Status"]);

        //                //BookingsDetail_Model sa = new BookingsDetail_Model();
        //                //sa.Coperation = 1;
        //                //sa.Branch_Code = getModel.Branch_Code;
        //                //sa.Booking_Code = getModel.Booking_Code;
        //                //var getbk = GetBookingsDetailList(sa);
        //                //RetObj.BkgDetails = getbk.BookingsDetails_List;



        //                BookingsDetail_Model pdm = new BookingsDetail_Model();

        //                pdm.Coperation = 1;
        //                pdm.Branch_Code = RetObj.Branch_Code;
        //                pdm.Booking_Code = RetObj.Booking_Code;

        //                BookingsDetail_Return glist = RObj1.GetBookingsDetailList(pdm);

        //                if (glist.Status == "1")
        //                {
        //                    RetObj.BkgDetails = glist.BookingsDetails_List;
        //                }

        //                RetLst.Add(RetObj);
        //            }
        //        }

        //        RObj.Status = retstatus;
        //        RObj.Message = retmessage;
        //        RObj.Bookings_List = RetLst;

        //        return RObj;
        //    }
        //    catch (Exception ex)
        //    {
        //        RObj.Status = "5";
        //        RObj.Message = "Error Found, " + ex.ToString();
        //        return RObj;
        //    }
        //    await notificatinos.SendBookingNotification(fcmtoken, title, body);
        //    //return true;
        //}


        //public async Task<bool> SendPushNotification(string token, string body = "", string title = "", string StudentId = "", int ButtonType = 0, int Position = 0)
        //{
        //    var issend = false;
        //    var httpClient = new HttpClient();
        //    var accessToken = await _credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        //    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        //    var message = new
        //    {
        //        message = new
        //        {
        //            token = token,
        //            notification = new
        //            {
        //                title = title,
        //                body = body
        //            },
        //            data = new Dictionary<string, string>
        //             {
        //                 { "body", body },
        //                 { "title", title },
        //                 { "buttonType", ButtonType.ToString() },
        //                 { "studentId", StudentId },
        //                 { "position", Position.ToString() }
        //             },
        //            android = new
        //            {
        //                priority = "high"
        //            },
        //            apns = new
        //            {
        //                headers = new Dictionary<string, string>
        //            {
        //                { "apns-priority", "10" },  // High priority for iOS
        //                { "apns-expiration", "1604750400" }  // Expiration for the notification (optional)
        //            },
        //                payload = new
        //                {
        //                    aps = new
        //                    {
        //                        alert = new
        //                        {
        //                            title = title,
        //                            body = body
        //                        },
        //                        badge = 1,  // Badge number for iOS icon
        //                        sound = "default"  // Sound to play on receiving notification
        //                    }
        //                }
        //            }
        //        }

        //    };


        //    var jsonMessage = JsonConvert.SerializeObject(message);
        //    var httpContent = new StringContent(jsonMessage, System.Text.Encoding.UTF8, "application/json");

        //    var response = await httpClient.PostAsync("https://fcm.googleapis.com/v1/projects/metasalon-33c58/messages:send", httpContent);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        issend = true;
        //        Console.WriteLine("Notification sent successfully.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to send notification. Status code: " + response.StatusCode);
        //    }
        //    return issend;
        //}
    }
}