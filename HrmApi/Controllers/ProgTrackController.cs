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
    public class ProgTrackController : ApiController
    {
        ProgTrackDAL _bal = new ProgTrackDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Project/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region ProgTrack

        [HttpPost]
        [Route("api/ProgTrack/GetProjects")]
        public Task<ProgTrackReturn> GetProjects(ProgTrack getModel)
        {
            var retList = _bal.GetProjects(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/ProgTrack/GetTrackReport")]
        public Task<ProgTrackReturn> GetTrackReport(ProgTrack getModel)
        {
            var retList = _bal.GetTrackReport(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/ProgTrack/GetTrackStatus")]
        public Task<ProgTrackReturn> GetTrackStatus(ProgTrack getModel)
        {
            var retList = _bal.GetTrackStatus(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/ProgTrack/AUDProgTrack")]
        public Task<ProgTrackReturn> AUDProgTrack(ProgTrack getModel)
        {
            var retList = _bal.AUDProgTrack(getModel);
            return retList;
        }


        #endregion

    }
}