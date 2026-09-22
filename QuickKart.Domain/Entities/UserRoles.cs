using System.Data;

namespace QuickKart.Domain.Entities
{
    public class UserRoles
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        public User User { get; set; } = null!;

        public Roles Role { get; set; } = null!;
    }
}
