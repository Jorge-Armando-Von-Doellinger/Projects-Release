using HMS.Employee.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.DataContext
{
    public sealed class ContractualInformationContext : DbContext
    {
        public ContractualInformationContext(DbContextOptions options) : base(options)
        {
        }
        public ContractualInformationContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectionTest.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<ContractualInformation> ContractualInformation { get; set; }
    }
}
