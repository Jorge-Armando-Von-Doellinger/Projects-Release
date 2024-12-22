using Consul;
using HMS.Payments.Core.Interfaces.Internal_Services;
using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Infrastructure.Context;
using HMS.Payments.Infrastructure.Repository;
using HMS.Payments.Infrastructure.Services;
using HMS.Payments.Infrastructure.Settings.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Modules;

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
            var settings = sp.GetRequiredService<IOptionsMonitor<DatabaseSettings>>();
            return new MongoClient(settings.CurrentValue.ConnectionString);
        });
        services.AddSingleton<IConsulClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptionsMonitor<ServiceDiscoverySettigs>>();
            return new ConsulClient(configs => { configs.Address = new Uri(settings.CurrentValue.Address); });
        });
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentEmployeeRepository, PaymentEmployeeRepository>();
        services.AddScoped<IRefundRepository, RefundRepository>();
        return services;
    }
}