using Microsoft.AspNetCore.Mvc;

namespace QuickKart.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Unauthorized();
        }

        [HttpPost("signup")]
        public IActionResult SignUp()
        {
            return Unauthorized();
        }
    }
}
