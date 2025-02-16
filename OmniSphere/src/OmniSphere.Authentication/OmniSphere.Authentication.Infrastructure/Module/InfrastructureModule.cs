using Microsoft.Extensions.DependencyInjection;
using OmniSphere.Authentication.Core.Interfaces.External_Services;
using OmniSphere.Authentication.Core.Interfaces.TokenCacheService;
using OmniSphere.Authentication.Infrastructure.Implementations.External_Services;
using OmniSphere.Authentication.Infrastructure.Implementations.TokenCacheService;

namespace OmniSphere.Authentication.Infrastructure.Module;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddExternalServices()
            .AddServices();
        return services;
    }

    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginExternalService, ValidateCredentialsExternalService>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenCacheService, RedisTokenCacheService>();
        return services;
    }
}