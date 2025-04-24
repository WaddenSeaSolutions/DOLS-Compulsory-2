using Microsoft.AspNetCore.Mvc;
using DOLS.UserMicroService.DTO;
using DOLS.UserMicroService.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DOLS.UserMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _userService.RegisterUserAsync(request);

            if (result)
                return Ok(new { message = "User registered successfully!" });

            return BadRequest(new { message = "User registration failed." });
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _userService.LoginUserAsync(request);

            if (result != null)
            {
                return Ok(new
                {
                    message = "Login successful!",
                    token = result // or whatever your login method returns (JWT, user info, etc.)
                });
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }

    }
}