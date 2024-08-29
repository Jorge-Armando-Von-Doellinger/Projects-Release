using HMS.Employee.Application.Manager;
using HMS.Employee.Core.Interface.Manager;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Nuget.Employee.Inputs;

namespace HMS.Employee.API
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
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
