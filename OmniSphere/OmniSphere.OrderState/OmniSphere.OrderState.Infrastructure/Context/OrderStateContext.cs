using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OmniSphere.OrderState.Infrastructure.Configs;

namespace OmniSphere.OrderState.Infrastructure.Context;

public class OrderStateContext : DbContext
{

    public OrderStateContext(DbContextOptions<OrderStateContext> options) : base(options)
    {
    }

    public DbSet<Core.Entity.OrderState> OrderStates { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Core.Entity.OrderState>()
            .HasKey(x => x.Id);
    }
}