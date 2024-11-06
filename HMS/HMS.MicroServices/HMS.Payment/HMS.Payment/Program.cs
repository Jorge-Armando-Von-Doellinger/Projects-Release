
using HMS.Payments.API.Modules;
using HMS.Payments.Application.Modules;
using HMS.Payments.Infrastructure.Modules;
using HMS.Payments.Messaging.Modules;

namespace HMS.Payments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Threading.SynchronizationContext.SetSynchronizationContext(null);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache(); // Adding Cache

            builder.Services
                .ConfigureAppSettings(builder.Configuration);


            // Adding Dependency Injection modules
            builder.Services
                .AddApiModule()
                .AddApplicationModule()
                .AddInfrastructureModule()
                .AddMessagingModule();


            var app = builder.Build();

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
}
