using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OmniSphere.Users.Infrastructure.Context;

namespace OmniSphere.Users.Infrastructure.Factory;

public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
{
    public UserContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=jorge;Password=123;Database=Users;Trust Server Certificate=true;");
        return new UserContext(optionsBuilder.Options);
    }
}