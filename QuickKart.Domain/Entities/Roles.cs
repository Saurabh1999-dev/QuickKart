namespace QuickKart.Domain.Entities
{
    public class Roles
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<UserRoles> UserRoles { get; set; }
            = new List<UserRoles>();
    }
}
