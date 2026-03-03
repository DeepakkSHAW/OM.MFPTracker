using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OM.MFPTracker.Data.Models
{
	public enum TransactionType
	{
		Buy = 1,
		Sell = 2,
		Dividend = 3,
		SwitchIn = 4,
		SwitchOut = 5
	}
	public enum EventType
	{
		[Display(Name = "Major Event")] MajorEvent,             // High market impact, e.g., Budget, RBI policy, Election results
		[Display(Name = "Minor Event")] MinorEvent,             // Low market impact, informational, e.g., holiday announcement
		[Display(Name = "Regulatory")] Regulatory,              // Compliance or law-related events, e.g., SEBI/RBI circulars, T+1 settlement
		[Display(Name = "Macro Economic")] Macroeconomic,       // Domestic macro indicators: GDP, inflation (CPI/WPI), IIP, PMI, employment
		[Display(Name = "Monetary Policy")] MonetaryPolicy,     // RBI/Fed/ECB rate decisions, policy statements, liquidity measures
		[Display(Name = "Elections")] Elections,                // Indian or global elections impacting market sentiment & volatility
		[Display(Name = "Corporate Action")] CorporateAction,   // Dividends, splits, mergers, buybacks, corporate restructurings
		[Display(Name = "Earnings Release")] EarningsRelease,   // Quarterly/annual financial results of companies
		[Display(Name = "Global Event")] GlobalEvent,           // Major world events not directly economic, e.g., pandemics, global crises
		[Display(Name = "Global Macro")] GlobalMacro,           // Global economic shocks, commodity shocks, EM risk-off, inflation, recession fears
		[Display(Name = "Trade Policy")] TradePolicy,           // Tariffs, free trade agreements, import/export bans, India–US/China trade events
		[Display(Name = "Market Milestone")] MarketMilestone,   // Sensex/Nifty record highs, market-cap milestones, structural reforms
		[Display(Name = "Market Correction")] MarketCorrection, // Crashes, sharp drawdowns, volatility spikes
		[Display(Name = "Technology")] Technology,              // AI disruptions, IT sector shocks, tech-led rallies or sell-offs
		[Display(Name = "Climate Event")] ClimateEvent,         // El Niño, floods, droughts, agricultural shocks affecting prices
		[Display(Name = "Geopolitics")] Geopolitics,            // Wars, border tensions, regional conflicts affecting risk sentiment
		[Display(Name = "Economic Data")] EconomicData,         // Key data releases like GDP, trade balance, fiscal deficit, industrial production
		[Display(Name = "Corporate")] Corporate,                // Major corporate events not captured by earnings/dividends, e.g., debt crisis, default, large M&A
		[Display(Name = "Climate")] Climate                     // Short-term climate/energy shocks (hurricanes, floods, droughts) affecting markets
	}
	public class Dummy
	{
		public int Id { get; set; }          // Primary Key

		[Required(ErrorMessage = "First Name is Required")]
		[MaxLength(100)]
		public string FirstName { get; set; } = null!;
		public DateTime InDate { get; set; }
		public DateTime updateDate { get; set; }
	}
	
	public class FolioHolder
	{
		[Key]
		public int Id { get; set; }          // Primary Key

		[Required(ErrorMessage = "First Name is Required")]
		[MaxLength(100)]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "last Name is Required")]
		[StringLength(100, MinimumLength = 4, ErrorMessage = "Last name should be between 100 to 3 characters long")]
		public string LastName { get; set; } = null!;

		[DataType(DataType.Date)]
		[DateInPastAttribute(ErrorMessage = "Date of Birth must be in the past")]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Signature is Required")]
		[StringLength(5, MinimumLength = 2, ErrorMessage = "Signature should be between 5 to 2 characters long")]
		public string Signature { get; set; } = null!;
		public DateTime InDate { get; set; }//only for audit purposes
		public DateTime UpdateDate { get; set; }//only for audit purposes

		public ICollection<Folio> Folios { get; set; } = new List<Folio>();
	}
	
	public class MFCategory
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Category Name is Required")]
		[StringLength(50, MinimumLength = 8, ErrorMessage = "Category Name should be between 50 to 8 characters long")]
		public string CategoryName { get; set; } = null!;

		// Navigation
		public ICollection<MutualFund> MutualFunds { get; set; } = new List<MutualFund>();
	}
	public class Amc
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "AMC Name is required")]
		[StringLength(120, MinimumLength = 2, ErrorMessage = "AMC Name must be between 2 and 120 characters")]
		public string Name { get; set; } = null!;

		[MaxLength(20)]
		public string? Code { get; set; }   // Optional: e.g. HDFC, ICICI, SBI

		// Navigation
		public ICollection<MutualFund> MutualFunds { get; set; } = new List<MutualFund>();
		public ICollection<Folio> Folios { get; set; } = new List<Folio>();

		[NotMapped]
		public int FundCount { get; set; }
	}
	public class MutualFund
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Scheme Code is Required")]
		[MaxLength(20)]
		public string SchemeCode { get; set; } = null!;

		[Required(ErrorMessage = "ISIN Code is Required")]
		[MaxLength(20)]
		public string ISIN { get; set; } = null!;

		[Required(ErrorMessage = "Scheme Name is Required")]
		[MaxLength(100)]
		public string SchemeName { get; set; } = null!;
		
		// ==========================
		// 🔹 Foreign Keys
		// ==========================
		public int AmcId { get; set; }
		public Amc Amc { get; set; } = null!;
		public int MFCategoryId { get; set; }

		public MFCategory MFCategory { get; set; } = null!;



		[Range(1, int.MaxValue, ErrorMessage = "Operational Status is required")]
		public int OperationalStatusId { get; set; }
		public OperationalStatus OperationalStatus { get; set; } = null!;

		// ==========================
		// 🔹 Navigation Collections
		// ==========================
		public ICollection<NavHistory> NavHistories { get; set; } = new List<NavHistory>();
		public ICollection<FundTransaction> Transactions { get; set; } = new List<FundTransaction>();
		public DateTime InDate { get; set; }//only for audit purposes
	}

	public class OperationalStatus
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Code  is Required")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Code should be between 5 to 50 characters long")]

		public string Code { get; set; } = null!;
		// e.g. ACTIVE, CLOSED, SUSPENDED

		//public string Name { get; set; } = null!;
		//// e.g. Active / Open-ended

		[StringLength(200, MinimumLength = 5, ErrorMessage = "Description should be between 5 to 200 characters long")]

		public string Description { get; set; } = null!;

		public bool IsTransactionAllowed { get; set; }
		public bool IsNavAllowed { get; set; }
	}

	//public class Portfolio
	//{
	//    public int Id { get; set; }

	//    public string Name { get; set; } = null!;
	//    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

	//    public ICollection<PortfolioHolding> Holdings { get; set; } = [];
	//}

	//public class PortfolioHolding
	//{
	//    public int Id { get; set; }

	//    public int PortfolioId { get; set; }
	//    public Portfolio Portfolio { get; set; } = null!;

	//    public int MutualFundId { get; set; }
	//    public MutualFund MutualFund { get; set; } = null!;

	//    public decimal Units { get; set; }
	//}

	public class NavHistory
	{
		[Key]
		public int Id { get; set; }

		public int MutualFundId { get; set; }
		public MutualFund MutualFund { get; set; } = null!;

		// NAV details
		[Required(ErrorMessage = "NAV Date is required")]
		[DataType(DataType.Date)]
		[DateInPastOrToday(ErrorMessage = "NAV date cannot be in the future")]
		public DateTime NavDate { get; set; }

		[Required(ErrorMessage = "NAV Value is required")]
		[Range(0.000001, double.MaxValue, ErrorMessage = "NAV must be greater than 0")]
		public decimal NavValue { get; set; }
		// Audit
		public DateTime InDate { get; set; }
	}
	
	public class SpecialEvent
	{
		public int Id { get; set; }

		[Required]
		public DateTime EventDate { get; set; }

		[Required(ErrorMessage = "Event Title  is Required")]
		[StringLength(200, MinimumLength = 5, ErrorMessage = "Title should be between 5 to 200 characters long")]
		public string Title { get; set; } = null!;

		[Required]
		public EventType EventType { get; set; }  // <- enum type

		[MaxLength(500)]
		public string? Notes { get; set; }
	}

	public class Folio
	{
		[Key]
		public int FolioId { get; set; }          // Primary Key
		[Required(ErrorMessage = "Folio Name (Number) is Required")]
		[MaxLength(30)]
		public string FolioName { get; set; } = null!;

		[StringLength(100, MinimumLength = 5, ErrorMessage = "Folio Description should be between 100 to 5 characters long")]
		public string? FolioDescription { get; set; } 

		[MaxLength(50, ErrorMessage = "Folio Purpose shouldn't be more than 50 characters long")]
		public string? FolioPurpose { get; set; }

		public bool FolioIsActive { get; set; } = true;

		[MaxLength(50, ErrorMessage = "Attached Bank to this Folio shouldn't be more than 50 characters long")]
		public string? AttachedBank { get; set; }

		// ==========================
		// 🔹 Foreign Keys
		// ==========================

		public int? AmcId { get; set; }
		public Amc Amc { get; set; } = null!;

		public int? FolioHolderId { get; set; }
		public FolioHolder FolioHolder { get; set; } = null!;

		public DateTime InDate { get; set; }//only for audit purposes
		public DateTime UpdateDate { get; set; }//only for audit purposes

		// ==========================
		// 🔹 Navigation Collections
		// ==========================
		public ICollection<FundTransaction> Transactions { get; set; } = new List<FundTransaction>();
	}

	public class FundTransaction
	{
		[Key]
		public int FundTransactionId { get; set; }
		[Required(ErrorMessage = "Transaction Type is mandatory")]
		public TransactionType Type { get; set; }
		[Required(ErrorMessage = "Transaction date is mandatory")]
		public DateTime TransactionDate { get; set; }
		[Required(ErrorMessage = "Units is mandatory and should be positive number only")]
		public decimal Units { get; set; }
		// Only for BUY
		public decimal? UnitsLeft { get; set; }
		[Required(ErrorMessage = "NAV is mandatory value")]
		public decimal Nav { get; set; }
		public decimal Amount { get; set; }

		public decimal? Charges { get; set; }
		[MaxLength(50, ErrorMessage = "Remarks shouldn't be more than 50 characters long")]
		public string? Remarks { get; set; }
		// Used to group split sells
		public Guid? SellGroupId { get; set; }

		// Optional: explicitly reference Buy lot
		public int? ConsumedBuyTransactionId { get; set; }

		public DateTime InDate { get; set; }//only for audit purposes
		public DateTime UpdateDate { get; set; }//only for audit purposes

		// ==========================
		// 🔹 Foreign Keys
		// ==========================
		public int FolioId { get; set; }
		public Folio Folio { get; set; }

		public int FundId { get; set; }
		public MutualFund Fund { get; set; }
	}

	//*******Flat structure Data Model*****//

	public class MutualFundTransaction
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Transaction Date should be given")]
		public DateTime Date { get; set; }                   // Trade/Transaction DateTime
		[Required, MaxLength(30, ErrorMessage = "Folio shouldn't be more than 30 characters long")]
		public string Folio { get; set; } = null!;

		[Required, MaxLength(100, ErrorMessage = "Fund Name shouldn't be more than 100 characters long")]
		public string FundName { get; set; } = null!;

		[MaxLength(20, ErrorMessage = "Fund Code shouldn't be more than 20 characters long")]
		public string? FundCode { get; set; }

		[MaxLength(50, ErrorMessage = "Fund Type shouldn't be more than 50 characters long")]
		public string? FundType { get; set; }                // e.g., "GR", "IDCW", "Multi Cap", etc.
		[Required(ErrorMessage = "Units is mandatory value")]
		public decimal Units { get; set; }                    // as requested (double)
		[Required(ErrorMessage = "NAV Value is required")]
		[Range(0.000001, double.MaxValue, ErrorMessage = "NAV must be greater than 0")]
		public decimal NAV { get; set; }                     // as requested (double)
		public decimal AmountPaid { get; set; }               // Calculated Value

		[MaxLength(120, ErrorMessage = "Fund Source (Bank name) shouldn't be more than 120 characters long")]
		public string? Source { get; set; }                  // Bank name or source

		[MaxLength(500, ErrorMessage = "Any notes Code shouldn't be more than 500 characters long")]
		public string? Note { get; set; }

		// Optional: audit
		public DateTime CreatedUtc { get; set; } 
		public DateTime UpdatedUtc { get; set; }
	}

}
