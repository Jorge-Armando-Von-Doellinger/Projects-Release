using OmniSphere.Payments.Application.Module;
using OmniSphere.Payments.gRpc.Configurator;
using OmniSphere.Payments.gRpc.Module;
using OmniSphere.Payments.gRpc.Services;
using OmniSphere.Payments.Infrastructure.Module;

namespace OmniSphere.Payments.gRpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc(); // Add grpc service

        builder.Services // Add layers configs
            .AddInfrastructureConfigs(builder.Configuration);
        
        builder.Services
            .AddGrpcModule()
            .AddApplicationModule()
            .AddInfrastructureModule();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<Services.PaymentProtoService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}