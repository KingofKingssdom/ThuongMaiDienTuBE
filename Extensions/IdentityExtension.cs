using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentity(
            this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>()
               .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
    }
}
