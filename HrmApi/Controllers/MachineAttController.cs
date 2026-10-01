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
    public class MachineAttController : ApiController
    {
        MachineAttendanceDAL _bal = new MachineAttendanceDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/MachineAtt/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region MachineAttendance
        // get all role hadndler
        [HttpPost]
        [Route("api/MachineAtt/GetAllMachineAtt")]
        public MachineAttendanceReturn GetAllMachineAtt(MachineAttendance getModel)
        {
            var retList = _bal.GetAllMachineAtt(getModel);
            return retList;
        }

        // aud single role hadndler
        [HttpPost]
        [Route("api/MachineAtt/AUDMachineAtt")]
        public MachineAttendanceReturn AUDMachineAtt(MachineAttendance getModel)
        {
            var retList = _bal.AUDMachineAtt(getModel);
            return retList;
        }

        #endregion
    }
}
