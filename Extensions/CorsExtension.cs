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
                    policy.WithOrigins("https://thuong-mai-dien-tu-fe.vercel.app")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                });
            });


            return services;
        }
    }
}
