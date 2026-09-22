namespace QuickKart.Application.DTOs
{
    public class LoginResult
    {
        public Guid UserId { get; init; }

        public string Email { get; init; } = string.Empty;

        public List<string> Role { get; init; } = [];

        public string AccessToken { get; init; } = string.Empty;

        public DateTime ExpiresAt { get; init; }
    }
}
