﻿// <auto-generated />
using System;
using HMS.Employee.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMS.Employee.Infrastructure.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HMS.Employee.Core.Entity.ContractualInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Benefits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContractType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<int>("EmploymentStatus")
                        .HasColumnType("int");

                    b.Property<DateOnly>("EndDay")
                        .HasColumnType("date");

                    b.Property<int>("ExperienceLevel")
                        .HasColumnType("int");

                    b.Property<short>("HourlySalaryInDollar")
                        .HasColumnType("smallint");

                    b.Property<int>("JobTitle")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("LunchTime")
                        .HasColumnType("time");

                    b.Property<short>("ProbationPeriodInMonths")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkingHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContractualInformation", (string)null);
                });

            modelBuilder.Entity("HMS.Employee.Core.Entity.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Age")
                        .HasColumnType("smallint");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<long>("CPF")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PIS")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.HasIndex("ContractId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PIS")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("HMS.Employee.Core.Entity.Payroll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Benefits")
                        .HasColumnType("nvarchar(450)");

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

                    b.HasIndex("Benefits")
                        .IsUnique()
                        .HasFilter("[Benefits] IS NOT NULL");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Payroll", (string)null);
                });

            modelBuilder.Entity("HMS.Employee.Core.Entity.Employee", b =>
                {
                    b.HasOne("HMS.Employee.Core.Entity.ContractualInformation", "ContractualInformation")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContractualInformation");
                });

            modelBuilder.Entity("HMS.Employee.Core.Entity.Payroll", b =>
                {
                    b.HasOne("HMS.Employee.Core.Entity.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

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

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}