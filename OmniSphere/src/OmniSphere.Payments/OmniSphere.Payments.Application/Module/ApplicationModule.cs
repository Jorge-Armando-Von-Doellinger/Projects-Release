using Microsoft.Extensions.DependencyInjection;
using OmniSphere.Payments.Application.Interfaces.UseCases;
using OmniSphere.Payments.Application.Mappers;
using OmniSphere.Payments.Application.Services;
using OmniSphere.Payments.Application.UseCases;
using OmniSphere.Payments.Core.Interfaces.Services;

namespace OmniSphere.Payments.Application.Module;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddMappers()
            .AddServices()
            .AddUseCases();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IPaymentUseCase, PaymentUseCase>();
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<PaymentMapper>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();
        return services;
    }
}