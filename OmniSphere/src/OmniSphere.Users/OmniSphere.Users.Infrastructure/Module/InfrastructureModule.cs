using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OmniSphere.Users.Core.Interfaces.Repository;
using OmniSphere.Users.Infrastructure.Configs;
using OmniSphere.Users.Infrastructure.Context;
using OmniSphere.Users.Infrastructure.Implementation.Repository;

namespace OmniSphere.Users.Infrastructure.Module;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        Console.WriteLine("Adding Infrastructure Module");
        services
            .AddContexts()
            .AddRepository();
        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
    private static IServiceCollection AddContexts(this IServiceCollection services)
    {
        services.AddDbContext<UserContext>((serviceProvider,options) =>
        {
            var configs = serviceProvider.GetRequiredService<IOptionsMonitor<DatabaseConfigs>>();
            Console.WriteLine(configs.CurrentValue.ConnectionString);
            options.UseNpgsql(configs.CurrentValue.ConnectionString);
        });
        return services;
    }
}