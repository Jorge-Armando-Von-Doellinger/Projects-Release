using HMS.Infrastructure.DataContext;
using HMS.Infrastructure.TransactionServices;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddContexts().
                AddTransactionsServices();
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
    }
}
