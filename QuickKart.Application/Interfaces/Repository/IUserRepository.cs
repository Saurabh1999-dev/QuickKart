using QuickKart.Domain.Entities;
using QuickKart.Domain.Entities.Worker;
namespace QuickKart.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task AddUserAsync(User user);
        Task SaveAsync();
        Task<List<User>> Get();
        //Worker
        Task<Worker?> GetWorkerByEmail(string email);
    }
}
