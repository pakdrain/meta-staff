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
    public class ProjectsController : ApiController
    {
        ProjectsDAL _bal = new ProjectsDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Project/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Projects

        [HttpPost]
        [Route("api/Projects/GetDepProject")]
        public Task<ProjectReturn> GetDepProject(ProjectModel getModel)
        {
            var retList = _bal.GetDepProject(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Projects/GetByIdProject")]
        public Task<ProjectReturn> GetByIdProject(ProjectModel getModel)
        {
            var retList = _bal.GetByIdProject(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Projects/AUDProjects")]
        public Task<ProjectReturn> AUDProjects(ProjectModel getModel)
        {
            var retList = _bal.AUDProjects(getModel);
            return retList;
        }


        #endregion

    }
}