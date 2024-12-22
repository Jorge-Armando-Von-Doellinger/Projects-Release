using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.Context
{
    public sealed class EmployeeContext : DbContext
    {
        internal readonly string connectionString = "Server=localhost,1433; " +
            "Database=ProjectsRelease;" +
            "User Id=SA;" +
            "Password=YourStrong!Passw0rd;" +
            "TrustServerCertificate=true";
        public EmployeeContext(DbContextOptions options) : base(options)
        {
        }
        public EmployeeContext()
        {
        }

        public DbSet<Core.Entity.Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectionTest.connectionString);
        }


    }
}
