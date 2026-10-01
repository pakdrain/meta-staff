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
    public class OrderController : ApiController
    {
        OrderDAL _bal = new OrderDAL();
        // get connection string hadndler
        [HttpGet]
        [Route("api/Orders/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Orders

        [HttpPost]
        [Route("api/Orders/AUDOrder")]
        public Task<OrderMastReturn> AUDOrder(OrderMastModel getModel)
        {
            var retList = _bal.AUDOrder(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Orders/GetPendingOrders")]

        public Task<OrderMastReturn> GetPendingOrders(OrderMastModel getModel )
        {
            var retList = _bal.GetPendingOrders(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Orders/GetOrderById")]

        public Task<OrderMastReturn> GetOrderById(OrderMastModel getModel)
        {
            var retList = _bal.GetOrderById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Orders/GetOrderDetail")]

        public Task<OrderMastReturn> GetOrderDetail(OrderMastModel getModel)
        {
            var retList = _bal.GetOrderDetail(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Orders/StatusChanger")]

        public Task<OrderMastReturn> StatusChanger(OrderMastModel getModel)
        {
            var retList = _bal.StatusChanger(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Orders/GetPostedOrders")]

        public Task<OrderMastReturn> GetPostedOrders(OrderMastModel getModel)
        {
            var retList = _bal.GetPostedOrders(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Orders/GetPostOrderById")]

        public Task<OrderMastReturn> GetPostOrderById(OrderMastModel getModel)
        {
            var retList = _bal.GetPostOrderById(getModel);
            return retList;
        }



        // get orders list
        //[HttpPost]
        //[Route("api/Orders/GetOrderList")]
        //public OrderMastReturn GetOrderList(OrderMastModel getModel)
        //{
        //    var retList = _bal.GetOrderList(getModel);
        //    return retList;
        //}


        //// get orders mast by id
        //[HttpPost]
        //[Route("api/Orders/GetOrderMastById")]
        //public OrderMastReturn GetOrderMastById(OrderMastModel getModel)
        //{
        //    var retList = _bal.GetOrderMastById(getModel);
        //    return retList;
        //}


        //// get order det by id
        //[HttpPost]
        //[Route("api/Orders/GetOrderDetById")]
        //public OrderDetReturn GetOrderDetById(OrderDetModel getModel)
        //{
        //    var retList = _bal.GetOrderDetById(getModel);
        //    return retList;
        //}





        //// get all order detail
        //[HttpPost]
        //[Route("api/Orders/GetAllOrderDetail")]
        //public OrderMastReturn GetAllOrderDetail(OrderMastModel getModel)
        //{
        //    var retList = _bal.GetAllOrderDetail(getModel);
        //    return retList;
        //}

        //// get order detail by id
        //[HttpPost]
        //[Route("api/Orders/GetOrderDetailById")]
        //public OrderMastReturn GetOrderDetailById(OrderMastModel getModel)
        //{
        //    var retList = _bal.GetOrderDetailById(getModel);
        //    return retList;
        //}

        //// update orders
        //[HttpPost]
        //[Route("api/Orders/UpdateOrdersStatus")]
        //public OrderMastReturn UpdateOrdersStatus(OrderMastModel getModel)
        //{
        //    var retList = _bal.UpdateOrdersStatus(getModel);
        //    return retList;
        //}


        //// aud order
        //[HttpPost]
        //[Route("api/Orders/AUDOrders")]
        //public OrderMastReturn AUDOrders(OrderMastModel getModel)
        //{
        //    var retList = _bal.AUDOrders(getModel);
        //    return retList;
        //}

        #endregion
    }
}
