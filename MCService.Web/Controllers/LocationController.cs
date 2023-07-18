using MCService.Services;
using MCService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationService locationService;
        public LocationController(LocationService locationService = null)
        {
            this.locationService = locationService;
        }

        [HttpGet("{id:int}", Name = "GetLocationByID")]
        public ActionResult GetLocationByID(int id)
        {
            return Ok(locationService.GetLocationByID(id));
        }

        [HttpPost(Name = "AddNewLocation")]
        public ActionResult AddNewLocation(LocationModel model)
        {
            return Ok(locationService.AddNewLocation(model));
        }

        [HttpPut(Name = "UpdateLocationByID")]
        public ActionResult UpdateLocationByID(int id, LocationModel newModel)
        {

            return Ok(locationService.ChangeLocationByID(id, newModel));
        }

        [HttpDelete("{id:int}", Name = "DeleteLocationByID")]
        public ActionResult DeleteLocationByID(int id)
        {
            return Ok(locationService.DeleteLocationByID(id));
        }
    }
}
