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
    public class DashboardController : ApiController
    {
        DashboardDAL _bal = new DashboardDAL();
        // get connection string hadndler
        [HttpGet]
        [Route("api/Dashboard/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Dashboard
        // get all leaves hadndler
        [HttpPost]
        [Route("api/Dashboard/GetDashboardAtt")]
        public DashAttReturn GetDashboardAtt(DashboardAttendance getModel)
        {
            var retList = _bal.GetDashboardAtt(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Dashboard/GetDashboardHours")]
        public DashHourReturn GetDashboardHours(DashWorkHoursModel getModel)
        {
            var retList = _bal.GetDashboardHours(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Dashboard/GetDashLeaveBal")]
        public DashLeaveBalReturn GetDashLeaveBal(DashLeaveBalModel getModel)
        {
            var retList = _bal.GetDashLeaveBal(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Dashboard/GetLiveLastSevDayAtt")]

        public Task<AttendanceReturn> GetLiveLastSevDayAtt(AttendanceModel getModel)
        {
            var retList = _bal.GetLiveLastSevDayAtt(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Dashboard/GetLiveAttCount")]

        public Task<AttendanceReturn> GetLiveAttCount(AttendanceModel getModel)
        {
            var retList = _bal.GetLiveAttCount(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Dashboard/GetLiveLeaveBal")]

        public Task<LeaveReturn> GetLiveLeaveBal(LeaveModel getModel)
        {
            var retList = _bal.GetLiveLeaveBal(getModel);
            return retList;
        }


       

        #endregion
    }
}
