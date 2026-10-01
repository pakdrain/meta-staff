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
        public Task<ComplainReturn>  GetAllComplains(ComplainModel getModel)
        {
            var retList = _bal.GetAllComplains(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Complains/GetComplainById")]
        public Task<ComplainReturn> GetComplainById(ComplainModel getModel)
        {
            var retList = _bal.GetComplainById(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Complains/GetHodComplains")]
        public Task<ComplainReturn> GetHodComplains(ComplainModel getModel)
        {
            var retList = _bal.GetHodComplains(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Complains/GetHrComplains")]
        public Task<ComplainReturn> GetHrComplains(ComplainModel getModel)
        {
            var retList = _bal.GetHrComplains(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Complains/AUDComplains")]
        public Task<ComplainReturn> AUDComplains(ComplainModel getModel)
        {
            var retList = _bal.AUDComplains(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Complains/StatusChanger")]
        public Task<ComplainReturn> StatusChanger(ComplainModel getModel)
        {
            var retList = _bal.StatusChanger(getModel);
            return retList;
        }



        #endregion
    }
}
