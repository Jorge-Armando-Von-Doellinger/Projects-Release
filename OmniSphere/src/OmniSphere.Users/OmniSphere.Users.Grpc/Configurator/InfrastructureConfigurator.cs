using OmniSphere.Users.Infrastructure.Configs;

namespace OmniSphere.Users.Grpc.Configurator;

public static class InfrastructureConfigurator
{
    internal static IServiceCollection ConfigureInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfigs>(configuration.GetSection("Database"));
        return services;
    }
}