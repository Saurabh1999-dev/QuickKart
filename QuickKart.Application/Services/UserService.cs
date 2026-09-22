using QuickKart.Application.Interfaces;
using QuickKart.Application.Interfaces.Repository;

namespace QuickKart.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Get()
        {
            var user = await _userRepository.Get();
            return user;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            var normalLizeEmail = email.Trim().ToLowerInvariant();
            var existingUser = await _userRepository.GetUserByEmail(normalLizeEmail);
            return existingUser;
        }
    }
}
