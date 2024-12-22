using HMS.Employee.Core.Entity;
using HMS.Employee.Infrastructure.DataContext.ConfiguringContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Employee.Infrastructure.DataContext
{
    public sealed class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions options) : base(options)
        { }
        public DefaultContext() // Para não ocorrer erros durante a adição ou atualização das migrations
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectionTest.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ContextConfigurator().ConfigureAllContexts(modelBuilder); 
        }

        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<Core.Entity.Employee> Employee { get; set; }
        public DbSet<ContractualInformation> ContractualInformation { get; set; }
    }
}
