using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class First2 : Migration
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
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    HourlySalaryInDollar = table.Column<short>(type: "smallint", nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LunchTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    ProbationPeriodInMonths = table.Column<short>(type: "smallint", nullable: false),
                    EmploymentStatus = table.Column<int>(type: "int", nullable: false),
                    EndDay = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractualInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CPF = table.Column<long>(type: "bigint", nullable: false),
                    PIS = table.Column<long>(type: "bigint", nullable: false),
                    Age = table.Column<short>(type: "smallint", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_ContractualInformation_ContractId",
                        column: x => x.ContractId,
                        principalTable: "ContractualInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlySalary = table.Column<int>(type: "int", nullable: false),
                    HoursWorkedInMonth = table.Column<short>(type: "smallint", nullable: false),
                    TotalAmountOfBenefits = table.Column<int>(type: "int", nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Payroll_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ContractId",
                table: "Employee",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CPF",
                table: "Employee",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Email",
                table: "Employee",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PhoneNumber",
                table: "Employee",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PIS",
                table: "Employee",
                column: "PIS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_Benefits",
                table: "Payroll",
                column: "Benefits",
                unique: true,
                filter: "[Benefits] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_EmployeeId",
                table: "Payroll",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ContractualInformation");
        }
    }
}
