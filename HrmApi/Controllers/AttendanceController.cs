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
    public class AttendanceController : ApiController
    {
        AttendanceDAL _bal = new AttendanceDAL();
        // get connection string hadndler
        [HttpGet]
        [Route("api/Attendance/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }
        #region Attendance


        [HttpPost]
        [Route("api/Attendance/MarkAttendance")]
        public Task<AttendanceReturn> MarkAttendance(AttendanceModel getModel)
        {
            var retList = _bal.MarkAttendance(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Dashboard/GetLiveMonthAtt")]

        public Task<AttendanceReturn> GetLiveMonthAtt(AttendanceModel getModel)
        {
            var retList = _bal.GetLiveMonthAtt(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Dashboard/GetAttReport")]

        public Task<AttendanceReturn> GetAttReport(AttendanceModel getModel)
        {
            var retList = _bal.GetAttReport(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Dashboard/GetHierarchyAtt")]
        public Task<AttendanceReturn> GetHierarchyAtt(AttendanceModel getModel)
        {
            return _bal.GetHierarchyAtt(getModel);
        }

        [HttpPost]
        [Route("api/Dashboard/GetAttDesigWise")]

        public Task<AttendanceReturn> GetAttDesigWise(AttendanceModel getModel)
        {
            var retList = _bal.GetAttDesigWise(getModel);
            return retList;
        }



        #endregion
    }
}
