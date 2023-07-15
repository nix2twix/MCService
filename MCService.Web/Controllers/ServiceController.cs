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

        [HttpGet("{id:int}", Name = "GetServiceByID")]
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

        [HttpGet("{name}", Name = "GetServiceByName")]
        public ActionResult GetServiceByName(string name)
        {
            sqlDriver.Open();
            var serviceInfo = sqlDriver.ExecReader(workService.GetServiceByName(name));
            var serviceList = new List<ServiceModel>();

            while(serviceInfo.Read())
            {
                serviceList.Add(new ServiceModel
                {
                    Id = (int)serviceInfo[0],
                    Name = serviceInfo[1].ToString(),
                    MinCount = (int)serviceInfo[2],
                    MaxCount = (int)serviceInfo[3],
                    Measurement = serviceInfo[4].ToString(),
                    CompanyID = (int)serviceInfo[5]
                });
            }

            return Ok(serviceList);
        }

        [HttpPost(Name = "AddNewService")]
        public ActionResult AddNewService(string name, int minCount, int maxCount, string measurement, int idMC)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(
                workService.AddNewService(name, minCount, maxCount, measurement, idMC))
                + " services was added successfully!");
        }

        [HttpPut(Name = "UpdateServiceByID")]
        public ActionResult UpdateServiceByID(int id, string name, int minCount, int maxCount, string measurement, int idMC)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(workService.ChangeServiceByID
                (id, name, minCount, maxCount, measurement, idMC))
                + " services was updated successfully!");
        }

        [HttpDelete("{id:int}", Name = "DeleteServiceByID")]
        public ActionResult DeleteServiceByID(int id)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(workService.DeleteServiceByID(id)) 
                + " service was deleted successfully!");
        }
    }
}
