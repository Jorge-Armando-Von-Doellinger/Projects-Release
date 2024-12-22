using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Employee.Infrastructure.DataContext
{
    public sealed class ProfessionalHistoryContext : DbContext
    {
        public ProfessionalHistoryContext(DbContextOptions options) : base(options)
        {
        }
        public ProfessionalHistoryContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectionTest.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HMS.Employee.Core.Entity.InternalProfessionalHistory>()
                .ToTable("ProfessionalHistory");
        }
        DbSet<InternalProfessionalHistory> InternalProfessionalHistory { get; set; }

    }
}
