using OmniSphere.Payments.gRpc.Mapper;

namespace OmniSphere.Payments.gRpc.Module;

public static class GrpcModule
{
    internal static IServiceCollection AddGrpcModule(this IServiceCollection services)
    {
        services
            .AddMappers();
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<PaymentProtoMapper>();
        return services;
    }
}