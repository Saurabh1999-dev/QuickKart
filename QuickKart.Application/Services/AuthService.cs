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
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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

        public async Task<User?> GetUserByEmail(string email)
        {
            var normalLizeEmail = email.Trim().ToLowerInvariant();
            var existingUser = await _userRepository.GetUserByEmail(normalLizeEmail);
            return existingUser;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var user = await _userRepository.GetUserByEmail(email) ?? throw new UserNotFoundException("The email is not exist. Please check the email and try again");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new UserNotFoundException("username or passowrd is not correct");
            }
            return new LoginResponse
            {
                UserId = user.Id,
                Role = user.UserRoles,
                Email = user.Email,
            };
        }
    }
}
