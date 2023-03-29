using InstituteDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace InstituteDemo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DataAccess")));
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("InstituteDemo"));
            return services;
        }
        public static IWebHost MigrateDatabase<T>(this IWebHost host) where T :class
        {
            var builder = host;
            using (var scope = builder.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<T>>();
                    logger.LogError(ex, "an error occured while migrating or seeding database.");
                }
            }
            return host;
        }
    }
}
