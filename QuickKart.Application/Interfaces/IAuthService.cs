using QuickKart.Application.DTOs;
namespace QuickKart.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task RegisterWorkerAsync(WorkerRegisterRequest request);
        Task<LoginResult> LoginAsync(LoginRequest request);
    }
}
