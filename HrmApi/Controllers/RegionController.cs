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
    public class RegionController : ApiController
    {
        RegionDAL _bal = new RegionDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Region/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Region

        [HttpPost]
        [Route("api/Region/GetRegions")]
        public Task<RegionReturn> GetRegions(RegionModel getModel)
        {
            var retList = _bal.GetRegions(getModel);
            return retList;
        }

        #endregion
    }
}
