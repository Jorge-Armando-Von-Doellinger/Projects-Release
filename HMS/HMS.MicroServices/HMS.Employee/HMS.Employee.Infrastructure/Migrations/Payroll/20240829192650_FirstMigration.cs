﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Employee.Infrastructure.Migrations.Payroll
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlySalary = table.Column<int>(type: "int", nullable: false),
                    HoursWorkedInMonth = table.Column<short>(type: "smallint", nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmountOfBenefits = table.Column<int>(type: "int", nullable: false),
                    Discounts_MandatoryDiscounts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discounts_TotalDiscounts = table.Column<int>(type: "int", nullable: false),
                    Discounts_OtherDiscounts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payroll");
        }
    }
}
