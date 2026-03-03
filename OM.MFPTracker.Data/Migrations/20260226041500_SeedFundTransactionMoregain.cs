using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFundTransactionMoregain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TFundTransaction",
                columns: new[] { "FundTransactionId", "Amount", "Charges", "ConsumedBuyTransactionId", "FolioId", "FundId", "Nav", "Remarks", "SellGroupId", "TransactionDate", "Type", "Units", "UnitsLeft" },
                values: new object[] { 4, 480.00m, 5.00m, 3, 1, 1, 12.000000m, "Partial Profit Booking", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 40.000000m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TFundTransaction",
                keyColumn: "FundTransactionId",
                keyValue: 4);
        }
    }
}
