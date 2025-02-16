using OmniSphere.OrderState.Infrastructure.Configs;

namespace OmniSphere.OrderState.Grpc.Configurator;

public static class InfrastructureConfigurator
{
    public static void ConfigureInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<DatabaseConfigs>(configuration.GetSection("DatabaseConfigs:Postgresql"));
    }
}