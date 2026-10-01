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
    public class SheetController : ApiController
    {
        SheetDAL _bal = new SheetDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Sheet/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Sheet



        [HttpPost]
        [Route("api/Sheet/GetSheets")]

        public Task<SheetReturn> GetSheets(SheetModel getModel)
        {
            var retList = _bal.GetSheets(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Sheet/AUDSheets")]

        public Task<SheetReturn> AUDSheets(SheetModel getModel)
        {
            var retList = _bal.AUDSheets(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Sheet/GetSheetById")]

        public Task<SheetReturn> GetSheetById(SheetModel getModel)
        {
            var retList = _bal.GetSheetById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Sheet/StatusChanger")]

        public Task<SheetReturn> StatusChanger(SheetModel getModel)
        {
            var retList = _bal.StatusChanger(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Sheet/HRSheets")]

        public Task<SheetReturn> HRSheets(SheetModel getModel)
        {
            var retList = _bal.HRSheets(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Sheet/EDSheets")]

        public Task<SheetReturn> EDSheets(SheetModel getModel)
        {
            var retList = _bal.EDSheets(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Sheet/HODSheets")]

        public Task<SheetReturn> HODSheets(SheetModel getModel)
        {
            var retList = _bal.HODSheets(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Sheet/GMSheets")]

        public Task<SheetReturn> GMSheets(SheetModel getModel)
        {
            var retList = _bal.GMSheets(getModel);
            return retList;
        }

        #endregion

    }
}