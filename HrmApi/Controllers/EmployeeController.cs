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
    public class EmployeeController : ApiController
    {
        EmployeeDAL _bal = new EmployeeDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Employee/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Employee
        // get all employee hadndler
        [HttpPost]
        [Route("api/Employee/GetAllEmployee")]
        public EmployeeReturn GetAllEmployee(EmployeeModel getModel)
        {
            var retList = _bal.GetAllEmployee(getModel);
            return retList;
        }

        // get HOD employee hadndler
        [HttpPost]
        [Route("api/Employee/GetHODEmp")]
        public EmployeeReturn GetHODEmp(EmployeeModel getModel)
        {
            var retList = _bal.GetHODEmp(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Employee/GetGMEmp")]
        public EmployeeReturn GetGMEmp(EmployeeModel getModel)
        {
            return _bal.GetGMEmp(getModel);
        }

        [HttpPost]
        [Route("api/Employee/GetDirectorEmp")]
        public EmployeeReturn GetDirectorEmp(EmployeeModel getModel)
        {
            return _bal.GetDirectorEmp(getModel);
        }

        // get  employee by id
        [HttpPost]
        [Route("api/Employee/GetEmployeeById")]
        public EmployeeReturn GetEmployeeById(EmployeeModel getModel)
        {
            var retList = _bal.GetEmployeeById(getModel);
            return retList;
        }

        // insert employee
        [HttpPost]
        [Route("api/Employee/AUDEmployees")]
        public EmployeeReturn AUDEmployees([FromBody] List<EmployeeModel> getModel)
        {
            var retList = _bal.AUDEmployees(getModel);
            return retList;
        }
        // bulk employee
        [HttpPost]
        [Route("api/Employee/EmpBulkEditing")]
        public EmployeeReturn EmpBulkEditing([FromBody] List<EmployeeModel> getModel)
        {
            var retList = _bal.EmpBulkEditing(getModel);
            return retList;
        }


        // insert employee
        [HttpPost]
        [Route("api/Employee/AUDSingleEmployees")]
        public  Task<EmployeeReturn> AUDSingleEmployees(EmployeeModel getModel)

        {
            var retList = _bal.AUDSingleEmployees(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Employee/DeleteEmp")]
        public Task<EmployeeReturn> DeleteEmp(EmployeeModel getModel)
        {
            return _bal.DeleteEmp(getModel);
        }

        [HttpPost]
        [Route("api/Employee/EmpSignUp")]
        public Task<EmployeeReturn> EmpSignUp(EmployeeModel getModel)
        {
            return _bal.EmpSignUp(getModel);
        }

        [HttpPost]
        [Route("api/Employee/AUDApprEmp")]
        public Task<EmployeeReturn> AUDApprEmp(EmployeeModel getModel)
        {
            return _bal.AUDApprEmp(getModel);
        }

        [HttpPost]
        [Route("api/Employee/SyncEmployees")]
        public Task<EmployeeReturn> SyncEmployees(EmployeeModel getModel)
        {
            var retList = _bal.SyncEmployees(getModel);
            return retList;
        }


        // get all assignable employees
        [HttpPost]
        [Route("api/Employee/GetAssignableEmployee")]
        public Task<EmployeeReturn> GetAssignableEmployee(EmployeeModel getModel)
        {
            var retList = _bal.GetAssignableEmployee(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Employee/GetFilterEmp")]
        public Task<EmployeeReturn> GetFilterEmp(EmployeeModel getModel)
        {
            var retList = _bal.GetFilterEmp(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Employee/GetClearanceEmp")]
        public Task<EmployeeReturn> GetClearanceEmp(EmployeeModel getModel)
        {
            var retList = _bal.GetClearanceEmp(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Employee/GetMasterAccess")]
        public EmployeeReturn GetMasterAccess(EmployeeModel getModel)
        {
            var retList = _bal.GetMasterAccess(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Employee/GetHrAccess")]
        public EmployeeReturn GetHrAccess(EmployeeModel getModel)
        {
            var retList = _bal.GetHrAccess(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Employee/GetComActiveEmp")]
        public EmployeeReturn GetComActiveEmp(EmployeeModel getModel)
        {
            var retList = _bal.GetComActiveEmp(getModel);
            return retList;
        }

        // deactive employee
        [HttpPost]
        [Route("api/Employee/DeactiveEmployee")]
        public EmployeeReturn DeactiveEmployee(EmployeeModel getModel)
        {
            var retList = _bal.DeactiveEmployee(getModel);
            return retList;
        }

        #endregion
    }
}
