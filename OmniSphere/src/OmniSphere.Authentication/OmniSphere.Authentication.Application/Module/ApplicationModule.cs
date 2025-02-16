using Microsoft.Extensions.DependencyInjection;
using OmniSphere.Authentication.Application.Implementations.Services;
using OmniSphere.Authentication.Application.Implementations.UseCase;
using OmniSphere.Authentication.Application.Interfaces.UseCases;
using OmniSphere.Authentication.Core.Interfaces.Services;

namespace OmniSphere.Authentication.Application.Module;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddUseCases();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAuthUseCase, AuthUseCase>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenJwtService, TokenJwtService>();
        return services;
    }
}