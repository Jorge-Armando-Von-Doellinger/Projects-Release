using DotNetEnv;
using HMS.Notification.API.Modules;
using HMS.Notification.Application.Modules;
using HMS.Notification.Infrastructure;

namespace HMS.Notification.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.WebHost.UseKestrel();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder
            .Services
            .AddSettingsModule(builder.Configuration)
            .AddApiModule()
            .AddApplicationModule()
            .AddInfrastructureModule();
        
        var app = builder.Build();
        Console.WriteLine(Directory.GetCurrentDirectory()+"\\.env");
        Env.Load(Directory.GetCurrentDirectory()+"\\.env"); // Load .env archive
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}