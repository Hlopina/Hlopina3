using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealtyRentalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        List<User> Users = new List<User>();

        private readonly List<User> _users;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        // Регистрация пользователя
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] User newUser)
        {
            if (newUser.UserId < 0)
            {
                return BadRequest("Invalid user ID");
            }

            Users.Add(newUser);
            return Ok(newUser);
        }

        // Авторизация пользователя 
        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] User loginUser)
        {
            var user = Users.FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }


        [HttpGet("user/getID")]
        public IActionResult GetUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPut("user/updateuser/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] User user)
        {
            if (userId < 0 || user.UserId < 0)
            {
                return BadRequest("Invalid user ID");
            }

            var existingUser = Users.FirstOrDefault(u => u.UserId == userId);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            return Ok(existingUser);
        }
    }
}
