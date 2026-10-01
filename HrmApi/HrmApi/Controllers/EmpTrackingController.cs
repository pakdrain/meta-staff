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
    public class EmpTrackingController : ApiController
    {
        EmpTrackingDAL _bal = new EmpTrackingDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Tracking/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Tracking
      


        [HttpPost]
        [Route("api/Tracking/AUDSingleTracking")]
        public EmpTrackingReturn AUDSingleTracking(EmployeeTrackingModel getModel)
        {
            var retList = _bal.AUDSingleTracking(getModel);
            return retList;
        }

        #endregion
    }
}
