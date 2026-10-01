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
    public class ClearanceController : ApiController
    {
        ClearanceDAL _bal = new ClearanceDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Clearance/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }
        #region Clearance

        [HttpPost]
        [Route("api/Clearance/AUDClearance")]
        public Task<ClearanceReturn> AUDClearance(ClearanceModel getModel)
        {
            var retList = _bal.AUDClearance(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Clearance/ClearanceStatusChanger")]
        public Task<ClearanceReturn> ClearanceStatusChanger(ClearanceModel getModel)
        {
            var retList = _bal.ClearanceStatusChanger(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Clearance/GetClearance")]
        public Task<ClearanceReturn> GetClearance(ClearanceModel getModel)
        {
            var retList = _bal.GetClearance(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Clearance/GetHodClearance")]
        public Task<ClearanceReturn> GetHodClearance(ClearanceModel getModel)
        {
            var retList = _bal.GetHodClearance(getModel);
            return retList;
        }

       

        [HttpPost]
        [Route("api/Clearance/GetClearanceById")]
        public Task<ClearanceReturn> GetClearanceById(ClearanceModel getModel)
        {
            var retList = _bal.GetClearanceById(getModel);
            return retList;
        }


        ////////////////////////dep clearance/////////////////////////////////

        [HttpPost]
        [Route("api/Clearance/GetDepClearance")]
        public Task<ClearanceReturn> GetDepClearance(ClearanceModel getModel)
        {
            var retList = _bal.GetDepClearance(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Clearance/GetDepClearanceByClearId")]
        public Task<ClearanceReturn> GetDepClearanceByClearId(ClearanceModel getModel)
        {
            var retList = _bal.GetDepClearanceByClearId(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Clearance/GetDepClearanceById")]
        public Task<ClearanceReturn> GetDepClearanceById(ClearanceModel getModel)
        {
            var retList = _bal.GetDepClearanceById(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Clearance/DepClearanceStatusChanger")]
        public Task<ClearanceReturn> DepClearanceStatusChanger(ClearanceModel getModel)
        {
            var retList = _bal.DepClearanceStatusChanger(getModel);
            return retList;
        }

        #endregion
    }
}
