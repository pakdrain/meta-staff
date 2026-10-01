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
    public class OfficeController : ApiController
    {
        OfficeDAL _bal = new OfficeDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Office/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Office
        [HttpPost]
        [Route("api/Office/AUDOffices")]
        public Task<OfficeReturn> AUDOffices(OfficeModel getModel)
        {
            var retList = _bal.AUDOffices(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Office/GetOffices")]
        public Task<OfficeReturn> GetOffices(OfficeModel getModel)
        {
            var retList = _bal.GetOffices(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Office/GetOfficeById")]
        public Task<OfficeReturn> GetOfficeById(OfficeModel getModel)
        {
            var retList = _bal.GetOfficeById(getModel);
            return retList;
        }

        #endregion
    }
}
