using MCService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMC
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {

        [HttpGet(Name = "GetServiceByID")]
        public ActionResult Get(int id)
        {
            return Ok();
        }

    }
}
