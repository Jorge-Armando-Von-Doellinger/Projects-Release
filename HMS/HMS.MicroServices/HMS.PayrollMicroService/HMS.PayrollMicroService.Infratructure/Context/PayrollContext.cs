using HMS.PayrollMicroService.Core.Entity;
using HMS.PayrollMicroService.Core.Enums;
using HMS.PayrollMicroService.Core.Json;
using HMS.PayrollMicroService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HMS.PayrollMicroService.Infratructure.Context
{
    public sealed class PayrollContext : DbContext
    {
        public PayrollContext() 
        { }

        public PayrollContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
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
            modelBuilder.Entity<Payroll>()
                .ToTable("Payroll")
                .HasKey(p => p.ID);

            /*var converter = new ValueConverter<List<MandatoryDiscountsEnum>, string>(
                v => JsonConvert.Serialize(v).Result, // Converte List<enum> para string (JSON)
                v => JsonConvert.Deserialize<List<MandatoryDiscountsEnum>>(v).Result
                        );*/
            // Set Own
            modelBuilder.Entity<Payroll>()
                .OwnsOne(x => x.Discounts, discount =>
                {
                    //discount.Property(d => d.MandatoryDiscounts).HasConversion(converter);
                });


        }

        public DbSet<Payroll> Payroll { get; set; }    
    }
}
