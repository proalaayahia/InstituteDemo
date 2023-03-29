using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using InstituteDemo.Infrastructure;

namespace InstituteDemo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder=CreateHostBuilder(args).Build();
            builder.MigrateDatabase<Program>();
            builder.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
