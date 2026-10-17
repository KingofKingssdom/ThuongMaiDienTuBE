using Ecommerce.Repositories.Implementations;
using Ecommerce.Repositories.Interface;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Implementations;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils;

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
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ImageStorageUtil>();
            return services;
        }
    }
}
