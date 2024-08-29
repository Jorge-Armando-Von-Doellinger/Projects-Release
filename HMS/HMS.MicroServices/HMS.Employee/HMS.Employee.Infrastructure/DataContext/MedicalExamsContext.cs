using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.DataContext
{
    public sealed class MedicalExamsContext : DbContext
    {
        public MedicalExamsContext(DbContextOptions options) : base(options)
        {
        }
        public MedicalExamsContext()
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
            modelBuilder.Entity<HMS.Employee.Core.Entity.MedicalExams>()
                .ToTable("MedicalExams");
            modelBuilder.Entity<MedicalExams>()
                .HasKey(x => x.Id);
        }
        DbSet<MedicalExams> MedicalExams { get; set; }

    }
}
