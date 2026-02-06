using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class MFdataadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TMutualFunds",
                columns: new[] { "Id", "ISIN", "MFCategoryId", "SchemeCode", "SchemeName" },
                values: new object[,]
                {
                    { 3, "INF879O01027", 1, "122639", "Parag Parikh Flexi Cap Fund - Direct Plan - Growth" },
                    { 4, "INF760K01167", 2, "102920", "CANARA ROBECO LARGE AND MID CAP FUND - REGULAR PLAN - GROWTH" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TMutualFunds",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TMutualFunds",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
