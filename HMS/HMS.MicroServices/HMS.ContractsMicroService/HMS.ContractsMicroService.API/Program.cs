using Consul;
using HMS.ContractsMicroService.API.Module;
using HMS.ContractsMicroService.API.Services;
using HMS.ContractsMicroService.Application;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Infrastructure;
using HMS.ContractsMicroService.Messaging;

namespace HMS.ContractsMicroService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Environment.ApplicationName = "Contracts";
            
            //builder.Environment.

            //ServiceDiscovery.Register(null);

            builder.Services.AddMemoryCache();

            builder.Services
                .AddApiModule()
                .AddApplicationModule()
                .AddInfrastructureModule()
                .AddMessagingModule();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || true)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
