using HrmApi.Models;
using HrmApi.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using static HrmApi.DAL.SQLHelper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Antlr.Runtime.Misc;
using HrmApi.Controllers;
namespace HrmApi.DAL
{
    public class TestDAL
    {
        private string _ConnString; // variable for connection string
        public TestDAL() // constructor call for connection
        {
            _ConnString = ConfigurationSettings.AppSettings["MISDBConnection"];
        }

        #region Test



  

     



        #endregion


    }
}
