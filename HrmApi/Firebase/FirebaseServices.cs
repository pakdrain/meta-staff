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
        //public async Task<bool> SendBookingNotification(string token, string title, string body, string screen)
        //{
        //    var isSent = false;
        //    var httpClient = new HttpClient();

        //    // Debug _credential
        //    if (_credential == null || _credential.UnderlyingCredential == null)
        //    {
        //        throw new Exception("_credential or UnderlyingCredential is null");
        //    }

        //    var accessToken = await _credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        //    if (string.IsNullOrEmpty(accessToken))
        //    {
        //        throw new Exception("AccessToken is null or empty");
        //    }

        //    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        //    //var httpClient = new HttpClient();
        //    //var accessToken = await _credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        //    //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        //    var message = new
        //    {
        //        token = token, // Changed from "token" to "to" for standard FCM format
        //        notification = new
        //        {
        //            title = title,
        //            body = body,
        //            screen = screen,
        //        },
        //        data = new
        //        {
        //            screen = screen,
        //            // Add any additional data fields you need
        //            // id = "123" 
        //        },
        //        android = new { priority = "high" },
        //        apns = new
        //        {
        //            headers = new Dictionary<string, string>
        //    {
        //        { "apns-priority", "10" }
        //    },
        //            payload = new
        //            {
        //                aps = new
        //                {
        //                    alert = new
        //                    {
        //                        title = title,
        //                        body = body
        //                    },
        //                    sound = "default"
        //                }
        //            }

        //        }
        //    };

        //    var jsonMessage = JsonConvert.SerializeObject(message);
        //    var httpContent = new StringContent(jsonMessage, System.Text.Encoding.UTF8, "application/json");

        //    try
        //    {
        //        var response = await httpClient.PostAsync(
        //            "https://fcm.googleapis.com/v1/projects/employeeportal-be2b4/messages:send",
        //            httpContent);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseContent = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"Notification sent successfully: {responseContent}");
        //        }
        //        else
        //        {
        //            var errorContent = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"Failed to send notification: {errorContent}");
        //        }

        //        isSent = response.IsSuccessStatusCode;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception while sending notification: {ex.Message}");
        //    }

        //    return isSent;
        //}



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

        public async Task<bool> SendBookingNotification(string token, string title, string body, string screen)
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


    }
}