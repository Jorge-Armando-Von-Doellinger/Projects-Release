using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Employee.Infrastructure.Migrations.ContractualInformation
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractualInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobTitle = table.Column<int>(type: "int", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    HourlySalaryInDollar = table.Column<short>(type: "smallint", nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LunchTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    ProbationPeriodInMonths = table.Column<short>(type: "smallint", nullable: false),
                    EmploymentStatus = table.Column<int>(type: "int", nullable: false),
                    EndDay = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractualInformation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractualInformation");
        }
    }
}
