using QuickKart.Application.DTOs;
using QuickKart.Domain.Entities;
namespace QuickKart.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<User?> GetUserByEmail(string email);
    }
}
