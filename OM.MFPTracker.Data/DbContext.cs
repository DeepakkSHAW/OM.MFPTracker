using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OM.MFPTracker.Data.Models;
using System;

namespace OM.MFPTracker.Data
{
	public class MFPTrackerDbContext : DbContext
	{
		public MFPTrackerDbContext(DbContextOptions<MFPTrackerDbContext> options) : base(options) { }

		//public DbSet<Portfolio> Portfolios => Set<Portfolio>();
		//public DbSet<PortfolioHolding> PortfolioHoldings => Set<PortfolioHolding>();

		public DbSet<MFCategory> MFCategories => Set<MFCategory>();
		public DbSet<MutualFund> MutualFunds => Set<MutualFund>();
		public DbSet<OperationalStatus> OperationalStatuses => Set<OperationalStatus>();
		public DbSet<NavHistory> NavHistories => Set<NavHistory>();
		public DbSet<SpecialEvent> SpecialEvents => Set<SpecialEvent>();
		public DbSet<Person> People => Set<Person>();
		public DbSet<Amc> Amcs => Set<Amc>();
		public DbSet<OperationalStatus> operationalStatuses => Set<OperationalStatus>();
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//            optionsBuilder
			//.UseLoggerFactory(GetLoggerFactory()) //* Encapsulated approach to enable logging, when consumer does have exposed database context *//
			//.UseSqlite("", options => options.MaxBatchSize(30));

			//IConfiguration configuration = new ConfigurationBuilder()
			//    .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
			//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			//    .Build();

			//var dbConnection = configuration.GetConnectionString("DefaultConnection");
			//System.Diagnostics.Debug.WriteLine(dbConnection);

			//optionsBuilder.UseSqlite("", options => options.MaxBatchSize(512));
			//optionsBuilder.EnableSensitiveDataLogging();

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Dummy>(entity =>
			{
				entity.ToTable("DummyTable");

				entity.HasKey(p => p.Id);
				entity.Property(p => p.FirstName).IsRequired().UseCollation("NOCASE").HasMaxLength(100);
				entity.Property(p => p.InDate).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
				//.ValueGeneratedOnAddOrUpdate();
				entity.Property(p => p.updateDate).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();
			});
			modelBuilder.Entity<Dummy>().HasData(
				new Dummy() { Id = 1, FirstName = "DK" },
				new Dummy() { Id = 2, FirstName = "RS" }
				);

			modelBuilder.Entity<Person>(entity =>
			{
				entity.ToTable("People");

				entity.HasKey(p => p.Id);                 // Set key for entity
				entity.Property(p => p.FirstName)
					  .IsRequired()
					  .HasMaxLength(100);
				entity.Property(p => p.LastName)
					  .IsRequired()
					  .HasMaxLength(100);
				entity.Property(p => p.Signature)
					  .IsRequired()
					  .HasMaxLength(5);
				entity.Property(p => p.InDate)
				  .HasDefaultValueSql("CURRENT_TIMESTAMP")
				  .ValueGeneratedOnAdd();
				entity.Property(p => p.UpdateDate)
					.HasDefaultValueSql("CURRENT_TIMESTAMP")
					.ValueGeneratedOnAddOrUpdate();
			});
			modelBuilder.Entity<Person>().HasData(
				new Person() { Id = 1, FirstName = "Rupam", LastName = "Shaw", DateOfBirth = DateTime.Parse("1/1/2002"), Signature = "RS" },
				new Person() { Id = 2, FirstName = "Deepak", LastName = "Shaw", DateOfBirth = DateTime.Parse("10/12/1981"), Signature = "DK" },
				new Person() { Id = 3, FirstName = "Jagruti", LastName = "Shaw", DateOfBirth = DateTime.Parse("21/04/1974"), Signature = "JS" },
				new Person() { Id = 4, FirstName = "Divyam", LastName = "Shaw", DateOfBirth = DateTime.Parse("11/11/2001"), Signature = "DS" },
				new Person() { Id = 5, FirstName = "Durga Prasad", LastName = "Shaw", Signature = "DP" },
				new Person() { Id = 6, FirstName = "Radha", LastName = "Shaw", Signature = "RD" }
				);

			modelBuilder.Entity<MFCategory>(entity =>
			{
				entity.ToTable("TMFCategory");

				entity.HasKey(p => p.Id);                 // Set key for entity
				entity.Property(p => p.CategoryName)
					  .IsRequired()
					  .UseCollation("NOCASE").HasMaxLength(50);

				entity.HasIndex(c => c.CategoryName).IsUnique();
			});
			modelBuilder.Entity<MFCategory>().HasData(
			   new MFCategory() { Id = 1, CategoryName = "E-Multi Cap Mutual Funds" },
			   new MFCategory() { Id = 2, CategoryName = "E-Flexi Cap Mutual Funds" },
			   new MFCategory() { Id = 3, CategoryName = "E-Large & MidCap Mutual Funds" },
			   new MFCategory() { Id = 4, CategoryName = "E-Large Cap Mutual Funds" },
			   new MFCategory() { Id = 5, CategoryName = "E-Mid Cap Mutual Funds" },
			   new MFCategory() { Id = 6, CategoryName = "E-ELSS Mutual Funds" },
			   new MFCategory() { Id = 7, CategoryName = "E-Dividend Yield Mutual Funds" },
			   new MFCategory() { Id = 8, CategoryName = "E-Contra Mutual Funds" },
			   new MFCategory() { Id = 9, CategoryName = "E-Sectoral Mutual Funds" },
			   new MFCategory() { Id = 10, CategoryName = "E-Value Oriented Mutual Funds" }
			   );

			modelBuilder.Entity<Amc>(entity =>
			{
				entity.ToTable("TAMC");

				entity.HasKey(p => p.Id);                 // Set key for entity
				entity.Property(p => p.Name)
					  .IsRequired()
					  .UseCollation("NOCASE").HasMaxLength(120);
				entity.Property(p => p.Code)
					.IsRequired()
					.UseCollation("NOCASE").HasMaxLength(20);
				entity.HasIndex(c => c.Code).IsUnique();
			});
			modelBuilder.Entity<Amc>().HasData(
				new Amc { Id = 1, Name = "HDFC Asset Management Company", Code = "HDFC" },
				new Amc { Id = 2, Name = "ICICI Prudential Asset Management", Code = "ICICI" },
				new Amc { Id = 3, Name = "SBI Funds Management", Code = "SBI" },
				new Amc { Id = 4, Name = "Axis Asset Management", Code = "AXIS" },
				new Amc { Id = 5, Name = "Kotak Mahindra Asset Management", Code = "KOTAK" },
				new Amc { Id = 6, Name = "PPFAS Mutual Fund", Code = "PPFAS" },
				new Amc { Id = 7, Name = "Mirae Asset Mutual Fund", Code = "MAMF" },
				new Amc { Id = 8, Name = "CANARA ROBECO Mutual Fund", Code = "CRAMC" },
				new Amc { Id = 9, Name = "BANDHAN Mutual Fund", Code = "BANDHAN MF" },
				new Amc { Id = 10, Name = "Nippon India Mutual Fund", Code = "NIMF" },
				new Amc { Id = 11, Name = "Tata Asset Management", Code = "TMF" }
			);
			modelBuilder.Entity<MutualFund>(entity =>
			{
				entity.ToTable("TMutualFunds");

				entity.HasKey(p => p.Id);                 // Set key for entity
				entity.Property(p => p.SchemeCode)
					  .IsRequired().UseCollation("NOCASE").HasMaxLength(20);
				entity.Property(p => p.ISIN)
					  .IsRequired().UseCollation("NOCASE").HasMaxLength(20);
				entity.Property(p => p.SchemeName)
					  .IsRequired()
					  .HasMaxLength(100);
				//entity.Property(p => p.Signature)
				//	  .IsRequired()
				//	  .HasMaxLength(5);
				entity.Property(p => p.InDate)
				  .HasDefaultValueSql("CURRENT_TIMESTAMP")
				  .ValueGeneratedOnAdd();

				// Unique
				entity.HasIndex(f => f.SchemeCode).IsUnique();
				entity.HasIndex(f => f.ISIN).IsUnique();

				// Relationship with Mutual Fund Category 
				entity.HasOne(f => f.MFCategory)
					  .WithMany(c => c.MutualFunds)
					  .HasForeignKey(f => f.MFCategoryId)
					  .OnDelete(DeleteBehavior.Restrict);
				// Relationship with OperationalStatus
				entity.HasOne(m => m.OperationalStatus)
					.WithMany()
					.HasForeignKey(m => m.OperationalStatusId)
					.OnDelete(DeleteBehavior.Restrict);
				// Relationship with AMC
				entity.HasOne(m => m.Amc)
					.WithMany(a => a.MutualFunds)
					.HasForeignKey(m => m.AmcId)
					.OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<MutualFund>().HasData(
				new MutualFund() { Id = 1, ISIN = "INF846K01K35", SchemeCode = "125354", MFCategoryId = 2, AmcId =4, SchemeName = "AXIS SMALL CAP Fund - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 2, ISIN = "INF194KB1AJ8", SchemeCode = "147944", MFCategoryId = 2, AmcId = 9, SchemeName = "BANDHAN SMALL CAP FUND - REGULAR PLAN GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 3, ISIN = "INF760K01167", SchemeCode = "102920", MFCategoryId = 2, AmcId = 8, SchemeName = "CANARA ROBECO LARGE AND MID CAP FUND - REGULAR PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 4, ISIN = "INF760K01EI4", SchemeCode = "118278", MFCategoryId = 2, AmcId = 8, SchemeName = "CANARA ROBECO LARGE AND MID CAP FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 5, ISIN = "INF760K01JC6", SchemeCode = "146130", MFCategoryId = 2, AmcId = 8, SchemeName = "CANARA ROBECO SMALL CAP FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 6, ISIN = "INF760K01JW4", SchemeCode = "149085", MFCategoryId = 2, AmcId = 8, SchemeName = "CANARA ROBECO VALUE FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 7, ISIN = "INF174K01LS2", SchemeCode = "120166", MFCategoryId = 2, AmcId = 5, SchemeName = "KOTAK FLEXICAP FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 8, ISIN = "INF174K01LF9", SchemeCode = "120158", MFCategoryId = 2, AmcId = 5, SchemeName = "KOTAK LARGE & MIDCAP FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 9, ISIN = "INF769K01BI1", SchemeCode = "118834", MFCategoryId = 2, AmcId = 7, SchemeName = "MIRAE ASSET LARGE & MIDCAP FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 10, ISIN = "INF769K01HP3", SchemeCode = "149169", MFCategoryId = 2, AmcId = 7, SchemeName = "MIRAE ASSET S&P 500 Top 50 ETF", OperationalStatusId = 1 },
				new MutualFund() { Id = 11, ISIN = "INF769K01HF4", SchemeCode = "148927", MFCategoryId = 2, AmcId = 7, SchemeName = "MIRAE ASSET NYSE FANG + ETF", OperationalStatusId = 1 },
				new MutualFund() { Id = 12, ISIN = "INF769K01DM9", SchemeCode = "135781", MFCategoryId = 2, AmcId = 7, SchemeName = "MIRAE ASSET ELSS Tax Saver FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 13, ISIN = "INF204K01K15", SchemeCode = "118778", MFCategoryId = 2, AmcId = 10, SchemeName = "NIPPON INDIA SMALL CAP FUND - Direct Plan Growth Plan - Growth Option", OperationalStatusId = 1 },
				new MutualFund() { Id = 14, ISIN = "INF879O01266", SchemeCode = "152468", MFCategoryId = 2, AmcId = 6, SchemeName = "PARAG PARIKH DYNAMIC ASSET ALLOCATION FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 15, ISIN = "INF879O01027", SchemeCode = "122639", MFCategoryId = 1, AmcId = 6, SchemeName = "PARAG PARIKH FLEXI CAP FUND- DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 16, ISIN = "INF200K01QX4", SchemeCode = "119598", MFCategoryId = 2, AmcId = 3, SchemeName = "SBI LARGE Cap FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 17, ISIN = "INF200K01T51", SchemeCode = "125497", MFCategoryId = 2, AmcId = 3, SchemeName = "SBI SMALL Cap FUND - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 18, ISIN = "INF277K01QO1", SchemeCode = "119251", MFCategoryId = 2, AmcId = 11, SchemeName = "TATA RETIREMENT SAVINGS FUND - PROGRESSIVE Plan - DIRECT PLAN - GROWTH", OperationalStatusId = 1 },
				new MutualFund() { Id = 19, ISIN = "INF277K01PK1", SchemeCode = "119287", MFCategoryId = 2, AmcId = 11, SchemeName = "TATA S&P BSE SENSEX Index FUND - DIRECT PLAN", OperationalStatusId = 1 },
				new MutualFund() { Id = 20, ISIN = "x", SchemeCode = "0", MFCategoryId = 2, AmcId = 11, SchemeName = "TEST ME FUND - DIRECT PLAN", OperationalStatusId = 2 }
				
				);

			modelBuilder.Entity<OperationalStatus>(entity =>
			{
				entity.ToTable("TOperationalStatus");
				// Primary key
				entity.HasKey(e => e.Id);
				// Required properties with max length 50
				entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
				// Optional description with max length 200
				entity.Property(e => e.Description).HasMaxLength(200);
				entity.HasData(
					new OperationalStatus
					{
						Id = 1,
						Code = "ACTIVE",
						Description = "Open to new investors with continuous subscriptions and redemption",
						IsTransactionAllowed = true,
						IsNavAllowed = true

					},
					new OperationalStatus
					{
						Id = 2,
						Code = "CLOSED",
						Description = "Units available only during NFO with fixed maturity",
						IsTransactionAllowed = false,
						IsNavAllowed = false
					},
					new OperationalStatus
					{
						Id = 3,
						Code = "SUSPENDED",
						Description = "Temporarily halted due to regulatory or market conditions",
						IsTransactionAllowed = false,
						IsNavAllowed = false
					},
					new OperationalStatus
					{
						Id = 4,
						Code = "CLOSED_NEW",
						Description = "Not accepting new investments",
						IsTransactionAllowed = false,
						IsNavAllowed = false
					},
					new OperationalStatus
					{
						Id = 5,
						Code = "LIQUIDATED",
						Description = "Fund closed and assets paid out or merged",
						IsTransactionAllowed = false,
						IsNavAllowed = false
					}
					);
			});
			modelBuilder.Entity<NavHistory>(entity =>
		{
			entity.ToTable("TNavHistory");

			entity.HasKey(n => n.Id);
			entity.Property(n => n.NavValue)
				  .HasPrecision(18, 4)
				  .IsRequired();
			entity.Property(n => n.InDate)
				  .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
			entity.HasOne(n => n.MutualFund)
				  .WithMany(f => f.NavHistories)
				  .HasForeignKey(n => n.MutualFundId)
				  .OnDelete(DeleteBehavior.Cascade);

			// One NAV per fund per day
			entity.HasIndex(n => new { n.MutualFundId, n.NavDate })
				  .IsUnique();
		});
			modelBuilder.Entity<NavHistory>().HasData(
			// NAVs for MutualFund Id = 20 // TEST ME FUND - DIRECT PLAN
			new NavHistory { Id = 1, MutualFundId = 1, NavDate = new DateTime(2024, 01, 01), NavValue = 125.4321m },
			new NavHistory { Id = 2, MutualFundId = 1, NavDate = new DateTime(2024, 01, 02), NavValue = 126.1000m }
		);

			modelBuilder.Entity<SpecialEvent>(entity =>
		{
			entity.ToTable("TSpecialEvents");

			// Primary key
			entity.HasKey(e => e.Id);

			// Required properties
			entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

			entity.Property(e => e.EventDate).IsRequired();

			entity.Property(e => e.Notes).HasMaxLength(500);

			// Enum: store as string for readability
			entity.Property(e => e.EventType).HasConversion<string>().HasMaxLength(50).IsRequired();

			// Index on EventDate for faster lookup / filter
			entity.HasIndex(e => e.EventDate);
			// Seed sample data
			entity.HasData(
				new SpecialEvent { Id = 1, EventDate = new DateTime(2023, 1, 10), Title = "Global Energy Crisis & Commodity Price Shock", EventType = EventType.GlobalMacro, Notes = "High inflation, pressure on Indian equities and rupee due to elevated energy prices" },
				new SpecialEvent { Id = 2, EventDate = new DateTime(2023, 6, 15), Title = "El Niño Impact on Global Agriculture", EventType = EventType.Climate, Notes = "Concerns over food inflation and rural demand in India" },
				new SpecialEvent { Id = 3, EventDate = new DateTime(2023, 1, 25), Title = "Adani Group Short Seller Report Fallout", EventType = EventType.Corporate, Notes = "Sharp selloff in Adani stocks, dragged broader Indian indices" },
				new SpecialEvent { Id = 4, EventDate = new DateTime(2023, 1, 27), Title = "India Implements T+1 Settlement", EventType = EventType.Regulatory, Notes = "Structural reform improving liquidity and settlement efficiency" },
				new SpecialEvent { Id = 5, EventDate = new DateTime(2024, 6, 4), Title = "India General Election Results 2024", EventType = EventType.Elections, Notes = "High volatility in Sensex and Nifty based on election outcome" },
				new SpecialEvent { Id = 6, EventDate = new DateTime(2024, 12, 26), Title = "India Becomes World's 4th Largest Stock Market", EventType = EventType.MarketMilestone, Notes = "Positive sentiment and global visibility for Indian equities" },
				new SpecialEvent { Id = 7, EventDate = new DateTime(2024, 3, 20), Title = "US Federal Reserve Rate Outlook Shift", EventType = EventType.MonetaryPolicy, Notes = "Impacted FII flows and emerging market valuations including India" },
				new SpecialEvent { Id = 8, EventDate = new DateTime(2024, 5, 10), Title = "US-China Trade & Tech Tensions Escalate", EventType = EventType.Geopolitics, Notes = "Global risk-off sentiment affecting export-oriented Indian stocks" },
				new SpecialEvent { Id = 9, EventDate = new DateTime(2025, 4, 3), Title = "Global Stock Market Selloff 2025", EventType = EventType.GlobalMacro, Notes = "Sharp correction across global equities including Indian markets" },
				new SpecialEvent { Id = 10, EventDate = new DateTime(2025, 8, 27), Title = "US Imposes 50% Tariff on Indian Goods", EventType = EventType.TradePolicy, Notes = "Negative impact on rupee and export-focused Indian stocks" },
				new SpecialEvent { Id = 11, EventDate = new DateTime(2025, 6, 13), Title = "Middle East Conflict Escalation (Israel–Iran)", EventType = EventType.Geopolitics, Notes = "Oil price spike led to inflation concerns in India" },
				new SpecialEvent { Id = 12, EventDate = new DateTime(2025, 3, 22), Title = "Turkey Currency Crisis & EM Selloff", EventType = EventType.GlobalMacro, Notes = "Spillover risk aversion affecting emerging markets including India" },
				new SpecialEvent { Id = 13, EventDate = new DateTime(2025, 5, 7), Title = "India-Pakistan Border Tensions (Pahalgam Incident)", EventType = EventType.Geopolitics, Notes = "Short-term volatility in Indian indices and defence stocks" },
				new SpecialEvent { Id = 14, EventDate = new DateTime(2025, 2, 15), Title = "Indian Market Correction Early 2025", EventType = EventType.MarketCorrection, Notes = "FII outflows and global risk aversion caused broad market decline" },
				new SpecialEvent { Id = 15, EventDate = new DateTime(2025, 9, 20), Title = "US Fed Signals Prolonged Higher Rates", EventType = EventType.MonetaryPolicy, Notes = "Pressure on valuations and capital flows to India" },
				new SpecialEvent { Id = 16, EventDate = new DateTime(2025, 8, 30), Title = "India GDP Growth Beats Expectations", EventType = EventType.EconomicData, Notes = "Boosted confidence in Indian equities and domestic sectors" },
				new SpecialEvent { Id = 17, EventDate = new DateTime(2026, 2, 3), Title = "India-US Trade Deal Announcement", EventType = EventType.TradePolicy, Notes = "Positive rally in Indian markets and strengthening of rupee" },
				new SpecialEvent { Id = 18, EventDate = new DateTime(2026, 2, 4), Title = "Global AI & Tech Stock Selloff", EventType = EventType.Technology, Notes = "Valuation reset in IT and tech stocks including Indian IT sector" },
				new SpecialEvent { Id = 19, EventDate = new DateTime(2024, 11, 15), Title = "Russia-Ukraine War Commodity Impact", EventType = EventType.Geopolitics, Notes = "Energy and fertilizer price volatility affecting Indian inflation" },
				new SpecialEvent { Id = 20, EventDate = new DateTime(2024, 7, 15), Title = "Global Elections & Policy Uncertainty (US/EU)", EventType = EventType.GlobalMacro, Notes = "Increased volatility and cautious FII positioning in Indian markets" }
			);
		});


			//modelBuilder.Entity<PortfolioHolding>()
			//    .HasIndex(x => new { x.PortfolioId, x.MutualFundId })
			//    .IsUnique();
		}
	}
}
