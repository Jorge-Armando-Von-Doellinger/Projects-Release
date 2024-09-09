using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

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
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectiosTest.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Indicate Table
            modelBuilder.Entity<Core.Entity.Employee>()
                .ToTable("Employee");

            // Set Primary Key
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasKey(x => x.Id);



            // Set Columns Unique
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

            // Set ForeignKeys
           /* modelBuilder.Entity<Core.Entity.Employee>()
                .HasOne(e => e.ContractualInformation)
                .WithOne() // Usa WithMany se um contrato pode ter múltiplos funcionários
                .HasForeignKey(nameof(ContractualInformation)) // Define a chave estrangeira
                //.OnDelete(DeleteBehavior.Restrict)
                .IsRequired();*/


        }


        

    }
}
