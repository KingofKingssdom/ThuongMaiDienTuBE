
using Microsoft.OpenApi;

namespace Ecommerce.Extensions
{
    public static class SwaggerExtension
    {
       public static IServiceCollection AddSwaggerDocumentation(
           this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
             {
                 options.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Title = "Ecommerce API",
                     Version = "v1",
                     Description = "REST API for Ecommerce"
                 });

                 options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Name = "Authorization",
                     Description = "Enter JWT Bearer token",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                     Scheme = "bearer",
                     BearerFormat = "JWT"
                 });

                 options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecuritySchemeReference("Bearer", document),
                            new List<string>()
                        }
                    });

             });

                return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(
            this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
                options.RoutePrefix = "swagger";
            });

            return app;
        }
    }
    
}
