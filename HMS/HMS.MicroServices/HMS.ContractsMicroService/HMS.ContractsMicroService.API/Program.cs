using HMS.ContractsMicroService.API.Module;
using HMS.ContractsMicroService.API.Services.Data;
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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ---------------------------

            builder.Services.AddMemoryCache(); // Adiciona ao projeto a manipulação de memoria CACHE
            builder.Services.AddSettings(builder.Configuration); // Adiciona as settings de todas as camadas
            SettingsStartupState.SetSettingsCompleted(); // Permite os servicos background iniciarem!
            builder.Services        // Adiciona todas as injeções de depemdencia das camadas abaixo
                .AddMessagingModule()
                .AddInfrastructureModule()
                .AddApplicationModule()
                .AddApiModule();

            // ------------------------------
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
