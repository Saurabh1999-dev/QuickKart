using Microsoft.EntityFrameworkCore;
using QuickKart.Domain.Entities;
using QuickKart.Domain.Entities.Worker;
using System.Data;

namespace QuickKart.Infrastructure.Data.Configurations
{
    public class UserConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                modelBuilder.Entity<User>()
    .HasOne(u => u.Role)
    .WithMany(r => r.Users)
    .HasForeignKey(u => u.RoleId)
    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(x => x.Name)
                    .IsUnique();
            });
        }
    }
}
