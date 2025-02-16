using OmniSphere.Users.Application.Module;
using OmniSphere.Users.Grpc.Configurator;
using OmniSphere.Users.Grpc.Module;
using OmniSphere.Users.Grpc.Services;
using OmniSphere.Users.Infrastructure.Module;

namespace OmniSphere.Users.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        builder.Services // Add layer configuration
            .ConfigureInfrastructureLayer(builder.Configuration);
        
        builder.Services
            .AddGrpcModule()
            .AddApplicationModule()
            .AddInfrastructureModule();
        
        var app = builder.Build();
        Console.WriteLine("Modified");
        // Configure the HTTP request pipeline.
        app.MapGrpcService<UserServices>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}