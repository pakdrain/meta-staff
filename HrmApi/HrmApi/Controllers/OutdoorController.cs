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
    public class OutdoorController : ApiController
    {

        OutDoorDAL _bal = new OutDoorDAL();
        // get connection string hadndler
        [HttpGet]
        [Route("api/Outdoor/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region OutDoor

        [HttpPost]
        [Route("api/Outdoor/AUDOutDoor")]

        public Task<OutDoorReturn> AUDOutDoor(OutDoorModel getModel)
        {
            var retList = _bal.AUDOutDoor(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Outdoor/GetMyOD")]

        public Task<OutDoorReturn> GetMyOD(OutDoorModel getModel)
        {
            var retList = _bal.GetMyOD(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Outdoor/GetODById")]

        public Task<OutDoorReturn> GetODById(OutDoorModel getModel)
        {
            var retList = _bal.GetODById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Outdoor/GetHodOD")]

        public Task<OutDoorReturn> GetHodOD(OutDoorModel getModel)
        {
            var retList = _bal.GetHodOD(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Outdoor/GetHrOD")]

        public Task<OutDoorReturn> GetHrOD(OutDoorModel getModel)
        {
            var retList = _bal.GetHrOD(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Outdoor/ODStatusChanger")]

        public Task<OutDoorReturn> ODStatusChanger(OutDoorModel getModel)
        {
            var retList = _bal.ODStatusChanger(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Outdoor/GetDutyDuration")]

        public Task<OutDoorReturn> GetDutyDuration(OutDoorModel getModel)
        {
            var retList = _bal.GetDutyDuration(getModel);
            return retList;
        }

        #endregion
    }
}
