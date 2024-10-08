﻿using HMS.Employee.Core.Entity;
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
                optionsBuilder.UseSqlServer(ConnectionTest.connectionString);
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Payroll> Payroll { get; set; }

    }
}
