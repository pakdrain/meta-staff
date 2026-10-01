using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using HrmApi.FirbaseServices.Services;
using HrmApi.Areas.HelpPage.ModelDescriptions;
using HrmApi.Areas.HelpPage.Models;
using HrmApi.Areas.HelpPage;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Net;




namespace HrmApi.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class NotificationSenderController : Controller
    {
        private const string ErrorViewName = "Error";

        public NotificationSenderController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public NotificationSenderController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
        //public async Task<ActionResult> SendPushNotification(string fcmtoken, string title, string body, string screen)
        //{
        //    // Input validation
        //    if (string.IsNullOrWhiteSpace(fcmtoken))
        //        return Json(new { success = false, message = "FCM token is required" });
        //    if (string.IsNullOrWhiteSpace(title))
        //        return Json(new { success = false, message = "Title is required" });
        //    if (string.IsNullOrWhiteSpace(screen))
        //        return Json(new { success = false, message = "Screen is required" });

        //    try
        //    {
        //        var notificatinos = new FirbaseService();
        //        var success = await notificatinos.SendBookingNotification(fcmtoken, title, body, screen);

        //        if (success)
        //            return Json(new { success = true, screen = screen, message = "Notification sent successfully" });
        //        else
        //            return Json(new { success = false, message = "Failed to send notification" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = $"Internal server error: {ex.Message}" });
        //    }
        //}



        public async Task<string> SendPushNotification(string fcmtoken, string title, string body, string screen)
        {
            var notificatinos = new FirbaseService();
            await notificatinos.SendBookingNotification(fcmtoken, title, body, screen);
            return screen;
        }

        //public async Task<String> SendPushNotifications([FromBody] NotificationRequest request)
        //{
        //    var service = new FirbaseService(); // Replace with your actual service class
        //    var result = await service.SendPushNotifier(request.FcmToken, request.Title, request.Body, request.BookingModel);

        //    return result.ToString(); // Return result to the client
        //}
    }
}