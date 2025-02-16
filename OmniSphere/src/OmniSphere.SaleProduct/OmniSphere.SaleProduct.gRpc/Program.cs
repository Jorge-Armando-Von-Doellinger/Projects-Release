using OmniSphere.SaleProduct.Application.Module;
using OmniSphere.SaleProduct.Infrastructure.Module;

namespace OmniSphere.SaleProduct.gRpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();
        
        builder.Services // Add modules from all layers
            .AddApplicationModule()
            .AddInfrastructureModule();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<Services.SaleProductService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}