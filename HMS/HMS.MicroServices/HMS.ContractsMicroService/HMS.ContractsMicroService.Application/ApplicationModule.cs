using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Manager;
using HMS.ContractsMicroService.Application.Messaging.Processor;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.ContractsMicroService.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddManagers()
                .AddMessageProcessors();
            return services;
        }

        private static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IContractManager, ContractManager>();
            services.AddScoped<IEmployeeContractManager, EmployeeContractsManager>();
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
