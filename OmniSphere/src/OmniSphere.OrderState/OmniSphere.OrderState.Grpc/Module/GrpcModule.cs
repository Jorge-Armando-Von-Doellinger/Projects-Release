using OmniSphere.OrderState.Grpc.ProtoMapper;

namespace OmniSphere.OrderState.Grpc.Module;

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
        services.AddScoped<OrderStateProtoDtoMapper>();
        return services;
    }
}