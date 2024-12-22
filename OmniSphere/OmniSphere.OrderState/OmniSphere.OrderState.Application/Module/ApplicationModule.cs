using Microsoft.Extensions.DependencyInjection;
using OmniSphere.OrderState.Application.Interfaces.UseCases;
using OmniSphere.OrderState.Application.Mapper;
using OmniSphere.OrderState.Application.UseCases;

namespace OmniSphere.OrderState.Application.Module;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddMappers()
            .AddUseCases();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IOrderStateUseCase, OrderStateUseCase>();
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<OrderStateMapper>();
        return services;
    }
}