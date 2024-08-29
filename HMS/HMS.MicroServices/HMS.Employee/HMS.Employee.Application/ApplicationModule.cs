using HMS.Employee.Application.Manager;
using HMS.Employee.Core.Interface.Manager;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Employee.Inputs;

namespace HMS.Employee.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddManagers();
            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IManager<Nuget.Response.Response, EmployeeInput>, EmployeeManager>();
            return services;
        }
    }
}
