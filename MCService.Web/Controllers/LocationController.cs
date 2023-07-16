using MCService.Services;
using MCService.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        SqlDriver sqlDriver = new SqlDriver();
        LocationService locationService = new LocationService();

        [HttpGet("{id:int}", Name = "GetLocationByID")]
        public ActionResult GetLocationByID(int id)
        {
            sqlDriver.Open();
            var locationInfo = sqlDriver.ExecReader(locationService.GetLocationByID(id));
            locationInfo.Read();

            return Ok(new LocationModel
            { 
                Name = locationInfo[0].ToString(),
                CompanyID = (int)locationInfo[1]
            });
        }

        [HttpPost(Name = "AddNewLocation")]
        public ActionResult AddNewLocation(string name, int idMC)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(
                locationService.AddNewLocation(name, idMC))
                + " locations was added successfully!");
        }

        [HttpPut(Name = "UpdateLocationByID")]
        public ActionResult UpdateLocationByID(int id, string name, int idMC)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(locationService.ChangeLocationByID
                (id, name, idMC))
                + " locations was updated successfully!");
        }

        [HttpDelete("{id:int}", Name = "DeleteLocationByID")]
        public ActionResult DeleteLocationByID(int id)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(locationService.DeleteLocationByID(id))
                + " locations was deleted successfully!");
        }
    }
}
