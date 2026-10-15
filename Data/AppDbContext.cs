using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class AppDbContext: IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>,
    UserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasMany(e => e.UserRoles)
                 .WithOne(e => e.User)
                 .HasForeignKey(ur => ur.UserId)
                 .IsRequired();
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                 .WithOne(e => e.Role)
                 .HasForeignKey(ur => ur.RoleId)
                 .IsRequired();
            });
        }
    }
}
