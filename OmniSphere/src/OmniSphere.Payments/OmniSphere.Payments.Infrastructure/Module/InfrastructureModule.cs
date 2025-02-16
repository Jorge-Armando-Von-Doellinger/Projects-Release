using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OmniSphere.Payments.Core.Entity;
using OmniSphere.Payments.Core.Interfaces.Repository;
using OmniSphere.Payments.Infrastructure.Factory;
using OmniSphere.Payments.Infrastructure.Implementations.Repository;

namespace OmniSphere.Payments.Infrastructure.Module;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddFactory()
            .AddMongoClient()
            .AddDatabase()
            .AddCollections()
            .AddRepository();
        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        return services;
    }

    private static IServiceCollection AddFactory(this IServiceCollection services)
    {
        services.AddSingleton<MongoConnectionFactory>();
        services.AddSingleton<MongoCollectionFactory>();
        return services;
    }

    private static IServiceCollection AddCollections(this IServiceCollection services)
    {
        services.AddSingleton<IMongoCollection<Payment>>(sp =>
        {
            var factory = sp.GetRequiredService<MongoCollectionFactory>();
            return factory.GetPaymentCollection();
        });
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var factory = sp.GetRequiredService<MongoCollectionFactory>();
            return factory.GetDatabase();
        });
        return services;
    }

    private static IServiceCollection AddMongoClient(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var factory = sp.GetRequiredService<MongoConnectionFactory>();
            return factory.GetMongoClient();
        });
        return services;
    }
}