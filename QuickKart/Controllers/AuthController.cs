using Microsoft.AspNetCore.Mvc;
using QuickKart.Application.DTOs;
using QuickKart.Application.Exceptions.UserException;
using QuickKart.Application.Interfaces;
namespace QuickKart.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegisterRequest request)
        {
            await _authService.RegisterAsync(request);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Please provide an email.");
            }
            var user = await _authService.GetUserByEmail(email) ?? throw new UserNotFoundException("User not found");
            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.UserRoles.Select(x=>x.RoleId)
            };
            return Ok(userDto);
        }
    }
}
