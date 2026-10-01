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
    public class LoanController : ApiController
    {
        LoanDAL _bal = new LoanDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Loan/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Loans
        // get all Complains hadndler
        [HttpPost]
        [Route("api/Loan/GetAllLoans")]
        public LoanReturn GetAllLoans(LoanModel getModel)
        {
            var retList = _bal.GetAllLoans(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/GetLoanDetById")]
        public LoanDetReturn GetLoanDetById(LoanDetailModel getModel)
        {
            var retList = _bal.GetLoanDetById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/LoanDetStatusUpdater")]
        public LoanDetReturn LoanDetStatusUpdater(LoanDetailModel getModel)
        {
            var retList = _bal.LoanDetStatusUpdater(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/UpdateLoanStatus")]
        public LoanDetReturn UpdateLoanStatus(LoanDetailModel getModel)
        {
            var retList = _bal.UpdateLoanStatus(getModel);
            return retList;
        }
        [HttpPost]
        [Route("api/Loan/AUDLoans")]
        public LoanReturn AUDLoans([FromBody] List<LoanModel> getModel)
        {
            var retList = _bal.AUDLoans(getModel);
            return retList;
        }


        // get all loan type hadndler
        [HttpPost]
        [Route("api/Loan/GetAllLoanTypes")]
        public LoanTypeReturn GetAllLoanTypes(LoanTypeModel getModel)
        {
            var retList = _bal.GetAllLoanTypes(getModel);
            return retList;
        }
        #endregion
    }
}
