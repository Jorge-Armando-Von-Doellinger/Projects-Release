using Microsoft.Extensions.DependencyInjection;
using OmniSphere.Users.Application.Implementations.Hasher;
using OmniSphere.Users.Application.Implementations.UseCases;
using OmniSphere.Users.Application.Interfaces.UseCases;
using OmniSphere.Users.Application.Mapper;
using OmniSphere.Users.Core.Interfaces.Hasher;

namespace OmniSphere.Users.Application.Module;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddMapper()
            .AddHasher()
            .AddUseCases();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IUserUseCase, UserUseCase>();
        return services;
    }

    private static IServiceCollection AddHasher(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddScoped<UserMapper>();
        return services;
    }
}