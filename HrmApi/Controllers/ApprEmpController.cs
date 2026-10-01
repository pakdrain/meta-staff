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
    public class ApprEmpController : ApiController
    {
        ApprEmpDAL _bal = new ApprEmpDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/ApprEmp/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region ApprEmp

        [HttpPost]
        [Route("api/ApprEmp/GetApprEmp")]
        public Task<ApprEmpReturn> GetApprEmp(ApprEmpModel getModel)
        {
            return _bal.GetApprEmp(getModel);
        }

        [HttpPost]
        [Route("api/ApprEmp/UpdateApprEmp")]
        public Task<ApprEmpReturn> UpdateApprEmp(ApprEmpRequest getModel)
        {
            return _bal.UpdateApprEmp(getModel);
        }

        [HttpPost]
        [Route("api/ApprEmp/StatusChangerApprEmp")]
        public Task<ApprEmpReturn> StatusChangerApprEmp(ApprEmpModel getModel)
        {
            return _bal.StatusChangerApprEmp(getModel);
        }

        [HttpPost]
        [Route("api/ApprEmp/GetSummaryOne")]
        public Task<ApprSummariesReturn> GetSummaryOne(ApprSummariesModel getModel)
        {
            return _bal.GetSummaryOne(getModel);
        }

        [HttpPost]
        [Route("api/ApprEmp/GetSummaryTwo")]
        public Task<ApprSummariesReturn> GetSummaryTwo(ApprSummariesModel getModel)
        {
            return _bal.GetSummaryTwo(getModel);
        }

        [HttpPost]
        [Route("api/ApprEmp/GetSummaryThree")]
        public Task<ApprSummariesReturn> GetSummaryThree(ApprSummariesModel getModel)
        {
            return _bal.GetSummaryThree(getModel);
        }

        [HttpPost]
        [Route("api/ApprEmp/TNAReport")]
        public Task<ApprEmpReturn> TNAReport(ApprEmpModel getModel)
        {
            return _bal.TNAReport(getModel);
        }

        #endregion

    }
}