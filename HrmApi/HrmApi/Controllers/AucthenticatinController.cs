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
    public class AucthenticatinController : ApiController
    {
        AuthenticationDAL _bal = new AuthenticationDAL();

        #region Client

        // GET  EMPLOYEE BY ID
        [HttpPost]
        [Route("api/Client/ClientVerification")]
        public ClientReturn ClientVerification(ClientModel getModel)
        {
            var retList = _bal.ClientVerification(getModel);
            return retList;
        }

        #endregion

        #region Authentication


        // get connection string hadndler
        [HttpGet]
        [Route("api/Authentication/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        // get  employee by id
        [HttpPost]
        [Route("api/Authentication/MobileAuthentication")]
        public EmployeeReturn MobileAuthentication(EmployeeModel getModel)
        {
            var retList = _bal.MobileAuthentication(getModel);
            return retList;
        }

        // get  change employee password
        [HttpPost]
        [Route("api/Authentication/ChangePaswword")]
        public EmployeeReturn ChangePaswword(EmployeeModel getModel)
        {
            var retList = _bal.ChangePaswword(getModel);
            return retList;
        }

        // check auto logout
        [HttpPost]
        [Route("api/Authentication/AutoLogut")]
        public EmployeeReturn AutoLogut(EmployeeModel getModel)
        {
            var retList = _bal.AutoLogut(getModel);
            return retList;
        }

        #endregion
    }
}
