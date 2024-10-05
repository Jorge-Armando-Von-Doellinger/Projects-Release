using HMS.ContractsMicroService.API.Services.Hosted;
using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Application.Manager;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Infrastructure.Services;
using HMS.ContractsMicroService.Messaging.Publisher;

namespace HMS.ContractsMicroService.API.Module
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services
                .AddServices()
                .AddManager()
                .AddHostedServices();
            return services;
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddScoped<IMessagePublisher, MessagePubisher>();
            services.AddHostedService<StartupSettingsService>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscoveryService, DiscoveryServiceConsul>();
            return services;
        }

        public static IServiceCollection AddManager(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeContractManager, ContractsManager>();
            services.AddScoped<IWorkHoursManager, WorkHoursManager>();
            return services;
        }
    }
}
