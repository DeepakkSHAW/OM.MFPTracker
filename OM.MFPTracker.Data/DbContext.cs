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
				new MutualFund() { Id = 2, ISIN = "12B4C", SchemeCode = "AAC27", MFCategoryId = 1, SchemeName = "RK MF" }
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
					new SpecialEvent { Id = 1, EventDate = new DateTime(2026, 1, 9), Title = "Election in West Bengal", EventType = EventType.Elections, Notes = "No major impact on stock market" },
					new SpecialEvent { Id = 2, EventDate = new DateTime(2026, 2, 1), Title = "India Budget Date", EventType = EventType.MajorEvent, Notes = "Market expected to react to budget announcements" },
					new SpecialEvent { Id = 3, EventDate = new DateTime(2026, 3, 15), Title = "Quarterly Corporate Earnings Release", EventType = EventType.EarningsRelease, Notes = "Top 10 index companies releasing Q4 earnings" },
					new SpecialEvent { Id = 4, EventDate = new DateTime(2026, 4, 1), Title = "RBI Policy Meeting", EventType = EventType.Macroeconomic, Notes = "Expectations of interest rate decision" }
				);
			});


			//modelBuilder.Entity<PortfolioHolding>()
			//    .HasIndex(x => new { x.PortfolioId, x.MutualFundId })
			//    .IsUnique();
		}
	}
}
