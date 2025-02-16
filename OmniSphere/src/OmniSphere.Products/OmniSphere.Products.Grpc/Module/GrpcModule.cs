using OmniSphere.Products.Grpc.ProtoMapper;

namespace OmniSphere.Products.Grpc.Module;

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
        services.AddScoped<ProductProtoMapper>();
        return services;
    }
}