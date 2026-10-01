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
    public class RankingController : ApiController
    {
        RankingDAL _bal = new RankingDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Ranking/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Ranking



        [HttpPost]
        [Route("api/Ranking/GetRankigDet")]

        public Task<RankingReturn> GetRankigDet(RankingModel getModel)
        {
            var retList = _bal.GetRankigDet(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Ranking/RankigByID")]

        public Task<RankingReturn> RankigByID(RankingModel getModel)
        {
            var retList = _bal.RankigByID(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Ranking/UpdateRankig")]

        public Task<RankingReturn> UpdateRankig(RankingModel getModel)
        {
            var retList = _bal.UpdateRankig(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Ranking/AUDRanking")]

        public Task<RankingReturn> AUDRanking(RankingModel getModel)
        {
            var retList = _bal.AUDRanking(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Ranking/UpdateIncrement")]

        public Task<RankingReturn> UpdateIncrement(RankingModel getModel)
        {
            var retList = _bal.UpdateIncrement(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Ranking/RankigStatusChanger")]

        public Task<RankingReturn> RankigStatusChanger(RankingModel getModel)
        {
            var retList = _bal.RankigStatusChanger(getModel);
            return retList;
        }





        #endregion

    }
}