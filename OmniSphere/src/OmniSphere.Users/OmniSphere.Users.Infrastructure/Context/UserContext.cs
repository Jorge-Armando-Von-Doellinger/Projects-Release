using Microsoft.EntityFrameworkCore;
using OmniSphere.Users.Core.Entity;

namespace OmniSphere.Users.Infrastructure.Context;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        //this.Database.EnsureCreated();
    }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userEntity = modelBuilder.Entity<User>();
        userEntity
            .HasKey(x => x.Id);
        userEntity
            .HasIndex(x => x.Email)
            .IsUnique();
        userEntity
            .HasIndex(x => x.Username)
            .IsUnique();
        userEntity
            .Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(100);
        userEntity
            .Property(x => x.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        userEntity 
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        base.OnModelCreating(modelBuilder);
    }
}