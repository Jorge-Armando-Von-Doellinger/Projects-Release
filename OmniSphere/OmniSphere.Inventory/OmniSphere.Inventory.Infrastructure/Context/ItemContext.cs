using Microsoft.EntityFrameworkCore;

namespace OmniSphere.Inventory.Infrastructure.Context;

public class ItemContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=Inventory;Username=postgres;Password=123");
    }
}