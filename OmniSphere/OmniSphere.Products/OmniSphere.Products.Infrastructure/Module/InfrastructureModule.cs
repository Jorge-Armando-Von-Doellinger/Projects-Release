using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OmniSphere.Products.Core.Entity;
using OmniSphere.Products.Core.Interfaces.Repository;
using OmniSphere.Products.Infrastructure.Configurator;
using OmniSphere.Products.Infrastructure.Factory;
using OmniSphere.Products.Infrastructure.Implementations.Repository;
using OmniSphere.Products.Infrastructure.Services;

namespace OmniSphere.Products.Infrastructure.Module;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddFactory()
            .AddClient()
            .AddCollections()
            .AddConfigurators()
            .AddServices()
            .AddRepository();
        return services;
    }

    private static IServiceCollection AddClient(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var factory = sp.GetRequiredService<MongoConnectionFactory>();
            return factory.GetMongoClient();
        });
        return services;
    }

    private static IServiceCollection AddCollections(this IServiceCollection services)
    {
        services.AddSingleton<IMongoCollection<Product>>(sp =>
        {
            var factory = sp.GetRequiredService<MongoCollectionFactory>();
            return factory.GetProductCollection();
        });
        return services;
    }
    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<TransactionService>();
        return services;
    }
    
    private static IServiceCollection AddFactory(this IServiceCollection services)
    {
        services.AddSingleton<MongoConnectionFactory>();
        services.AddSingleton<MongoCollectionFactory>();
        return services;
    }

    private static IServiceCollection AddConfigurators(this IServiceCollection services)
    {
        services.AddSingleton<ProductCollectionConfigurator>();
        return services;
    }
}