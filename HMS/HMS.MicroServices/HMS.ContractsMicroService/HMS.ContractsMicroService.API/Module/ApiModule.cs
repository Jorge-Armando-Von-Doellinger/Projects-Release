using HMS.ContractsMicroService.API.Services.Background;
using HMS.ContractsMicroService.API.Services.Hosted;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Manager;
using HMS.ContractsMicroService.Application.Messaging.Processor;
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
                .AddHostedServices()
                .AddMessageProcessors();
            return services;
        }

        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<StartupSettingsService>();
            services.AddHostedService<MessageListenerService>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscoveryService, DiscoveryServiceConsul>();
            return services;
        }

        private static IServiceCollection AddManager(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeContractManager, ContractsManager>();
            services.AddScoped<IWorkHoursManager, WorkHoursManager>();
            return services;
        }

        private static IServiceCollection AddMessageProcessors(this IServiceCollection services)
        {
            services.AddTransient(provider => new Lazy<IMessageListener>(() => provider.GetRequiredService<IMessageListener>()));
            services.AddTransient(provider => new Lazy<IMessageProcessor>(() => provider.GetRequiredService<IMessageProcessor>()));
            services.AddSingleton<IMessageProcessor, MessageProcessor>();
            return services;
        }
    }
}
