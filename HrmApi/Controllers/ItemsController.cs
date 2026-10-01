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
    public class ItemsController : ApiController
    {
        ItemsDAL _bal = new ItemsDAL();

        // get connection string hadndler
        [HttpGet]
        [Route("api/Items/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Items
        // get all Complains hadndler
        //[HttpPost]
        //[Route("api/Items/GetAllItemm")]
        //public ItemReturn GetAllItemm(ItemModel getModel)
        //{
        //    var retList = _bal.GetAllItemm(getModel);
        //    return retList;
        //}



        [HttpPost]
        [Route("api/Items/GetAllItems")]

        public Task<ItemReturn> GetAllItems(ItemModel getModel)
        {
            var retList = _bal.GetAllItems(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Items/GetItemById")]

        public Task<ItemReturn> GetItemById(ItemModel getModel)
        {
            var retList = _bal.GetItemById(getModel);
            return retList;
        }


        #endregion
    }
}
