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
    public class SalSlipController : ApiController
    {
        SalSlipDAL _bal = new SalSlipDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/SalSlip/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region SalSlipDAL

        [HttpPost]
        [Route("api/SalSlip/GetMySalSlip")]

        public Task<SalSlipReturn> GetMySalSlip(SalSlipModel getModel)
        {
            var retList = _bal.GetMySalSlip(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/SalSlip/GetSalSlipById")]

        public Task<SalSlipReturn> GetSalSlipById(SalSlipModel getModel)
        {
            var retList = _bal.GetSalSlipById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/SalSlip/GetHodSalSlip")]

        public Task<SalSlipReturn> GetHodSalSlip(SalSlipModel getModel)
        {
            var retList = _bal.GetHodSalSlip(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/SalSlip/GetHrSalSlip")]

        public Task<SalSlipReturn> GetHrSalSlip(SalSlipModel getModel)
        {
            var retList = _bal.GetHrSalSlip(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/SalSlip/AudSalSlip")]

        public Task<SalSlipReturn> AudSalSlip(SalSlipModel getModel)
        {
            var retList = _bal.AudSalSlip(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/SalSlip/SalSlipStatusChanger")]

        public Task<SalSlipReturn> SalSlipStatusChanger(SalSlipModel getModel)
        {
            var retList = _bal.SalSlipStatusChanger(getModel);
            return retList;
        }



        #endregion

    }
}