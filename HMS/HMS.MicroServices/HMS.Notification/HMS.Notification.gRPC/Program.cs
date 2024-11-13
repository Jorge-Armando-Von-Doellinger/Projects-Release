using DotNetEnv;
using HMS.Notification.Application.Modules;
using HMS.Notification.gRPC.Modules;
using HMS.Notification.gRPC.Services;
using HMS.Notification.Infrastructure;

namespace HMS.Notification.gRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            
            builder.Services
                .AddSettingsModule(builder.Configuration)
                .AddgRpcModule()
                .AddApplicationModule()
                .AddInfrastructureModule();
            
            Env.Load(Directory.GetCurrentDirectory()+"\\.env");
            
            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            app.MapGrpcService<NotificationService>();
            app.MapGet("/batata", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            app.Run();
        }
    }
}