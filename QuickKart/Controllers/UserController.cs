using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickKart.Application.DTOs;
using QuickKart.Application.Exceptions.UserException;
using QuickKart.Application.Interfaces;
using QuickKart.Application.Services;

namespace QuickKart.API.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.Get();
            return Ok(users);
        }

        //[Authorize(Roles = "Admin, User, Worker")]
        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Please provide an email.");
            }
            var user = await _userService.GetUserByEmail(email) ?? throw new UserNotFoundException("User not found");
            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return Ok(userDto);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Please provide an email.");
            }

            return Ok();
        }
    }
}
