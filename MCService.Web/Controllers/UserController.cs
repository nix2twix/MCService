using MCService.Models;
using MCService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService userService = null)
        {
            this.userService = userService;
        }

        [HttpGet("{id:int}", Name = "GetUserByID")]
        public ActionResult GetUserByID(int id)
        {
            return Ok(userService.GetUserByID(id));
        }

        [HttpPost(Name = "AddNewUser")]
        public ActionResult AddNewService(UserModel model)
        {
            return Ok(userService.AddNewUser(model));
        }

        [HttpPut(Name = "UpdateUserByID")]
        public ActionResult UpdateServiceByID(int id, UserModel newModel)
        {
            return Ok(userService.ChangeUserByID(id, newModel));
        }

        [HttpDelete("{id:int}", Name = "DeleteUserByID")]
        public ActionResult DeleteServiceByID(int id)
        {
            return Ok(userService.DeleteUserByID(id));
        }
    }
}
