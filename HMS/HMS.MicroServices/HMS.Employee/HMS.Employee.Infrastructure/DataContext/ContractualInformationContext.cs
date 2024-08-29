﻿using HMS.Employee.Core.Entity;
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
                optionsBuilder.UseSqlServer(ConnectiosTest.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HMS.Employee.Core.Entity.ContractualInformation>()
                .ToTable("ContractualInformation");
            modelBuilder.Entity<ContractualInformation>()
                .HasKey(e => e.Id);
        }
        DbSet<ContractualInformation> ContractualInformation { get; set; }
    }
}
