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
    public class DepartmentController : ApiController
    {
        DepartmentDAL _bal = new DepartmentDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/departments/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region departments
        // get all employee hadndler
        [HttpPost]
        [Route("api/departments/DepartmentsDetail")]
        public DepartmentReturn DepartmentsDetail(DepartmentModel getModel)
        {
            var retList = _bal.DepartmentsDetail(getModel);
            return retList;
        }



        [HttpPost]
        [Route("api/departments/GetDepartments")]

        public Task<DepartmentReturn> GetDepartments(DepartmentModel getModel)
        {
            var retList = _bal.GetDepartments(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/departments/GetSheetsDep")]

        public Task<DepartmentReturn> GetSheetsDep(DepartmentModel getModel)
        {
            var retList = _bal.GetSheetsDep(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/departments/GetDesignation")]

        public Task<DepartmentReturn> GetDesignation(DepartmentModel getModel)
        {
            var retList = _bal.GetDesignation(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/departments/GetTaskDep")]

        public Task<DepartmentReturn> GetTaskDep(DepartmentModel getModel)
        {
            var retList = _bal.GetTaskDep(getModel);
            return retList;
        }



        [HttpPost]
        [Route("api/departments/GetDivisons")]

        public Task<DepartmentReturn> GetDivisons(DepartmentModel getModel)
        {
            var retList = _bal.GetDivisons(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/departments/AUDSeekingDepartment")]
        public Task<DepartmentReturn> AUDSeekingDepartment(DepartmentModel getModel)
        {
            var retList = _bal.AUDSeekingDepartment(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/departments/GetSeeDepartments")]
        public Task<DepartmentReturn> GetSeeDepartments(DepartmentModel getModel)
        {
            var retList = _bal.GetSeeDepartments(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/departments/GetFilterDepartments")]
        public Task<DepartmentReturn> GetFilterDepartments(DepartmentModel getModel)
        {
            var retList = _bal.GetFilterDepartments(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/departments/GetApprDep")]
        public Task<DepartmentReturn> GetApprDep(DepartmentModel getModel)
        {
            var retList = _bal.GetApprDep(getModel);
            return retList;
        }

        #endregion
    }
}
