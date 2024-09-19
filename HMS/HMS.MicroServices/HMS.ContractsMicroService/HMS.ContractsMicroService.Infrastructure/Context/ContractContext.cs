using HMS.ContractsMicroService.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMS.ContractsMicroService.Infrastructure.Context
{
    public sealed class ContractContext : DbContext
    {
        public ContractContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        { }

        public ContractContext()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectionTest.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EmployeeContract>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<EmployeeContract>()
                .HasOne(x => x.WorkHours)
                .WithMany()
                .HasForeignKey(x => x.WorkHoursID)
                .IsRequired();
        }

        public DbSet<EmployeeContract> EmployeeContract { get; set; }
    }
}
