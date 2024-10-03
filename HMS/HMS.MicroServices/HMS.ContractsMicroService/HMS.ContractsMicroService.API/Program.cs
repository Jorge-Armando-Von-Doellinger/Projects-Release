
using HMS.ContractsMicroService.API.Module;
using HMS.ContractsMicroService.Application;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Infrastructure;
using Nuget.UserDefinition;

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

            builder.Environment.ApplicationName = "Batata";
            var consulDefinition = builder.Configuration.GetSection("UserDefinitions")
                .Get<UserDefinition>();

            builder.Services
                .AddApiModule()
                .AddApplicationModule()
                .AddInfrastructureModule();

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
