namespace Ecommerce.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddConfigCORS(
        this IServiceCollection services)
        {
            // Repository
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                });
            });


            return services;
        }
    }
}
