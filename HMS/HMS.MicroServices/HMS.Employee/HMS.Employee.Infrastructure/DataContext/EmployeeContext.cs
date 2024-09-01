using HMS.Employee.Core.Entity;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectiosTest.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Core.Entity.Employee>()
                .ToTable("Employee");
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasIndex(x => x.PIS)
                .IsUnique();
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasIndex(x => x.CPF)
                .IsUnique();
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasIndex(x => x.Email)
                .IsUnique();
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasIndex(x => x.PhoneNumber)
                .IsUnique();
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasOne<Payroll>()
                .WithMany()
                .HasForeignKey(x => x.Id);
        }
        public DbSet<Core.Entity.Employee> Employee { get; set; }
        

    }
}
