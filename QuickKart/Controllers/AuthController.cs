using Microsoft.AspNetCore.Mvc;
using QuickKart.Application.DTOs;
using QuickKart.Application.Interfaces;
namespace QuickKart.Api.Controllers
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
            Response.Cookies.Append(
            "access_token",
            response.AccessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });
            return Ok(new LoginResponse
            {
                UserId = response.UserId,
                Email = response.Email,
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegisterRequest request)
        {
            await _authService.RegisterAsync(request);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
