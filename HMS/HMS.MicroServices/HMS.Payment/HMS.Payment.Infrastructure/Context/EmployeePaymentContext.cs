using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;
using HMS.Payments.Core.Json;
using HMS.Payments.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HMS.Payments.Infratructure.Context
{
    public sealed class EmployeePaymentContext : DbContext
    {
        public EmployeePaymentContext() 
        { }

        public EmployeePaymentContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if(optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(ConnectionTest.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Set table and primaryKey
            modelBuilder.Entity<EmployeePayment>()
                .ToTable("Payroll")
                .HasKey(p => p.ID);

            /*var converter = new ValueConverter<List<MandatoryDiscountsEnum>, string>(
                v => JsonConvert.Serialize(v).Result, // Converte List<enum> para string (JSON)
                v => JsonConvert.Deserialize<List<MandatoryDiscountsEnum>>(v).Result
                        );*/
            // Set Own
            modelBuilder.Entity<EmployeePayment>()
                .OwnsOne(x => x.Discounts, discount =>
                {
                    //discount.Property(d => d.MandatoryDiscounts).HasConversion(converter);
                });


        }

        public DbSet<Core.Entity.EmployeePayment> Payroll { get; set; }    
    }
}
