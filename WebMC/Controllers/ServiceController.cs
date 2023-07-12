using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {

        [HttpGet(Name = "GetServiceByID")]
        public ActionResult Get(int id)
        {
            return Ok(new ServiceModel
            {
                Id = 1,
                Name = "Уборка",
                MinCount = 1,
                MaxCount = 1,
                Measurement = "шт",
                MCId = 1
            });
        }
    }
}
