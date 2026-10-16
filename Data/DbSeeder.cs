using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();
            var roleExisting = await context.Roles.AnyAsync(r => r.NormalizedName == "ADMIN");
            if (roleExisting)
            {
                return;
            }
            else if (!roleExisting)
            {
                var role = new Role {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                context.Add(role);
                var user = new User
                {
                    FullName = "Administrator",
                    Email = "admin@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    IsActive = true
                };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                await context.UserRoles.AddAsync(new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id
                });

                await context.SaveChangesAsync();
            }

            var roleUserExisting = await context.Roles.AnyAsync(r => r.NormalizedName == "USER");
            if (roleUserExisting)
            {
                return;
            }
            else if (!roleUserExisting)
            {
                var role = new Role
                {
                    Name = "User",
                    NormalizedName = "USER"
                };
                context.Add(role);
            }
            await context.SaveChangesAsync();

        }
    }
}
