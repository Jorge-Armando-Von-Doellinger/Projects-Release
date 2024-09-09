using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Employee.Infrastructure.DataContext
{
    public sealed class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions options) : base(options)
        {
        }
        public PayrollContext()
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
            
            // Set table
            modelBuilder.Entity<Payroll>()
                .ToTable("Payroll");

            //Set Primary Key
            modelBuilder.Entity<Payroll>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Payroll>()
                .HasIndex(x => x.Benefits)
                .IsUnique();

            // Configuring foreignKeys
            /*            modelBuilder.Entity<Payroll>()
                            .HasOne(x => x.Employee)
                            .WithMany()
                            .HasForeignKey(x => x.EmployeeId)
                            .IsRequired();
                        modelBuilder.Entity<Payroll>()
                            .HasOne(x => x.ContractualInformation)
                            .WithMany()
                            .HasForeignKey(x => x.ContractId)
                            .OnDelete(DeleteBehavior.NoAction)
                            .IsRequired();*/

            // Set Own
            modelBuilder.Entity<Payroll>()
                .OwnsOne(x => x.Discounts);
        }
        public DbSet<Payroll> Payroll { get; set; }

    }
}
