using Microsoft.EntityFrameworkCore;
using QuickKart.Application.Interfaces.Repository;
using QuickKart.Domain.Entities;
using QuickKart.Domain.Entities.Worker;
using QuickKart.Infrastructure.Data;

namespace QuickKart.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Include(x=>x.Role)
        .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<List<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        // worker data
        public async Task<Worker?> GetWorkerByEmail(string email)
        {
            return await _context.Workers
        .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
