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
    public class LeaveController : ApiController
    {
        LeaveDAL _bal = new LeaveDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Leaves/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Leaves


        [HttpPost]
        [Route("api/Leaves/AUDLeave")]
        public Task<LeaveReturn> AUDLeave(LeaveModel getModel)
        {
            var retList = _bal.AUDLeave(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Leaves/GetMyLeaves")]

        public Task<LeaveReturn> GetMyLeaves(LeaveModel getModel)
        {
            var retList = _bal.GetMyLeaves(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Leaves/GetAppliedLeaves")]

        public Task<LeaveReturn> GetAppliedLeaves(LeaveModel getModel)
        {
            var retList = _bal.GetAppliedLeaves(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Leaves/GetHODLeaves")]

        public Task<LeaveReturn> GetHODLeaves(LeaveModel getModel)
        {
            var retList = _bal.GetHODLeaves(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Leaves/GetHRLeaves")]

        public Task<LeaveReturn> GetHRLeaves(LeaveModel getModel)
        {
            var retList = _bal.GetHRLeaves(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Leaves/GetLeaveById")]

        public Task<LeaveReturn> GetLeaveById(LeaveModel getModel)
        {
            var retList = _bal.GetLeaveById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Leaves/LeaveStatusChanger")]
        public Task<LeaveReturn> LeaveStatusChanger(LeaveModel getModel)
        {
            var retList = _bal.LeaveStatusChanger(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Leaves/GetSadwitchStatus")]
        public Task<LeaveReturn> GetSadwitchStatus(LeaveModel getModel)
        {
            var retList = _bal.GetSadwitchStatus(getModel);
            return retList;
        }

        #endregion
    }
}
