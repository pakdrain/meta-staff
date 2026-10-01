using HrmApi.DAL;
using HrmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
