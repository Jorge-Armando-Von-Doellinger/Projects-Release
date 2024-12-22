using OmniSphere.Users.Grpc.Mapper;

namespace OmniSphere.Users.Grpc.Module;

public static class GrpcModule
{
    public static IServiceCollection AddGrpcModule(this IServiceCollection services)
    {
        services
            .AddMapper();
        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddScoped<UserProtoMapper>();
        return services;
    }
}