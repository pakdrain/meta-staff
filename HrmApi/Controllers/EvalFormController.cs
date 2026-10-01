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
    public class EvalFormController : ApiController
    {
        EvalFormDAL _bal = new EvalFormDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/EvalForm/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region EvalForm

        [HttpPost]
        [Route("api/EvalForm/GetApprEmp")]

        public Task<EvalFormReturn> GetApprEmp(EvalFormModel getModel)
        {
            var retList = _bal.GetApprEmp(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/EvalForm/GetEvalDetail")]

        public Task<EvalFormReturn> GetEvalDetail(EvalFormModel getModel)
        {
            var retList = _bal.GetEvalDetail(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/EvalForm/UpdateEvalForm")]

        public Task<EvalFormReturn> UpdateEvalForm(EvalFormModel getModel)
        {
            var retList = _bal.UpdateEvalForm(getModel);
            return retList;
        }





        #endregion

    }
}