using HMS.Employee.Application.Manager;
using HMS.Employee.Core.Data.Discounts;
using HMS.Employee.Core.Enum;
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
            services
                .AddManagers();
            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IManager<Nuget.Response.Response, EmployeeInput>, EmployeeManager>();
            services.AddScoped<IManagerWithEmployeeId<Nuget.Response.Response, PayrollInput<BenefitsEnum, Discount>>, PayrollManager>();
            services.AddScoped<IManagerWithEmployeeId<Nuget.Response.Response, ContractualInformationInput<BenefitsEnum>>, ContractualManager>();
            return services;
        }
    }
}
