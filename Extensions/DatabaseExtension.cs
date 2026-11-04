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
                    //sqlOptions =>
                    //{
                    //    sqlOptions.EnableRetryOnFailure(
                    //        maxRetryCount: 5,
                    //        maxRetryDelay: TimeSpan.FromSeconds(30),
                    //        errorNumbersToAdd: null);
                    //});
            });
            return services;
        }
    }
}

