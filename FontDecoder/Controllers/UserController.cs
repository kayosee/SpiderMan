using Dapper.Contrib.Extensions;
using FontDecoder.Model;
using FontDecoder.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
namespace FontDecoder.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("User")]

    public class UserController : Controller
    {
        private IUserService userService;
        public UserController(IUserService user)
        {
            this.userService = user;
        }

        [HttpGet]
        public IActionResult Get(string username)
        {
            return Ok(userService.GetUser(username));
        }

        // POST: UserController/Create
        [HttpPut]
        public ActionResult Add([FromBody] User user)
        {
            if (TryValidateModel(user))
            {
                userService.AddUser(user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public ActionResult Edit([FromBody] User user)
        {
            if (TryValidateModel(user))
            {
                userService.UpdateUser(user);
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        public ActionResult Delete(string username)
        {
            userService.RemoveUser(username);
            return Ok();
        }
    }
}
