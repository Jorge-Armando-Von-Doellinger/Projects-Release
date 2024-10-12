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
                .AddHostedServices();
            return services;
        }

        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<StartupSettingsService>();
            services.AddHostedService<MessageListenerService>();
            return services;
        }

    }
}
