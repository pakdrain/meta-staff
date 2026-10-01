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
        // get all tasks hadndler
        [HttpPost]
        [Route("api/Tasks/GetAllTasks")]
        public TaskReturn GetAllTasks(TaskModel getModel)
        {
            var retList = _bal.GetAllTasks(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetTaskById")]
        public TaskReturn GetTaskById(TaskModel getModel)
        {
            var retList = _bal.GetTaskById(getModel);
            return retList;
        }


        // get GetGivenTasks
        [HttpPost]
        [Route("api/Tasks/GetGivenTasks")]
        public TaskReturn GetGivenTasks(TaskModel getModel)
        {
            var retList = _bal.GetGivenTasks(getModel);
            return retList;
        }

        // get GetPOCTasks
        [HttpPost]
        [Route("api/Tasks/GetPOCTasks")]
        public TaskReturn GetPOCTasks(TaskModel getModel)
        {
            var retList = _bal.GetPOCTasks(getModel);
            return retList;
        }

        // get GetRecievedTasks
        [HttpPost]
        [Route("api/Tasks/GetRecievedTasks")]
        public TaskReturn GetRecievedTasks(TaskModel getModel)
        {
            var retList = _bal.GetRecievedTasks(getModel);
            return retList;
        }


        // get task count hadndler
        [HttpPost]
        [Route("api/Tasks/GetTasksCount")]
        public TaskReturn GetTasksCount(TaskModel getModel)
        {
            var retList = _bal.GetTasksCount(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Tasks/AUDTasks")]
        public TaskReturn AUDTasks([FromBody] List<TaskModel> getModel)
        {
            var retList = _bal.AUDTasks(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/AUDSingleTask")]
        public TaskReturn AUDSingleTask(TaskModel getModel)
        {
            var retList = _bal.AUDSingleTask(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/TaskStatusUpdater")]
        public TaskReturn TaskStatusUpdater(TaskModel getModel)
        {
            var retList = _bal.TaskStatusUpdater(getModel);
            return retList;
        }


      


        // get GetTaskChats
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


        [HttpPost]
        [Route("api/Tasks/GetAllTaskTypes")]
        public TaskTypeAndListsReturn GetAllTaskTypes(TaskTypeAndLists getModel)
        {
            var retList = _bal.GetAllTaskTypes(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/GetAllTaskList")]
        public TaskTypeAndListsReturn GetAllTaskList(TaskTypeAndLists getModel)
        {
            var retList = _bal.GetAllTaskList(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/AUDSingleTaskType")]
        public TaskTypeAndListsReturn AUDSingleTaskType(TaskTypeAndLists getModel)
        {
            var retList = _bal.AUDSingleTaskType(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Tasks/AUDSingleTaskSubType")]
        public TaskTypeAndListsReturn AUDSingleTaskSubType(TaskTypeAndLists getModel)
        {
            var retList = _bal.AUDSingleTaskSubType(getModel);
            return retList;
        }

        #endregion
    }

}
