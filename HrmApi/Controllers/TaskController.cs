using HrmApi.DAL;
using HrmApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HrmApi.Controllers
{
    public class TaskController : ApiController
    {
        TaskDAL _bal = new TaskDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Tasks/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Tasks

        [HttpPost]
        [Route("api/Tasks/TaskById")]
        public Task<TasksModelReturn> TaskById(TasksModel getModel)
        {
            var retList = _bal.TaskById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetAddTasks")]
        public Task<TasksModelReturn> GetAddTasks(TasksModel getModel)
        {
            var retList = _bal.GetAddTasks(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetMyTasks")]
        public Task<TasksModelReturn> GetMyTasks(TasksModel getModel)
        {
            var retList = _bal.GetMyTasks(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetHODTasks")]
        public Task<TasksModelReturn> GetHODTasks(TasksModel getModel)
        {
            var retList = _bal.GetHODTasks(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetEDTasks")]
        public Task<TasksModelReturn> GetEDTasks(TasksModel getModel)
        {
            var retList = _bal.GetEDTasks(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetAssignableTasks")]
        public Task<TasksModelReturn> GetAssignableTasks(TasksModel getModel)
        {
            var retList = _bal.GetAssignableTasks(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Tasks/AUDTasks")]
        public Task<TasksModelReturn> AUDTasks(TasksModel getModel)
        {
            var retList = _bal.AUDTasks(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Tasks/TaskStatusChanger")]
        public Task<TasksModelReturn> TaskStatusChanger(TasksModel getModel)
        {
            var retList = _bal.TaskStatusChanger(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Tasks/GetTaskType")]
        public Task<TaskTypesReturn> GetTaskType(TaskTypes getModel)
        {
            var retList = _bal.GetTaskType(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/AUDTaskList")]
        public Task<TaskTypesReturn> AUDTaskList(TaskTypes getModel)
        {
            var retList = _bal.AUDTaskList(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetTaskList")]
        public Task<TaskTypesReturn> GetTaskList(TaskTypes getModel)
        {
            var retList = _bal.GetTaskList(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/AUDTaskPortion")]
        public Task<TaskTypesReturn> AUDTaskPortion(TaskTypes getModel)
        {
            var retList = _bal.AUDTaskPortion(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetTaskPortion")]
        public Task<TaskTypesReturn> GetTaskPortion(TaskTypes getModel)
        {
            var retList = _bal.GetTaskPortion(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetTaskChats")]
        public TaskChatsReturn GetTaskChats(TaskChat getModel)
        {
            var retList = _bal.GetTaskChats(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/AUDTaskChats")]
        public TaskChatsReturn AUDTaskChats(TaskChat getModel)
        {
            var retList = _bal.AUDTaskChats(getModel);

            return retList;

        }

        #endregion
    }

}
