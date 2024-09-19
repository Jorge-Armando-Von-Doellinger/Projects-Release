using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Application.Manager;

namespace HMS.ContractsMicroService.API.Module
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services.AddManager();
            return services;
        }

        public static IServiceCollection AddManager(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeContractManager, ContractsManager>();
            return services;
        }
    }
}
