using Consul;
using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Core.Interfaces.Services;
using HMS.Payments.Infrastructure.Connect;
using HMS.Payments.Infrastructure.Context;
using HMS.Payments.Infrastructure.Repository;
using HMS.Payments.Infrastructure.Services;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infratructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Modules
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContext()
                .AddClients()
                .AddRepositories()
                .AddServices();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceDiscovery, ConsulServiceDiscovery>();
            services.AddScoped<TransactionService>();
            return services;
        }

        private static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddScoped<MongoContext>();
            services.AddScoped<ConsulContext>();
            return services;
        }

        private static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var context = sp.GetRequiredService<IOptionsMonitor<DatabaseSettings>>();
                return new MongoClient(context.CurrentValue.ConnectionString);
            });
            services.AddSingleton<IConsulClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptionsMonitor<ServiceDiscoverySettigs>>();
                return new ConsulClient(configs =>
                {
                    configs.Address = new(settings.CurrentValue.Address);
                });
            });
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentEmployeeRepository, PaymentEmployeeRepository>();
            return services;
        }
    }
}
