using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace OM.MFPTracker.Data.Models
{
    public class Dummy
    {
        public int Id { get; set; }          // Primary Key

        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;
        public DateTime InDate { get; set; }
        public DateTime updateDate { get; set; }
    }
    public class Person
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

    public class MutualFund
    {
        //AMC Name
        //Category

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

		//public string FundCode { get; set; } = null!;
		//      public string FundName { get; set; } = null!;

		//      public ICollection<NavHistory> NavHistories { get; set; } = [];
		// FK
		public int MFCategoryId { get; set; }

		// Navigation
		public MFCategory MFCategory { get; set; } = null!;

		// NAV history
		public ICollection<NavHistory> NavHistories { get; set; } = new List<NavHistory>();

		public DateTime InDate { get; set; }//only for audit purposes
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

}
