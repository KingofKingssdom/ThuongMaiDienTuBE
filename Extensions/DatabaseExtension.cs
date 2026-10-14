using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
