using HrmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HrmApi.DAL
{
    public class ResignController : ApiController
    {
        ResignDAL _bal = new ResignDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Resigns/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Resigns
        // get all Complains hadndler
        [HttpPost]
        [Route("api/Resigns/GetAllResigns")]
        public ResignReturn GetAllResigns(ResignModel getModel)
        {
            var retList = _bal.GetAllResigns(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Resigns/AUDResigns")]
        public ResignReturn AUDResigns([FromBody] List<ResignModel> getModel)
        {
            var retList = _bal.AUDResigns(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Resigns/AUDSingleResigns")]
        public ResignReturn AUDSingleResigns(ResignModel getModel)
        {
            var retList = _bal.AUDSingleResigns(getModel);
            return retList;
        }

        #endregion
    }
}
