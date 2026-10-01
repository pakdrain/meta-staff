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
    public class WpPolicyController : ApiController
    {
        WpPolicyDAL _bal = new WpPolicyDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/WpPolicy/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region WpPolicy

        [HttpPost]
        [Route("api/WpPolicy/GetMyWpPolicy")]

        public Task<WpPolicyReturn> GetMyWpPolicy(WpPolicyModel getModel)
        {
            var retList = _bal.GetMyWpPolicy(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/WpPolicy/GetHrWpPolicy")]

        public Task<WpPolicyReturn> GetHrWpPolicy(WpPolicyModel getModel)
        {
            var retList = _bal.GetHrWpPolicy(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/WpPolicy/GetWpPolicyById")]

        public Task<WpPolicyReturn> GetWpPolicyById(WpPolicyModel getModel)
        {
            var retList = _bal.GetWpPolicyById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/WpPolicy/AUDWpPolicy")]

        public Task<WpPolicyReturn> AUDWpPolicy(WpPolicyModel getModel)
        {
            var retList = _bal.AUDWpPolicy(getModel);
            return retList;
        }


        #endregion

    }
}