using MCService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MCService.Services;

namespace MCService.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
 
        private readonly AdressService adressService;
        public AdressController(AdressService adressService = null)
        {
            this.adressService = adressService;
        }

        [HttpGet("{id:int}")]
        public ActionResult GetAdressByID(int id) => Ok(adressService.GetAdressByID(id));


        [HttpPost]
        public ActionResult AddNewFullAdress(int idMC, AdressModel model)
        {
            if (adressService.TryAddNewAdress(idMC, model))
                return Ok(adressService.AddNewAdress(model));
            else 
                return BadRequest();
        }

        [HttpPut("{id:int}", Name = "UpdateAdressByID")]
        public ActionResult UpdateLocationByID(int id, AdressModel newModel)
        {

            return Ok(adressService.ChangeAdressByID(id, newModel));
        }

        [HttpDelete("{id:int}", Name = "DeleteAdressByID")]
        public ActionResult DeleteLocationByID(int id)
        {
            return Ok(adressService.DeleteAdressByID(id));
        }

    }
}
