using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.DataContext.ConfiguringContexts
{
    internal class PayrollConfiguring
    {
        public PayrollConfiguring(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder);
        }
        internal void Configure(ModelBuilder modelBuilder)
        {

            // Set table and primaryKey
            modelBuilder.Entity<Payroll>()
                .ToTable("Payroll")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Payroll>()
                .HasIndex(x => x.Benefits)
                .IsUnique();

            // Configuring foreignKeys
            modelBuilder.Entity<Payroll>()
                .HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            // Set Own
            modelBuilder.Entity<Payroll>()
                .OwnsOne(x => x.Discounts);


        }
    }
}
