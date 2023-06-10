using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Interfaces;
using MovieProject.Models;
using MovieProject.Repositories;

namespace MovieProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateUser([FromBody] User userLogin)
        {
           _userRepository.CreateUser(userLogin);
            return Ok("User Added");
        }
    }
}
