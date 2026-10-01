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
        [Route("api/Attendance/MarkLocalAttendance")]
        public AttendanceReturn MarkLocalAttendance(AttendanceModel getModel)
        {
            var retList = _bal.MarkLocalAttendance(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Attendance/MarkAttendance")]
        public Task<AttendanceReturn> MarkAttendance(AttendanceModel getModel)
        {
            var retList = _bal.MarkAttendance(getModel);
            return retList; 
        }


        [HttpPost]
        [Route("api/Attendance/GetAttendance")]
        public AttendanceReturn GetAttendance(AttendanceModel getModel)
        {
            var retList = _bal.GetAttendance(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Attendance/GetDashboardAttendance")]
        public AttendanceReturn GetDashboardAttendance(AttendanceModel getModel)
        {
            var retList = _bal.GetDashboardAttendance(getModel);
            return retList;
        }



        [HttpPost]
        [Route("api/Dashboard/GetLiveMonthAtt")]

        public Task<AttendanceReturn> GetLiveMonthAtt(AttendanceModel getModel)
        {
            var retList = _bal.GetLiveMonthAtt(getModel);
            return retList;
        }



        #endregion
    }
}
