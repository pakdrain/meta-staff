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

        #region Loan


        [HttpPost]
        [Route("api/Loan/GetLoanSummary")]

        public Task<LoanSummaryReturn> GetLoanSummary(LoanSummary getModel)
        {
            var retList = _bal.GetLoanSummary(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/GetMyLoans")]

        public Task<LoansReturn> GetMyLoans(LoansModel getModel)
        {
            var retList = _bal.GetMyLoans(getModel);
            return retList;
        }

      

        [HttpPost]
        [Route("api/Loan/GetHODLoans")]

        public Task<LoansReturn> GetHODLoans(LoansModel getModel)
        {
            var retList = _bal.GetHODLoans(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/GetAppliedLoans")]

        public Task<LoansReturn> GetAppliedLoans(LoansModel getModel)
        {
            var retList = _bal.GetAppliedLoans(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/GetGMLoans")]

        public Task<LoansReturn> GetGMLoans(LoansModel getModel)
        {
            var retList = _bal.GetGMLoans(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/GetHRLoans")]

        public Task<LoansReturn> GetHRLoans(LoansModel getModel)
        {
            var retList = _bal.GetHRLoans(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/GetEDLoans")]

        public Task<LoansReturn> GetEDLoans(LoansModel getModel)
        {
            var retList = _bal.GetEDLoans(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/GetLoanById")]

        public Task<LoansReturn> GetLoanById(LoansModel getModel)
        {
            var retList = _bal.GetLoanById(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Loan/AUDLoan")]

        public Task<LoansReturn> AUDLoan(LoansModel getModel)
        {
            var retList = _bal.AUDLoan(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Loan/LoanStatusChanger")]

        public Task<LoansReturn> LoanStatusChanger(LoansModel getModel)
        {
            var retList = _bal.LoanStatusChanger(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Loan/AUDBonusDeduction")]
        public Task<BonusDeductionReturn> AUDBonusDeduction(BonusDeduction getModel)
        {
            return _bal.AUDBonusDeduction(getModel);
        }

        [HttpPost]
        [Route("api/Loan/GetBonusDed")]
        public Task<BonusDeductionReturn> GetBonusDed(BonusDeduction getModel)
        {
            return _bal.GetBonusDed(getModel);
        }



        #endregion
    }
}
