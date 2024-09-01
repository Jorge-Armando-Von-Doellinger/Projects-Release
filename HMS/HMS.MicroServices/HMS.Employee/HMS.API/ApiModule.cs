using HMS.Employee.Application.Manager;
using HMS.Employee.Core.Interface.Manager;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Nuget.Employee.Inputs;
using Nuget.Response;

namespace HMS.Employee.API
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            return services;
        }
    }
}
