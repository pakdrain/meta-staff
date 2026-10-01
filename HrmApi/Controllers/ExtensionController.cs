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
    public class ExtensionController : ApiController
    {
        ExtensionDAL _bal = new ExtensionDAL();

        [HttpGet]
        [Route("api/Extensions/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Extensions
        // get all role hadndler
        [HttpPost]
        [Route("api/Extensions/GetAllExtension")]
        public ExtensionReturn GetAllExtension(ExtensionModel getModel)
        {
            var retList = _bal.GetAllExtension(getModel);
            return retList;
        }

        // get single role hadndler
        [HttpPost]
        [Route("api/Extensions/GetSingleExtension")]
        public ExtensionReturn GetSingleExtension(ExtensionModel getModel)
        {
            var retList = _bal.GetSingleExtension(getModel);
            return retList;
        }

       


        // aud single role hadndler
        [HttpPost]
        [Route("api/Extensions/AUDSingleExtension")]
        public ExtensionReturn AUDSingleExtension(ExtensionModel getModel)
        {
            var retList = _bal.AUDSingleExtension(getModel);
            return retList;
        }

        #endregion
    }
}
