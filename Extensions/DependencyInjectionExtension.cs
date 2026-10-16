using Ecommerce.Repositories.Implementations;
using Ecommerce.Repositories.Interface;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Implementations;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
        {
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            //Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
