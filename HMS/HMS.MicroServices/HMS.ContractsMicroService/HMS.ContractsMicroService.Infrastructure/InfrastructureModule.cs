using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContexts()
                .AddServices();
            return services;
        }

        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddScoped<MongoContext>();
            services.AddScoped<WorkHoursContext>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITransaction<IMongoClient>, TransactionService>();
            return services;
        }
    }
}
