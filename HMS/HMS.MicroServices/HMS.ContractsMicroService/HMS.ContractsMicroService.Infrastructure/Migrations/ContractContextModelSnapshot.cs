﻿// <auto-generated />
using System;
using HMS.ContractsMicroService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMS.ContractsMicroService.Infrastructure.Migrations
{
    [DbContext(typeof(ContractContext))]
    partial class ContractContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HMS.ContractsMicroService.Core.Entity.EmployeeContract", b =>
                {
                    b.Property<Guid>("ID")
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

                    b.Property<short>("ProbationPeriodInMonths")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WorkHoursID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("WorkHoursID");

                    b.ToTable("EmployeeContract");
                });

            modelBuilder.Entity("HMS.ContractsMicroService.Core.Entity.WorkHours", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<TimeOnly>("EntryTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("ExitTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("IntervalEndTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("IntervalStartTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("WorkHours");
                });

            modelBuilder.Entity("HMS.ContractsMicroService.Core.Entity.EmployeeContract", b =>
                {
                    b.HasOne("HMS.ContractsMicroService.Core.Entity.WorkHours", "WorkHours")
                        .WithMany()
                        .HasForeignKey("WorkHoursID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkHours");
                });
#pragma warning restore 612, 618
        }
    }
}
