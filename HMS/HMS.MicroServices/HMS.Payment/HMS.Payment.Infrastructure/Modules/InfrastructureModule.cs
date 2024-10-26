using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Infrastructure.Connect;
using HMS.Payments.Infrastructure.Repository;
using HMS.Payments.Infratructure.Services;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<TransactionService>();
            return services;
        }

        private static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddSingleton<MongoContext>();
            return services;
        }

        private static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var context = sp.GetRequiredService<MongoContext>();
                return context.GetClient();
            });
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IEmployeePaymentRepository, PaymentEmployeeRepository>();
            return services;
        }
    }
}
