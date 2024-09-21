using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.ContractsMicroService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkHours",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IntervalStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IntervalEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ExitTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHours", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContract",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobTitle = table.Column<int>(type: "int", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    HourlySalaryInDollar = table.Column<short>(type: "smallint", nullable: false),
                    WorkHoursID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    ProbationPeriodInMonths = table.Column<short>(type: "smallint", nullable: false),
                    EmploymentStatus = table.Column<int>(type: "int", nullable: false),
                    EndDay = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContract", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_WorkHours_WorkHoursID",
                        column: x => x.WorkHoursID,
                        principalTable: "WorkHours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_WorkHoursID",
                table: "EmployeeContract",
                column: "WorkHoursID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeContract");

            migrationBuilder.DropTable(
                name: "WorkHours");
        }
    }
}
