using HMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace HMS.Infrastructure.DataContext
{
    public class ClientContext : DbContext
    {
        private const string SqlServerConnection = "Server=localhost,1433; " +
            "Database=ProjectsRelease; " +
            "User Id=SA; " +
            "Password=YourStrong!Passw0rd; " +
            "TrustServerCertificate=true";
        // DESENVOLVIMENTO
        public ClientContext()
        {

        }
        //---
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
        }

        public DbSet<ClientEntity> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(SqlServerConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        }
    }
}
