using Dapper.Contrib.Extensions;
using FontDecoder.Model;
using FontDecoder.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
namespace FontDecoder.Controllers
{
    [Authorize]
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {   
        private IUserService userService;
        public UserController(IUserService user) 
        {
            this.userService = user;
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Add")]
        public ActionResult Add(User user)
        {
            if (TryValidateModel(user))
            {
                userService.AddUser(user);
                return Ok();
            }
            return BadRequest();
        }

    }
}
