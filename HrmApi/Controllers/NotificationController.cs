using HrmApi.DAL;
using HrmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HrmApi.Controllers
{
    public class NotificationController : ApiController
    {

        NotificationDAL _bal = new NotificationDAL();
        // get connection string hadndler
        [HttpGet]
        [Route("api/Notifications/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Notifications
        ///////////// GET WHOLE NOTIFICATION ///////////////

        [HttpPost]
        [Route("api/Notifications/GetWholeNotif")]

        public Task<NotifReturn> GetWholeNotif(NotifModel getModel)
        {
            var retList = _bal.GetWholeNotif(getModel);
            return retList;
        }
        ///////////// MARK READ WHOLE NOTIFICATION ///////////////

        [HttpPost]
        [Route("api/Notifications/GetMarkReadWholeNotif")]

        public Task<NotifReturn> GetMarkReadWholeNotif(NotifModel getModel)
        {
            var retList = _bal.GetMarkReadWholeNotif(getModel);
            return retList;
        }
        ///////////// GET MY NOTIFICATION ///////////////

        [HttpPost]
        [Route("api/Notifications/GetMyNotif")]

        public Task<NotifReturn> GetMyNotif(NotifModel getModel)
        {
            var retList = _bal.GetMyNotif(getModel);
            return retList;
        }








        // get all leaves hadndler
        [HttpPost]
        [Route("api/Notifications/GetAllNotifications")]
        public NotificationReturn GetAllNotifications(NotificationModel getModel)
        {
            var retList = _bal.GetAllNotifications(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Notifications/AUDNotifications")]
        public NotificationReturn AUDNotifications(NotificationModel getModel)
        {
            var retList = _bal.AUDNotifications(getModel);
            return retList;
        }

        #endregion
    }
}
