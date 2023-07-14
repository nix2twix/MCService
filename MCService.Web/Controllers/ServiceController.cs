using Microsoft.AspNetCore.Mvc;
using MCService.Services;
using MCService.Web.Models;

namespace MCService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        SqlDriver sqlDriver = new SqlDriver();
        WorkService workService = new WorkService();

        [HttpGet(Name = "GetServiceByID")]
        public ActionResult GetServiceByID(int id)
        {
            sqlDriver.Open();
            var serviceInfo = sqlDriver.ExecReader(workService.GetServiceByID(id));
            serviceInfo.Read();
            
            return Ok(new ServiceModel
            {
                Id = id,
                Name = serviceInfo[0].ToString(),
                MinCount = (int)serviceInfo[1],
                MaxCount = (int)serviceInfo[2],
                Measurement = serviceInfo[3].ToString(),
                CompanyID = (int)serviceInfo[4]
            });
        }
    }
}
