using Microsoft.Extensions.DependencyInjection;
using OmniSphere.SaleProduct.Core.Interfaces.External_Services;
using OmniSphere.SaleProduct.Infrastructure.Implementations.External_Services;

namespace OmniSphere.SaleProduct.Infrastructure.Module;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddExternalServices();
        return services;
    }

    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IProductExternalService, ProductExternalService>();
        services.AddScoped<IPaymentExternalService, PaymentExternalService>();
        return services;
    }
}