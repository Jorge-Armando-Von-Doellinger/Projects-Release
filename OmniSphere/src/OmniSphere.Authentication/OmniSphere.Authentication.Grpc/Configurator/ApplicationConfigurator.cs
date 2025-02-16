using OmniSphere.Authentication.Application.Configs;

namespace OmniSphere.Authentication.Grpc.Configurator;

public static class ApplicationConfigurator
{
    internal static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSecureApiKey(configuration);
        return services;
    }

    private static IServiceCollection ConfigureSecureApiKey(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SecureKeyConfig>(configuration.GetSection("SECRET_KEYS"));
        return services;
    }
}