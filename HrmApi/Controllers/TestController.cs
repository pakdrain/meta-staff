using HrmApi.DAL;
using HrmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNetCore.Http;



namespace HrmApi.Controllers
{
    public class TestController : ApiController
    {
        TestDAL _bal = new TestDAL();



        #region Test



        //[HttpPost]
        //[Route("api/Test/AddTestTask")]
        //public TestReturn AddTestTask(TestModel getModel)
        //{
        //    var retList = _bal.AddTestTask(getModel);
        //    return retList;
        //}

        //[HttpPost]
        //[Route("api/Test/UpdateTestTask")]
        //public TestReturn UpdateTestTask(TestModel getModel)
        //{
        //    var retList = _bal.UpdateTestTask(getModel);
        //    return retList;
        //}

        //[HttpPost]
        //[Route("api/Test/UpdateTestTaskWithDel")]
        //public TestReturn UpdateTestTaskWithDel(TestModel getModel)
        //{
        //    var retList = _bal.UpdateTestTaskWithDel(getModel);
        //    return retList;
        //}

      


        #endregion
    }
}
