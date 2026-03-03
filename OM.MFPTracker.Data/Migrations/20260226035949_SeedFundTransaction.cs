using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OM.MFPTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFundTransaction : Migration
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
                name: "TAMC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false, collation: "NOCASE"),
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAMC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TFolioHolder",
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
                    table.PrimaryKey("PK_TFolioHolder", x => x.Id);
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
                name: "TOperationalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsTransactionAllowed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsNavAllowed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOperationalStatus", x => x.Id);
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
                name: "TFolio",
                columns: table => new
                {
                    FolioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FolioName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false, collation: "NOCASE"),
                    FolioDescription = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FolioPurpose = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FolioIsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    AttachedBank = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    AmcId = table.Column<int>(type: "INTEGER", nullable: true),
                    FolioHolderId = table.Column<int>(type: "INTEGER", nullable: true),
                    InDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFolio", x => x.FolioId);
                    table.ForeignKey(
                        name: "FK_TFolio_TAMC_AmcId",
                        column: x => x.AmcId,
                        principalTable: "TAMC",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TFolio_TFolioHolder_FolioHolderId",
                        column: x => x.FolioHolderId,
                        principalTable: "TFolioHolder",
                        principalColumn: "Id");
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
                    AmcId = table.Column<int>(type: "INTEGER", nullable: false),
                    MFCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationalStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    InDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMutualFunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TMutualFunds_TAMC_AmcId",
                        column: x => x.AmcId,
                        principalTable: "TAMC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TMutualFunds_TMFCategory_MFCategoryId",
                        column: x => x.MFCategoryId,
                        principalTable: "TMFCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TMutualFunds_TOperationalStatus_OperationalStatusId",
                        column: x => x.OperationalStatusId,
                        principalTable: "TOperationalStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TFundTransaction",
                columns: table => new
                {
                    FundTransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Units = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    UnitsLeft = table.Column<decimal>(type: "TEXT", nullable: true),
                    Nav = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Charges = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: true),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SellGroupId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ConsumedBuyTransactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    InDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FolioId = table.Column<int>(type: "INTEGER", nullable: false),
                    FundId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFundTransaction", x => x.FundTransactionId);
                    table.ForeignKey(
                        name: "FK_TFundTransaction_TFolio_FolioId",
                        column: x => x.FolioId,
                        principalTable: "TFolio",
                        principalColumn: "FolioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TFundTransaction_TFundTransaction_ConsumedBuyTransactionId",
                        column: x => x.ConsumedBuyTransactionId,
                        principalTable: "TFundTransaction",
                        principalColumn: "FundTransactionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TFundTransaction_TMutualFunds_FundId",
                        column: x => x.FundId,
                        principalTable: "TMutualFunds",
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
                table: "TAMC",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "HDFC", "HDFC Asset Management Company" },
                    { 2, "ICICI", "ICICI Prudential Asset Management" },
                    { 3, "SBI", "SBI Funds Management" },
                    { 4, "AXIS", "Axis Asset Management" },
                    { 5, "KOTAK", "Kotak Mahindra Asset Management" },
                    { 6, "PPFAS", "PPFAS Mutual Fund" },
                    { 7, "MAMF", "Mirae Asset Mutual Fund" },
                    { 8, "CRAMC", "CANARA ROBECO Mutual Fund" },
                    { 9, "BANDHAN MF", "BANDHAN Mutual Fund" },
                    { 10, "NIMF", "Nippon India Mutual Fund" },
                    { 11, "TMF", "Tata Asset Management" }
                });

            migrationBuilder.InsertData(
                table: "TFolioHolder",
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
                table: "TOperationalStatus",
                columns: new[] { "Id", "Code", "Description", "IsNavAllowed", "IsTransactionAllowed" },
                values: new object[,]
                {
                    { 1, "ACTIVE", "Open to new investors with continuous subscriptions and redemption", true, true },
                    { 2, "CLOSED", "Units available only during NFO with fixed maturity", false, false },
                    { 3, "SUSPENDED", "Temporarily halted due to regulatory or market conditions", false, false },
                    { 4, "CLOSED_NEW", "Not accepting new investments", false, false },
                    { 5, "LIQUIDATED", "Fund closed and assets paid out or merged", false, false }
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
                table: "TFolio",
                columns: new[] { "FolioId", "AmcId", "AttachedBank", "FolioDescription", "FolioHolderId", "FolioIsActive", "FolioName", "FolioPurpose" },
                values: new object[,]
                {
                    { 1, 1, "HDFC Bank", "Primary Investment Folio", 1, true, "HDFC-001", "Long term wealth creation" },
                    { 2, 1, "KOTAK NRE Bank", "Canara Rebeko Investment Folio", 1, true, "CANREB-001", "Long term wealth creation" }
                });

            migrationBuilder.InsertData(
                table: "TMutualFunds",
                columns: new[] { "Id", "AmcId", "ISIN", "MFCategoryId", "OperationalStatusId", "SchemeCode", "SchemeName" },
                values: new object[,]
                {
                    { 1, 4, "INF846K01K35", 2, 1, "125354", "AXIS SMALL CAP Fund - DIRECT PLAN - GROWTH" },
                    { 2, 9, "INF194KB1AJ8", 2, 1, "147944", "BANDHAN SMALL CAP FUND - REGULAR PLAN GROWTH" },
                    { 3, 8, "INF760K01167", 2, 1, "102920", "CANARA ROBECO LARGE AND MID CAP FUND - REGULAR PLAN - GROWTH" },
                    { 4, 8, "INF760K01EI4", 2, 1, "118278", "CANARA ROBECO LARGE AND MID CAP FUND - DIRECT PLAN - GROWTH" },
                    { 5, 8, "INF760K01JC6", 2, 1, "146130", "CANARA ROBECO SMALL CAP FUND - DIRECT PLAN - GROWTH" },
                    { 6, 8, "INF760K01JW4", 2, 1, "149085", "CANARA ROBECO VALUE FUND - DIRECT PLAN - GROWTH" },
                    { 7, 5, "INF174K01LS2", 2, 1, "120166", "KOTAK FLEXICAP FUND - DIRECT PLAN - GROWTH" },
                    { 8, 5, "INF174K01LF9", 2, 1, "120158", "KOTAK LARGE & MIDCAP FUND - DIRECT PLAN - GROWTH" },
                    { 9, 7, "INF769K01BI1", 2, 1, "118834", "MIRAE ASSET LARGE & MIDCAP FUND - DIRECT PLAN - GROWTH" },
                    { 10, 7, "INF769K01HP3", 2, 1, "149169", "MIRAE ASSET S&P 500 Top 50 ETF" },
                    { 11, 7, "INF769K01HF4", 2, 1, "148927", "MIRAE ASSET NYSE FANG + ETF" },
                    { 12, 7, "INF769K01DM9", 2, 1, "135781", "MIRAE ASSET ELSS Tax Saver FUND - DIRECT PLAN - GROWTH" },
                    { 13, 10, "INF204K01K15", 2, 1, "118778", "NIPPON INDIA SMALL CAP FUND - Direct Plan Growth Plan - Growth Option" },
                    { 14, 6, "INF879O01266", 2, 1, "152468", "PARAG PARIKH DYNAMIC ASSET ALLOCATION FUND - DIRECT PLAN - GROWTH" },
                    { 15, 6, "INF879O01027", 1, 1, "122639", "PARAG PARIKH FLEXI CAP FUND- DIRECT PLAN - GROWTH" },
                    { 16, 3, "INF200K01QX4", 2, 1, "119598", "SBI LARGE Cap FUND - DIRECT PLAN - GROWTH" },
                    { 17, 3, "INF200K01T51", 2, 1, "125497", "SBI SMALL Cap FUND - DIRECT PLAN - GROWTH" },
                    { 18, 11, "INF277K01QO1", 2, 1, "119251", "TATA RETIREMENT SAVINGS FUND - PROGRESSIVE Plan - DIRECT PLAN - GROWTH" },
                    { 19, 11, "INF277K01PK1", 2, 1, "119287", "TATA S&P BSE SENSEX Index FUND - DIRECT PLAN" },
                    { 20, 11, "x", 2, 2, "0", "TEST ME FUND - DIRECT PLAN" }
                });

            migrationBuilder.InsertData(
                table: "TFundTransaction",
                columns: new[] { "FundTransactionId", "Amount", "Charges", "ConsumedBuyTransactionId", "FolioId", "FundId", "Nav", "Remarks", "SellGroupId", "TransactionDate", "Type", "Units", "UnitsLeft" },
                values: new object[] { 1, 12050.00m, 10.50m, null, 1, 1, 120.50m, "Initial purchase", null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100.000000m, 60.000000m });

            migrationBuilder.InsertData(
                table: "TNavHistory",
                columns: new[] { "Id", "MutualFundId", "NavDate", "NavValue" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 125.4321m },
                    { 2, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 126.1000m }
                });

            migrationBuilder.InsertData(
                table: "TFundTransaction",
                columns: new[] { "FundTransactionId", "Amount", "Charges", "ConsumedBuyTransactionId", "FolioId", "FundId", "Nav", "Remarks", "SellGroupId", "TransactionDate", "Type", "Units", "UnitsLeft" },
                values: new object[] { 2, 6030.00m, 5.25m, 1, 1, 1, 150.75m, "Partial profit booking", new Guid("3e9f2c0d-5e0a-4f1c-9f8b-9c9b3b7f0a11"), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 40.000000m, null });

            migrationBuilder.CreateIndex(
                name: "IX_TAMC_Code",
                table: "TAMC",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TFolio_AmcId",
                table: "TFolio",
                column: "AmcId");

            migrationBuilder.CreateIndex(
                name: "IX_TFolio_FolioHolderId",
                table: "TFolio",
                column: "FolioHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_TFolio_FolioName",
                table: "TFolio",
                column: "FolioName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TFundTransaction_ConsumedBuyTransactionId",
                table: "TFundTransaction",
                column: "ConsumedBuyTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TFundTransaction_FolioId",
                table: "TFundTransaction",
                column: "FolioId");

            migrationBuilder.CreateIndex(
                name: "IX_TFundTransaction_FundId",
                table: "TFundTransaction",
                column: "FundId");

            migrationBuilder.CreateIndex(
                name: "IX_TFundTransaction_SellGroupId",
                table: "TFundTransaction",
                column: "SellGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TMFCategory_CategoryName",
                table: "TMFCategory",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TMutualFunds_AmcId",
                table: "TMutualFunds",
                column: "AmcId");

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
                name: "IX_TMutualFunds_OperationalStatusId",
                table: "TMutualFunds",
                column: "OperationalStatusId");

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
                name: "TFundTransaction");

            migrationBuilder.DropTable(
                name: "TNavHistory");

            migrationBuilder.DropTable(
                name: "TSpecialEvents");

            migrationBuilder.DropTable(
                name: "TFolio");

            migrationBuilder.DropTable(
                name: "TMutualFunds");

            migrationBuilder.DropTable(
                name: "TFolioHolder");

            migrationBuilder.DropTable(
                name: "TAMC");

            migrationBuilder.DropTable(
                name: "TMFCategory");

            migrationBuilder.DropTable(
                name: "TOperationalStatus");
        }
    }
}
