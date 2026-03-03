using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFundTransactionMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TFundTransaction",
                columns: new[] { "FundTransactionId", "Amount", "Charges", "ConsumedBuyTransactionId", "FolioId", "FundId", "Nav", "Remarks", "SellGroupId", "TransactionDate", "Type", "Units", "UnitsLeft" },
                values: new object[] { 3, 1050.00m, 1.25m, null, 2, 2, 10.500000m, "more mf bought Investment", null, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100.000000m, 60.000000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TFundTransaction",
                keyColumn: "FundTransactionId",
                keyValue: 3);
        }
    }
}
