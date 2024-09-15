using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.DataContext.ConfiguringContexts
{
    internal sealed class ContractualConfiguring
    {
        public ContractualConfiguring(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder);
        }
        internal void Configure(ModelBuilder modelBuilder)
        {
            //Set Table and Primary Key
            modelBuilder.Entity<HMS.Employee.Core.Entity.ContractualInformation>()
                .ToTable("ContractualInformation")
                .HasKey(ci => ci.Id);
        }
    }
}
