using OmniSphere.OrderState.Application.Module;
using OmniSphere.OrderState.Grpc.Configurator;
using OmniSphere.OrderState.Grpc.Module;
using OmniSphere.OrderState.Grpc.Services;
using OmniSphere.OrderState.Infrastructure.Module;

namespace OmniSphere.OrderState.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        builder.Services.ConfigureInfrastructure(builder.Configuration); // Set Infrastructure configurations
        
        builder.Services // Add services in DI scope            
            .AddGrpcModule()
            .AddApplicationModule()
            .AddInfrastructureModule();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<OrderStateService>();
        app.MapGet("/",
            () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}