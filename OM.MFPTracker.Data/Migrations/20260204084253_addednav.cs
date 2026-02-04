using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class addednav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SchemeCode",
                table: "TMutualFunds",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ISIN",
                table: "TMutualFunds",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "TMFCategory",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "DummyTable",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "TNavHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MutualFundId = table.Column<int>(type: "INTEGER", nullable: false),
                    NavDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NavValue = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    InDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TNavHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TNavHistory_TMutualFunds_MutualFundId",
                        column: x => x.MutualFundId,
                        principalTable: "TMutualFunds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TNavHistory",
                columns: new[] { "Id", "MutualFundId", "NavDate", "NavValue" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 125.4321m },
                    { 2, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 126.1000m },
                    { 3, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 98.7500m },
                    { 4, 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 99.2500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TNavHistory_MutualFundId_NavDate",
                table: "TNavHistory",
                columns: new[] { "MutualFundId", "NavDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TNavHistory");

            migrationBuilder.AlterColumn<string>(
                name: "SchemeCode",
                table: "TMutualFunds",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "ISIN",
                table: "TMutualFunds",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "TMFCategory",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "DummyTable",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldCollation: "NOCASE");
        }
    }
}
