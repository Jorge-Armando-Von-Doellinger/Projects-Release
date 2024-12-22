using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.DataContext.ConfiguringContexts
{
    internal sealed class EmployeeConfiguring
    {
        internal EmployeeConfiguring(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder);
        }
        internal void Configure(ModelBuilder modelBuilder)
        {
            // Indicate Table and PrimaryKey
            modelBuilder.Entity<Core.Entity.Employee>()
                .ToTable("Employee")
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
            modelBuilder.Entity<Core.Entity.Employee>()
                .HasOne(e => e.ContractualInformation)
                .WithMany() // Usa WithMany se um contrato pode ter múltiplos funcionários
                .HasForeignKey(x => x.ContractId) // Define a chave estrangeira
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();


        }
    }
}
