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
    public class HcController : ApiController
    {
        HcDAL _bal = new HcDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Hc/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region HcDAL

        [HttpPost]
        [Route("api/Hc/GetMyHc")]

        public Task<HcReturn> GetMyHc(HcModel getModel)
        {
            var retList = _bal.GetMyHc(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Hc/GetHcById")]

        public Task<HcReturn> GetHcById(HcModel getModel)
        {
            var retList = _bal.GetHcById(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Hc/GetHrHc")]

        public Task<HcReturn> GetHrHc(HcModel getModel)
        {
            var retList = _bal.GetHrHc(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Hc/AudHc")]

        public Task<HcReturn> AudHc(HcModel getModel)
        {
            var retList = _bal.AudHc(getModel);
            return retList;
        }

        #endregion

    }
}