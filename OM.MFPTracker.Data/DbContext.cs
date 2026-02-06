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
		public DbSet<NavHistory> NavHistories => Set<NavHistory>();
		public DbSet<SpecialEvent> SpecialEvents => Set<SpecialEvent>();
		public DbSet<Person> People => Set<Person>();
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

				// Uniques
				entity.HasIndex(f => f.SchemeCode).IsUnique();
				entity.HasIndex(f => f.ISIN).IsUnique();

				// Relationship
				entity.HasOne(f => f.MFCategory)
					  .WithMany(c => c.MutualFunds)
					  .HasForeignKey(f => f.MFCategoryId)
					  .OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<MutualFund>().HasData(
				new MutualFund() { Id = 1, ISIN = "123AA", SchemeCode = "B2C67", MFCategoryId = 2, SchemeName = "DK MF" },
				new MutualFund() { Id = 2, ISIN = "12B4C", SchemeCode = "AAC27", MFCategoryId = 1, SchemeName = "RK MF" },
				new MutualFund() { Id = 3, ISIN = "INF879O01027", SchemeCode = "122639", MFCategoryId = 1, SchemeName = "Parag Parikh Flexi Cap Fund - Direct Plan - Growth" },
				new MutualFund() { Id = 4, ISIN = "INF760K01167", SchemeCode = "102920", MFCategoryId = 2, SchemeName = "CANARA ROBECO LARGE AND MID CAP FUND - REGULAR PLAN - GROWTH" }
				);

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
				// NAVs for MutualFund Id = 1
				new NavHistory { Id = 1, MutualFundId = 1, NavDate = new DateTime(2024, 01, 01), NavValue = 125.4321m },
				new NavHistory { Id = 2, MutualFundId = 1, NavDate = new DateTime(2024, 01, 02), NavValue = 126.1000m },

				// NAVs for MutualFund Id = 2
				new NavHistory { Id = 3, MutualFundId = 2, NavDate = new DateTime(2024, 01, 01), NavValue = 98.7500m },
				new NavHistory { Id = 4, MutualFundId = 2, NavDate = new DateTime(2024, 01, 02), NavValue = 99.2500m }
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
