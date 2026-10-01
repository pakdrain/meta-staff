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
    public class ApprSetupController : ApiController
    {
        ApprSetupDAL _bal = new ApprSetupDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/ApprSetup/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region ApprSetup



        [HttpPost]
        [Route("api/ApprSetup/GetApprSetupDet")]

        public Task<ApprSetupReturn> GetApprSetupDet(ApprSetupModel getModel)
        {
            var retList = _bal.GetApprSetupDet(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/ApprSetup/UpdateApprSetup")]

        public Task<ApprSetupReturn> UpdateApprSetup(ApprSetupModel getModel)
        {
            var retList = _bal.UpdateApprSetup(getModel);
            return retList;
        }



        #endregion

    }
}