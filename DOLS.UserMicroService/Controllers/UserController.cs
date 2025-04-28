using Microsoft.AspNetCore.Mvc;
using DOLS.UserMicroService.DTO;
using DOLS.UserService.Service;
using DOLS.UserMicroService.Models;

namespace DOLS.UserMicroService.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersService _userService;

        public UserController(UsersService usersService)
        {
            _userService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                User user = _userService.Register(request.Username, request.Email, request.Password);

                return Ok(new { user.Id, user.Username, user.Email });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration.", details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                User user = _userService.Login(request.Username, request.Password);

                return Ok(new { user.Id, user.Username, user.Email });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login.", details = ex.Message });
            }
        }
    

        [HttpPost("init-db")]
        public async Task<IActionResult> InitializeDatabase()
        {
            try
            {
                await _userService.InitializeDatabase();

                return Ok(new { message = "Database initialized successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during database initialization.", details = ex.Message });
            }
        }

    }
}