using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DummyTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    InDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DummyTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Signature = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    InDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TMFCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMFCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TMutualFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchemeCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ISIN = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    SchemeName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MFCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    InDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMutualFunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TMutualFunds_TMFCategory_MFCategoryId",
                        column: x => x.MFCategoryId,
                        principalTable: "TMFCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DummyTable",
                columns: new[] { "Id", "FirstName" },
                values: new object[,]
                {
                    { 1, "DK" },
                    { 2, "RS" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Signature" },
                values: new object[,]
                {
                    { 1, new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rupam", "Shaw", "RS" },
                    { 2, new DateTime(1981, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deepak", "Shaw", "DK" },
                    { 3, new DateTime(1974, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jagruti", "Shaw", "JS" },
                    { 4, new DateTime(2001, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Divyam", "Shaw", "DS" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Durga Prasad", "Shaw", "DP" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Radha", "Shaw", "RD" }
                });

            migrationBuilder.InsertData(
                table: "TMFCategory",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "E-Multi Cap Mutual Funds" },
                    { 2, "E-Flexi Cap Mutual Funds" },
                    { 3, "E-Large & MidCap Mutual Funds" },
                    { 4, "E-Large Cap Mutual Funds" },
                    { 5, "E-Mid Cap Mutual Funds" },
                    { 6, "E-ELSS Mutual Funds" },
                    { 7, "E-Dividend Yield Mutual Funds" },
                    { 8, "E-Contra Mutual Funds" },
                    { 9, "E-Sectoral Mutual Funds" },
                    { 10, "E-Value Oriented Mutual Funds" }
                });

            migrationBuilder.InsertData(
                table: "TMutualFunds",
                columns: new[] { "Id", "ISIN", "MFCategoryId", "SchemeCode", "SchemeName" },
                values: new object[,]
                {
                    { 1, "123AA", 2, "B2C67", "DK MF" },
                    { 2, "12B4C", 1, "AAC27", "RK MF" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TMFCategory_CategoryName",
                table: "TMFCategory",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFunds_ISIN",
                table: "TMutualFunds",
                column: "ISIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFunds_MFCategoryId",
                table: "TMutualFunds",
                column: "MFCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFunds_SchemeCode",
                table: "TMutualFunds",
                column: "SchemeCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DummyTable");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "TMutualFunds");

            migrationBuilder.DropTable(
                name: "TMFCategory");
        }
    }
}
