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
    public class CompaniesController : ApiController
    {
        CompanyDAL _bal = new CompanyDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Companies/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region companies
        // get all employee hadndler
        [HttpPost]
        [Route("api/Companies/GetCompaniesFromApi")]
        public CompanyReturn GetCompaniesFromApi(CompaniesModel getModel)
        {
            var retList = _bal.GetCompaniesFromApi(getModel);
            return retList;
        }


        // get all employee hadndler
        [HttpPost]
        [Route("api/Companies/SyncCompanies")]
        public Task<CompanyReturn> SyncCompanies(CompaniesModel getModel)
        {
            var retList = _bal.SyncCompanies(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Companies/GetCompForFilter")]
        public Task<CompanyReturn> GetCompForFilter(CompaniesModel getModel)
        {
            var retList = _bal.GetCompForFilter(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Companies/GetCompForClearance")]
        public Task<CompanyReturn> GetCompForClearance(CompaniesModel getModel)
        {
            var retList = _bal.GetCompForClearance(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Companies/AUDSeekingCompanies")]
        public Task<CompanyReturn> AUDSeekingCompanies(CompaniesModel getModel)
        {
            var retList = _bal.AUDSeekingCompanies(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Companies/GetSeeCompaniesByEmp")]
        public Task<CompanyReturn> GetSeeCompaniesByEmp(CompaniesModel getModel)
        {
            var retList = _bal.GetSeeCompaniesByEmp(getModel);
            return retList;
        }


        #endregion
    }
}
