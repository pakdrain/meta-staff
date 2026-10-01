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
    public class ComplainController : ApiController
    {
        ComplainDAL _bal = new ComplainDAL();
        
        // get connection string hadndler
        [HttpGet]
        [Route("api/Complains/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Complains
        // get all Complains hadndler
        [HttpPost]
        [Route("api/Complains/GetAllComplains")]
        public ComplainReturn GetAllComplains(ComplainModel getModel)
        {
            var retList = _bal.GetAllComplains(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Complains/AUDComplains")]
        public ComplainReturn AUDComplains([FromBody] List<ComplainModel> getModel)
        {
            var retList = _bal.AUDComplains(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Complains/AUDSingleComplain")]
        public ComplainReturn AUDSingleComplain(ComplainModel getModel)
        {
            var retList = _bal.AUDSingleComplain(getModel);
            return retList;
        }

        #endregion
    }
}
