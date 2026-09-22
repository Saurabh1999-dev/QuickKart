using QuickKart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickKart.Application.DTOs
{
    public class LoginResponse
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
    }
}
