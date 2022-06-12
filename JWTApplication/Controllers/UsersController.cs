using JWTApplication.Models;
using JWTApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IJWTManagerRepository jWTManagerRepository;

        public UsersController(IJWTManagerRepository jWTManagerRepository)
        {
            this.jWTManagerRepository = jWTManagerRepository;
        }
        [HttpGet]
        [Route("userlist")]
        public List<string> Get()
        {
            var users = new List<string>
            {
                "John Mario",
                "Paul Carlos",
                "Jane West"
            };
            return users;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users userdata)
        {
            var token = jWTManagerRepository.Authonticate(userdata);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok();
        }
    }
}
