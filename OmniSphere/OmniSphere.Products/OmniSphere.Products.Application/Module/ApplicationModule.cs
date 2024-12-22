using Microsoft.Extensions.DependencyInjection;
using OmniSphere.Products.Application.Interfaces.UseCases;
using OmniSphere.Products.Application.Mapper;
using OmniSphere.Products.Application.UseCases;

namespace OmniSphere.Products.Application.Module;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddUseCases()
            .AddMappers();
        return services;
    }

    internal static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IProductUseCase, ProductUseCase>();
        return services;
    }
    
    internal static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<ProductMapper>();
        return services;
    }
}