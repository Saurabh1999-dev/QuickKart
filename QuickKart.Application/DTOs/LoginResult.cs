namespace QuickKart.Application.DTOs
{
    public class LoginResult
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}
