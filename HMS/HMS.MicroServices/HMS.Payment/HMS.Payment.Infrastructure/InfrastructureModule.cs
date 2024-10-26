using HMS.Payments.Infrastructure.Connect;
using HMS.Payments.Infrastructure.Settings.Interfaces;
using HMS.Payments.Infratructure.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContext()
                .AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<TransactionService>();
            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddTransient<MongoContext>();
            return services;
        }
    }
}
