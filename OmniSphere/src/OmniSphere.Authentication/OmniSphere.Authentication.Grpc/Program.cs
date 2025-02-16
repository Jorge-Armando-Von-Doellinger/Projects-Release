using OmniSphere.Authentication.Application.Module;
using OmniSphere.Authentication.Grpc.Configurator;
using OmniSphere.Authentication.Grpc.Services;
using OmniSphere.Authentication.Infrastructure.Module;

namespace OmniSphere.Authentication.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        builder.Services
            .ConfigureApplicationLayer(builder.Configuration);
        
        builder.Services
            .AddApplicationModule()
            .AddInfrastructureModule();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<Services.AuthService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}