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
            migrationBuilder.CreateTable(
                name: "DummyTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, collation: "NOCASE"),
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
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMFCategory", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "TMutualFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchemeCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, collation: "NOCASE"),
                    ISIN = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, collation: "NOCASE"),
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
                table: "TSpecialEvents",
                columns: new[] { "Id", "EventDate", "EventType", "Notes", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "GlobalMacro", "High inflation, pressure on Indian equities and rupee due to elevated energy prices", "Global Energy Crisis & Commodity Price Shock" },
                    { 2, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Climate", "Concerns over food inflation and rural demand in India", "El Niño Impact on Global Agriculture" },
                    { 3, new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corporate", "Sharp selloff in Adani stocks, dragged broader Indian indices", "Adani Group Short Seller Report Fallout" },
                    { 4, new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Regulatory", "Structural reform improving liquidity and settlement efficiency", "India Implements T+1 Settlement" },
                    { 5, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elections", "High volatility in Sensex and Nifty based on election outcome", "India General Election Results 2024" },
                    { 6, new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarketMilestone", "Positive sentiment and global visibility for Indian equities", "India Becomes World's 4th Largest Stock Market" },
                    { 7, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "MonetaryPolicy", "Impacted FII flows and emerging market valuations including India", "US Federal Reserve Rate Outlook Shift" },
                    { 8, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geopolitics", "Global risk-off sentiment affecting export-oriented Indian stocks", "US-China Trade & Tech Tensions Escalate" },
                    { 9, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "GlobalMacro", "Sharp correction across global equities including Indian markets", "Global Stock Market Selloff 2025" },
                    { 10, new DateTime(2025, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "TradePolicy", "Negative impact on rupee and export-focused Indian stocks", "US Imposes 50% Tariff on Indian Goods" },
                    { 11, new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geopolitics", "Oil price spike led to inflation concerns in India", "Middle East Conflict Escalation (Israel–Iran)" },
                    { 12, new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "GlobalMacro", "Spillover risk aversion affecting emerging markets including India", "Turkey Currency Crisis & EM Selloff" },
                    { 13, new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geopolitics", "Short-term volatility in Indian indices and defence stocks", "India-Pakistan Border Tensions (Pahalgam Incident)" },
                    { 14, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarketCorrection", "FII outflows and global risk aversion caused broad market decline", "Indian Market Correction Early 2025" },
                    { 15, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "MonetaryPolicy", "Pressure on valuations and capital flows to India", "US Fed Signals Prolonged Higher Rates" },
                    { 16, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "EconomicData", "Boosted confidence in Indian equities and domestic sectors", "India GDP Growth Beats Expectations" },
                    { 17, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "TradePolicy", "Positive rally in Indian markets and strengthening of rupee", "India-US Trade Deal Announcement" },
                    { 18, new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technology", "Valuation reset in IT and tech stocks including Indian IT sector", "Global AI & Tech Stock Selloff" },
                    { 19, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geopolitics", "Energy and fertilizer price volatility affecting Indian inflation", "Russia-Ukraine War Commodity Impact" },
                    { 20, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "GlobalMacro", "Increased volatility and cautious FII positioning in Indian markets", "Global Elections & Policy Uncertainty (US/EU)" }
                });

            migrationBuilder.InsertData(
                table: "TMutualFunds",
                columns: new[] { "Id", "ISIN", "MFCategoryId", "SchemeCode", "SchemeName" },
                values: new object[,]
                {
                    { 1, "123AA", 2, "B2C67", "DK MF" },
                    { 2, "12B4C", 1, "AAC27", "RK MF" }
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

            migrationBuilder.CreateIndex(
                name: "IX_TNavHistory_MutualFundId_NavDate",
                table: "TNavHistory",
                columns: new[] { "MutualFundId", "NavDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TSpecialEvents_EventDate",
                table: "TSpecialEvents",
                column: "EventDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DummyTable");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "TNavHistory");

            migrationBuilder.DropTable(
                name: "TSpecialEvents");

            migrationBuilder.DropTable(
                name: "TMutualFunds");

            migrationBuilder.DropTable(
                name: "TMFCategory");
        }
    }
}
