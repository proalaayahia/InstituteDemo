using InstituteDemo.Application.Concrete;
using InstituteDemo.Application.Interfaces.Common;
using Microsoft.Extensions.DependencyInjection;

namespace InstituteDemo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
