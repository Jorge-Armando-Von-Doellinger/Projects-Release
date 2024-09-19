using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.ContractsMicroService.Infrastructure.Migrations.WorkHours
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkHours");
        }
    }
}
