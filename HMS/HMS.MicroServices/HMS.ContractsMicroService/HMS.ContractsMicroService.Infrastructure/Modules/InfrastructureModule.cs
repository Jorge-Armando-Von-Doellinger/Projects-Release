using Consul;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Repository;
using HMS.ContractsMicroService.Infrastructure.Services;
using HMS.ContractsMicroService.Infrastructure.Settings;
using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Modules
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {

            services
                .AddRepositories()
                .AddClient()
                .AddContexts()
                .AddServices();
            return services;
        }



        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IEmployeeContractRepository, EmployeeContractRepository>();
            services.AddScoped<IWorkHoursRepository, WorkHoursRepository>();
            return services;
        }
        private static IServiceCollection AddClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IDatabaseSettings>();
                return new(settings.ConnectionString);
            });
            services.AddSingleton<IConsulClient, ConsulClient>(sp =>
            {
                var settings = sp.GetRequiredService<IServiceDiscoverySettings>(); // Precisará caso mude o address e etc
                return new ConsulClient(/*config =>
                {
                    Console.WriteLine(settings.Address);
                    config.Address = new Uri(settings.Address);
                }*/);
            });
            return services;
        }

        private static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddSingleton<MongoContext>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IServiceDiscovery, ServiceDiscovery>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddScoped<ITransaction, TransactionService>();
            return services;
        }

        private static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<OnUpdatedSettings, DatabaseSettings>();
            services.AddSingleton<OnUpdatedSettings, ServiceDiscoverySettings>();
            return services;
        }
    }
}
