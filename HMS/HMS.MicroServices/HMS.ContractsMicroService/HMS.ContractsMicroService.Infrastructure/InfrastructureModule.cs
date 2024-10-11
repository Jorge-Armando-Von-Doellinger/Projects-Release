using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using Nuget.Settings;

namespace HMS.ContractsMicroService.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddClient()    
                .AddContexts()
                .AddServices();
            return services;
        }

        private static IServiceCollection AddClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                return new(AppSettings.CurrentSettings.MongoDb.ConnectionString);
            });
            return services;
        }

        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddSingleton<MongoContext>();
            services.AddScoped<ConsulContext>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITransaction<IMongoClient>, TransactionService>();
            return services;
        }
    }
}
