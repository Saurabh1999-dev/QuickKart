using System;
using System.Collections.Generic;
using System.Text;

namespace QuickKart.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> Get();
        Task<User?> GetUserByEmail(string email);
    }
}
