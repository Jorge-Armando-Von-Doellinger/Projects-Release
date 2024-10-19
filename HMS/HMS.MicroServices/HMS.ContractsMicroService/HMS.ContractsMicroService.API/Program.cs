using HMS.ContractsMicroService.API.Module;
using HMS.ContractsMicroService.Application;
using HMS.ContractsMicroService.Infrastructure.Modules;
using HMS.ContractsMicroService.Messaging;

namespace HMS.ContractsMicroService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache(); // Adiciona ao projeto a manipulação de memoria CACHE

            builder.Services.AddSettings(builder.Configuration); // Adiciona as settings de todas as camadas
            builder.Services        // Adiciona todas as injeções de depemdencia das camadas abaixo
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
