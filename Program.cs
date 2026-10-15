using Ecommerce.Data;
using Ecommerce.Extensions;
using Microsoft.EntityFrameworkCore;
namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Host.UseSerilog((context, configuration) =>
            //{
            //    configuration.ReadFrom.Configuration(context.Configuration);
            //});
            // Add services to the container.
            //builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();

            builder.Services.AddDatabase(builder.Configuration);

            builder.Services.AddIdentity();

            builder.Services.AddSwaggerDocumentation();
            
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await context.Database.MigrateAsync();

                //await DbSeeder.SeedAdminAsync(scope.ServiceProvider);
            }
            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseAuthorization();

            //app.MapStaticAssets();
            app.MapControllers();
            app.Run();
        }
    }
}
