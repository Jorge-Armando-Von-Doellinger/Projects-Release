using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Repository;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

namespace HMS.ContractsMicroService.Infrastructure
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
                return new(IAppSettings.CurrentSettings.MongoDb.ConnectionString);
            });
            return services;
        }

        private static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddSingleton<MongoContext>();
            services.AddScoped<ConsulContext>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscoveryService, DiscoveryServiceConsul>();
            services.AddScoped<ITransaction, TransactionService>();
            return services;
        }
    }
}
