using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmniSphere.OrderState.Core.Interfaces.Repository;
using OmniSphere.OrderState.Infrastructure.Context;
using OmniSphere.OrderState.Infrastructure.Implementations.Repository;

namespace OmniSphere.OrderState.Infrastructure.Module;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddContexts()
            .AddRepositories();
        return services;
    }

    private static IServiceCollection AddContexts(this IServiceCollection services)
    {
        services.AddDbContext<OrderStateContext>(x => 
            x.UseNpgsql("Server=localhost;Port=5432;Database=jorge;User Id=jorge;Password='123';"));
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderStateRepository, OrderStateRepository>();
        return services;
    }
}