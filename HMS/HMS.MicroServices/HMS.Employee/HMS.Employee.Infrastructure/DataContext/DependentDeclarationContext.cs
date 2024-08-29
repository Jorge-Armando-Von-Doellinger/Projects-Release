using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.DataContext
{
    public sealed class DependentDeclarationContext : DbContext
    {
        public DependentDeclarationContext(DbContextOptions options) : base(options)
        {
        }
        public DependentDeclarationContext()
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
            modelBuilder.Entity<HMS.Employee.Core.Entity.DependentDeclaration>()
                .ToTable("DependentDeclaration");
            modelBuilder.Entity<DependentDeclaration>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<DependentDeclaration>()
                .HasIndex(x => x.CPF)
                .IsUnique();
        }
        DbSet<DependentDeclaration> DependentDeclaration { get; set; }

    }
}
