namespace QuickKart.Application.Interfaces.Repository
{
    public interface IJwtService
    {
        string GenerateToken(Guid id, string email, string role);
    }
}
