using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OmniSphere.OrderState.Infrastructure.Context;

namespace OmniSphere.OrderState.Infrastructure.Factory;

public class OrderStateContextFactory : IDesignTimeDbContextFactory<OrderStateContext>
{
    public OrderStateContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderStateContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=jorge;Password=123;Database=OrderState;Trust Server Certificate=true;");
        return new OrderStateContext(optionsBuilder.Options);
    }
}