using QuickKart.Domain.Entities;
namespace QuickKart.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task AddUserAsync(User user);
        Task SaveAsync();
    }
}
