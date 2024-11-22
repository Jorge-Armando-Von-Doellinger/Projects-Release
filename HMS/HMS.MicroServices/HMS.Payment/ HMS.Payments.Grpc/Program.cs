using HMS.Payments.Application.Modules;
using HMS.Payments.Grpc.Modules;
using HMS.Payments.Grpc.Services;
using HMS.Payments.Infrastructure.Modules;
using HMS.Payments.Messaging.Modules;

namespace HMS.Payments.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        builder.Services
            .AddSettingsModule(builder.Configuration) // Configura o app com base nas classes de representação
            .AddApplicationModule()
            .AddInfrastructureModule()
            .AddMessagingModule()
            .AddMemoryCache();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<GreeterService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}