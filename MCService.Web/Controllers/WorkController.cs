using Microsoft.AspNetCore.Mvc;
using MCService.Services;
using MCService.Models;

namespace MCService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly WorkService workService;
        public WorkController(WorkService workService = null)
        {
            this.workService = workService;
        }

        [HttpGet("{id:int}", Name = "GetServiceByID")]
        public ActionResult GetServiceByID(int id)
        {
            return Ok(workService.GetServiceByID(id));
        }

        [HttpPost(Name = "AddNewService")]
        public ActionResult AddNewService(ServiceModel model)
        {
            return Ok(workService.AddNewService(model));
        }

        [HttpPut(Name = "UpdateServiceByID")]
        public ActionResult UpdateServiceByID(int id, ServiceModel model)
        {
            return Ok(workService.ChangeServiceByID(id, model));
        }

        [HttpDelete("{id:int}", Name = "DeleteServiceByID")]
        public ActionResult DeleteServiceByID(int id)
        {
            return Ok(workService.DeleteServiceByID(id));
        }
    }
}
