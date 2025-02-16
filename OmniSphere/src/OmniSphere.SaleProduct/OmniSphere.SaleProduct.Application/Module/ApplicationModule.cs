using Microsoft.Extensions.DependencyInjection;
using OmniSphere.SaleProduct.Application.Interfaces.UseCase;
using OmniSphere.SaleProduct.Application.UseCase;

namespace OmniSphere.SaleProduct.Application.Module;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddUseCases();
        return services;    
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ISaleProductUseCase, SaleProductUseCase>();
        return services;
    }
}