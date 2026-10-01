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
    public class VehicleController : ApiController
    {
        VehicleDAL _bal = new VehicleDAL();
        // get connection string hadndler
        [HttpGet]
        [Route("api/Vehicle/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Vehicle
        // get all leaves hadndler
        [HttpPost]
        [Route("api/Vehicle/GetAllVehicle")]
        public VehicleReturn GetAllVehicle(VehicleModel getModel)
        {
            var retList = _bal.GetAllVehicle(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/AUDVehicle")]
        public VehicleReturn AUDVehicle(VehicleModel getModel)
        {
            var retList = _bal.AUDVehicle(getModel);
            return retList;
        }

        #endregion
    }
}
