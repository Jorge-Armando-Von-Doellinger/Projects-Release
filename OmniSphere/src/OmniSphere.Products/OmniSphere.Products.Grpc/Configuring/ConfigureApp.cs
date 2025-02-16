using OmniSphere.Products.Infrastructure.Configs;

namespace OmniSphere.Products.Grpc.Configuring;

public static class ConfigureApp
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureInfrastructure(configuration);
        return services;
    }

    private static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfiguration>(configuration.GetRequiredSection("Database"));
        return services;
    }
}