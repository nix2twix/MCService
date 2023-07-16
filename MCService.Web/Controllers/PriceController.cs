using MCService.Services;
using MCService.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        SqlDriver sqlDriver = new SqlDriver();
        PriceService priceService = new PriceService();
        WorkService workService = new WorkService();

        [HttpGet("{id:int}", Name = "GetPriceByID")]
        public ActionResult GetPriceByID(int id)
        {
            sqlDriver.Open();
            var priceInfo = sqlDriver.ExecReader(priceService.GetPriceByID(id));
            priceInfo.Read();

            var serviceInfo = sqlDriver.ExecReader(workService.GetServiceByID(id));
            serviceInfo.Read();

            return Ok(new PriceModel
            {
                Id = id,
                Name = serviceInfo["name"].ToString(),
                Price = (int)priceInfo[0],
                isPriceOnRequest = (bool)priceInfo[1],
                LocationID = (int)priceInfo[2],
                ServiceID = (int)priceInfo[3]
            });
        }

        [HttpGet("{id:int}/{name}", Name = "GetPriceByCode")]
        public ActionResult GetPriceByCode(int houseFIASCode, int idService)
        {
            sqlDriver.Open();
            var priceInfo = sqlDriver.ExecReader(priceService.GetPriceByCode(houseFIASCode, idService));
            priceInfo.Read();

            var serviceInfo = sqlDriver.ExecReader(workService.GetServiceByID(idService));
            serviceInfo.Read();

            return Ok(new PriceModel
            {
                Id = houseFIASCode,
                Name = serviceInfo["name"].ToString(),
                Price = (int) priceInfo[0],
                isPriceOnRequest = (bool)priceInfo[1],
                LocationID = (int)priceInfo[2],
                ServiceID = idService
            });
        }

[HttpPost(Name = "AddNewPrice")]
        public ActionResult AddNewPrice(int price, bool isPriceOnRequest, int locationID, int serviceID, string name)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(
                priceService.AddNewPrice(price, isPriceOnRequest, locationID, serviceID, name))
                + " prices was added successfully!");
        }

        [HttpPut(Name = "UpdatePriiceByID")]
        public ActionResult UpdatePriiceByID(int id, int price, bool isPriceOnRequest, int locationID, int serviceID, string name)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(
                priceService.ChangePriceByID(id, price, isPriceOnRequest, locationID, serviceID, name))
                + " services was updated successfully!");
        }

        [HttpDelete("{id:int}", Name = "DeletePriceByID")]
        public ActionResult DeletePriceByID(int id)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(priceService.DeletePriceByID(id))
                + " prices was deleted successfully!");
        }

    }
}
