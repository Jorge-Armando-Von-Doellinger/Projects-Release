﻿// <auto-generated />
using System;
using HMS.Employee.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMS.Employee.Infrastructure.Migrations.Payroll
{
    [DbContext(typeof(PayrollContext))]
    [Migration("20240829192650_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HMS.Employee.Core.Entity.Payroll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Benefits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("HourlySalary")
                        .HasColumnType("int");

                    b.Property<short>("HoursWorkedInMonth")
                        .HasColumnType("smallint");

                    b.Property<int>("TotalAmountOfBenefits")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Payroll", (string)null);
                });

            modelBuilder.Entity("HMS.Employee.Core.Entity.Payroll", b =>
                {
                    b.OwnsOne("HMS.Employee.Core.Data.Discounts.Discount", "Discounts", b1 =>
                        {
                            b1.Property<Guid>("PayrollId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("MandatoryDiscounts")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("OtherDiscounts")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("TotalDiscounts")
                                .HasColumnType("int");

                            b1.HasKey("PayrollId");

                            b1.ToTable("Payroll");

                            b1.WithOwner()
                                .HasForeignKey("PayrollId");
                        });

                    b.Navigation("Discounts")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
