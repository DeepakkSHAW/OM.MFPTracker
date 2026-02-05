using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TSpecialEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    EventType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSpecialEvents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TSpecialEvents",
                columns: new[] { "Id", "EventDate", "EventType", "Notes", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elections", "No major impact on stock market", "Election in West Bengal" },
                    { 2, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MajorEvent", "Market expected to react to budget announcements", "India Budget Date" },
                    { 3, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "EarningsRelease", "Top 10 index companies releasing Q4 earnings", "Quarterly Corporate Earnings Release" },
                    { 4, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macroeconomic", "Expectations of interest rate decision", "RBI Policy Meeting" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TSpecialEvents_EventDate",
                table: "TSpecialEvents",
                column: "EventDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TSpecialEvents");
        }
    }
}
