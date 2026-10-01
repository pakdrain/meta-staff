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
    public class VehicleController : ApiController
    {
        VehicleDAL _bal = new VehicleDAL();
        // get connection string hadndler
        [HttpGet]
        [Route("api/Vehicle/GetConnectionString")]
        public string GetConnectionString()
        {
            var conStr = _bal.GetConnectionString();
            return conStr;
        }

        #region Vehicle


        [HttpPost]
        [Route("api/Vehicle/GetMyBuyPack")]
        public Task<VehiclesReturn> GetMyBuyPack(VehiclesModel getModel)
        {
            var retList = _bal.GetMyBuyPack(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetAppliedBuyPack")]
        public Task<VehiclesReturn> GetAppliedBuyPack(VehiclesModel getModel)
        {
            var retList = _bal.GetAppliedBuyPack(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetBuyPackById")]
        public Task<VehiclesReturn> GetBuyPackById(VehiclesModel getModel)
        {
            var retList = _bal.GetBuyPackById(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetHodBuyPack")]
        public Task<VehiclesReturn> GetHodBuyPack(VehiclesModel getModel)
        {
            var retList = _bal.GetHodBuyPack(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetGmBuyPack")]
        public Task<VehiclesReturn> GetGmBuyPack(VehiclesModel getModel)
        {
            var retList = _bal.GetGmBuyPack(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetAdminBuyPack")]
        public Task<VehiclesReturn> GetAdminBuyPack(VehiclesModel getModel)
        {
            var retList = _bal.GetAdminBuyPack(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetEdBuyPack")]
        public Task<VehiclesReturn> GetEdBuyPack(VehiclesModel getModel)
        {
            var retList = _bal.GetEdBuyPack(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetHrBuyPack")]
        public Task<VehiclesReturn> GetHrBuyPack(VehiclesModel getModel)
        {
            var retList = _bal.GetHrBuyPack(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Vehicle/AudVehicles")]
        public Task<VehiclesReturn> AudVehicles(VehiclesModel getModel)
        {
            var retList = _bal.AudVehicles(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/BuyPackStatusChanger")]
        public Task<VehiclesReturn> BuyPackStatusChanger(VehiclesModel getModel)
        {
            var retList = _bal.BuyPackStatusChanger(getModel);
            return retList;
        }

        [HttpPost]
        [Route("api/Vehicle/GetOwnedVehicle")]

        public Task<OwnedVehicleReturn> GetOwnedVehicle(OwnedVehicle getModel)
        {
            var retList = _bal.GetOwnedVehicle(getModel);
            return retList;
        }


        [HttpPost]
        [Route("api/Vehicle/GenerateInstallment")]
        public Task<VehiclesDetReturn> GenerateInstallment(VehiclesDet getModel)
        {
            var retList = _bal.GenerateInstallment(getModel);
            return retList;
        }

        #endregion
    }
}
