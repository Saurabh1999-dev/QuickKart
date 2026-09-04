using System;
using System.Collections.Generic;
using System.Text;

namespace QuickKart.Application.DTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool Success { get; set; }
        public string? Message {  get; set; }
    }
}
