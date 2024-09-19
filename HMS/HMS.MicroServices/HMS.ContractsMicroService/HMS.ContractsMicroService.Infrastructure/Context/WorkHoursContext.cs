using HMS.ContractsMicroService.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMS.ContractsMicroService.Infrastructure.Context
{
    public sealed class WorkHoursContext : DbContext
    {
        public WorkHoursContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        { }

        public WorkHoursContext()
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
            modelBuilder.Entity<WorkHours>()
                .HasKey(x => x.ID);
        }

        public DbSet<WorkHours> WorkHours { get; set; }
    }
}
