using OmniSphere.Products.Application.Module;
using OmniSphere.Products.Grpc.Configuring;
using OmniSphere.Products.Grpc.Module;
using OmniSphere.Products.Grpc.Services;
using OmniSphere.Products.Infrastructure.Module;

namespace OmniSphere.Products.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddGrpc();
        builder.Services
            .ConfigureServices(builder.Configuration)
            .AddGrpcModule()
            .AddApplicationModule()
            .AddInfrastructureModule();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<ProductService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}