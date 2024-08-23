using HMS.Core.Interfaces.Repository;
using HMS.Infrastructure.DataContext;
using HMS.Infrastructure.Repository;
using HMS.Infrastructure.TransactionServices;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContexts()
                .AddRepositories()
                .AddTransactionsServices();
            return services;
        }

        public static IServiceCollection AddTransactionsServices(this IServiceCollection services)
        {
            services.AddScoped<ClientTransactionService>();
            return services;
        }

        public static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddScoped<ClientContext>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            return services;
        }
    }
}
