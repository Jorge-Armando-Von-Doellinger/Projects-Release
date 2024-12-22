using OmniSphere.Payments.Infrastructure.Configs;

namespace OmniSphere.Payments.gRpc.Configurator;

public static class InfrastructureConfigurator
{
    public static IServiceCollection AddInfrastructureConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfigs>(configuration.GetSection("Database"));
        return services;
    }
}