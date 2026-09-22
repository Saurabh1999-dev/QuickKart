using Microsoft.AspNetCore.Identity;
using QuickKart.Application.DTOs;
using QuickKart.Application.Exceptions.UserException;
using QuickKart.Application.Interfaces;
using QuickKart.Application.Interfaces.Repository;
using QuickKart.Domain.Entities;

namespace QuickKart.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task RegisterAsync(RegisterRequest request)
        {
            if (request == null)
            {
                throw new UserNotFoundException("User Not Found");
            }
            var normalLizeEmail = request.Email.Trim().ToLowerInvariant();
            var existingUser = await _userRepository.GetUserByEmail(normalLizeEmail);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Email + "User already exist.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = normalLizeEmail,
                CreatedAt = DateTime.UtcNow,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            user.UserRoles.Add(new UserRoles
            {
                Id = Guid.NewGuid(),
                RoleId = request.RoleId,
                User = user,
                UserId = user.Id,
            });


            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveAsync();
        }
        
        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var user = await _userRepository.GetUserByEmail(email) ?? throw new UserNotFoundException("The email is not exist. Please check the email and try again");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new UserNotFoundException("username or passowrd is not correct");
            }
            var token = _jwtService.GenerateToken(user.Id, user.Email, request.Role);
            var roles = user.UserRoles.Select(x => x.Role.Name).ToList();
            return new LoginResult
            {
                UserId = user.Id,
                Role = roles,
                Email = user.Email,
                AccessToken = token,
            };
        }
    }
}
