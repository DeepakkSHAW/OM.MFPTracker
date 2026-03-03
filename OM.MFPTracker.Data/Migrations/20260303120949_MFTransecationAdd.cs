using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class MFTransecationAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMutualFundTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Folio = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    FundName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FundCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    FundType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Units = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    NAV = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Note = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedUtc = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMutualFundTransaction", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TMutualFundTransaction",
                columns: new[] { "Id", "AmountPaid", "Date", "Folio", "FundCode", "FundName", "FundType", "NAV", "Note", "Source", "Units", "UpdatedUtc" },
                values: new object[,]
                {
                    { 1, 480.000000000000m, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "DK001", "HDF003", "HDFC Small cap", "GR - Multi Cap", 12.000000m, "data seeding", "Kotak M Bank", 40.000000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 350.000000000000m, new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "DK001", "HDF003", "HDFC Flexi cap", "GR - flexi Cap", 2.500000m, "data seeding", "Kotak M Bank", 140.000000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TFundTransaction_FolioId_FundId_TransactionDate",
                table: "TFundTransaction",
                columns: new[] { "FolioId", "FundId", "TransactionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_TFundTransaction_TransactionDate",
                table: "TFundTransaction",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_TFundTransaction_Type",
                table: "TFundTransaction",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFundTransaction_Date",
                table: "TMutualFundTransaction",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFundTransaction_Folio",
                table: "TMutualFundTransaction",
                column: "Folio");

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFundTransaction_Folio_FundName_Date",
                table: "TMutualFundTransaction",
                columns: new[] { "Folio", "FundName", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFundTransaction_FundName_FundCode",
                table: "TMutualFundTransaction",
                columns: new[] { "FundName", "FundCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMutualFundTransaction");

            migrationBuilder.DropIndex(
                name: "IX_TFundTransaction_FolioId_FundId_TransactionDate",
                table: "TFundTransaction");

            migrationBuilder.DropIndex(
                name: "IX_TFundTransaction_TransactionDate",
                table: "TFundTransaction");

            migrationBuilder.DropIndex(
                name: "IX_TFundTransaction_Type",
                table: "TFundTransaction");
        }
    }
}
