using MCService.Models;
using MCService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly PriceService priceService;
        public PriceController(PriceService priceService = null, 
            WorkService workService = null)
        {
            this.priceService = priceService;
        }

        [HttpGet("{id:int}", Name = "GetPriceByID")]
        public ActionResult GetPriceByID(int id)
        {
            return Ok(priceService.GetPriceByID(id));
        }

        [HttpGet(Name = "GetPriceByCode")]
        public ActionResult GetPriceByCode(int houseFIASCode, int idService)
        {
            return Ok(priceService.GetPriceByCode(houseFIASCode, idService));
        }

        [HttpPost(Name = "AddNewPrice")]
        public ActionResult AddNewPrice(PriceModel newModel)
        {
            return Ok(priceService.AddNewPrice(newModel));
        }

        [HttpPut(Name = "UpdatePriiceByID")]
        public ActionResult UpdatePriiceByID(int id, PriceModel newModel)
        {
            return Ok(priceService.ChangePriceByID(id, newModel));
        }

        [HttpDelete("{id:int}", Name = "DeletePriceByID")]
        public ActionResult DeletePriceByID(int id)
        {
            return Ok(priceService.DeletePriceByID(id));
        }

    }
}
