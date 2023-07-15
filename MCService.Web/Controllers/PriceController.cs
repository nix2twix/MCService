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

        [HttpGet("{id:int}", Name = "GetPriceByID")]
        public ActionResult GetServiceByID(int id)
        {
            sqlDriver.Open();
            var priceInfo = sqlDriver.ExecReader(priceService.GetPriceByID(id));
            priceInfo.Read();

            return Ok(new PriceModel 
            {
                Id = id,
                Name = priceInfo["name"].ToString(),
                Price = (int)priceInfo[0],
                isPriceOnRequest = (bool)priceInfo[1],
                LocationID = (int)priceInfo[2],
                ServiceID = (int)priceInfo[3]
            });
        }

        [HttpPost(Name = "AddNewPrice")]
        public ActionResult AddNewPrice(int price, bool isPriceOnRequest, int locationID, int serviceID)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(
                priceService.AddNewPrice(price, isPriceOnRequest, locationID, serviceID))
                + " prices was added successfully!");
        }

        [HttpPut(Name = "UpdatePriiceByID")]
        public ActionResult UpdatePriiceByID(int id, int price, bool isPriceOnRequest, int locationID, int serviceID)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(
                priceService.ChangePriceByID(id, price, isPriceOnRequest, locationID, serviceID))
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
