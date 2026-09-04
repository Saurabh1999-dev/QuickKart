using Azure.Core;
using Microsoft.AspNetCore.Identity;
using QuickKart.Application.DTOs;
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
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (request == null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Please fill all fields and try again"
                };
            }
            var normalLizeEmail = request.Email.Trim().ToLowerInvariant();
            var existingUser = await _userRepository.GetUserByEmail(normalLizeEmail);
            if (existingUser != null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "A user with this email already exists."
                };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.Trim().ToLowerInvariant(),
                CreatedAt = DateTime.UtcNow,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveAsync();
            return new RegisterResponse
            {
                Success = true,
                Message = "User saved sussfully"
            };
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
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }
            return new LoginResponse
            {
                UserId = user.Id,
                Role = user.Role,
                Email = user.Email,
                Success = true,
                Message = "Login successful."
            };
        }
    }
}
