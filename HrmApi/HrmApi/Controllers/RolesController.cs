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
    public class RolesController : ApiController
    {
        RoleDAL _bal = new RoleDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Roles/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Roles
        // get all role hadndler
        [HttpPost]
        [Route("api/Roles/GetAllRoles")]
        public RoleReturn GetAllRoles(RoleModel getModel)
        {
            var retList = _bal.GetAllRoles(getModel);
            return retList;
        }

        // get single role hadndler
        [HttpPost]
        [Route("api/Roles/GetSingleRole")]
        public RoleReturn GetSingleRole(RoleModel getModel)
        {
            var retList = _bal.GetSingleRole(getModel);
            return retList;
        }

        // get single role hadndler
        [HttpPost]
        [Route("api/Roles/GetRoleByEmp")]
        public RoleReturn GetRoleByEmp(RoleModel getModel)
        {
            var retList = _bal.GetRoleByEmp(getModel);
            return retList;
        }


        // aud single role hadndler
        [HttpPost]
        [Route("api/Roles/AUDSingleRole")]
        public RoleReturn AUDSingleRole(RoleModel getModel)
        {
            var retList = _bal.AUDSingleRole(getModel);
            return retList;
        }

        #endregion
    }
}
