using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Models
{
	public class NavToInsert
	{
		public string SchemeCode { get; set; } = "";
		public decimal NavValue { get; set; }
		public DateTime NavDate { get; set; }
	}
	public class NavImportResult
	{
		/// <summary>
		/// Number of NAVs successfully inserted into the database
		/// </summary>
		public int Inserted { get; set; }

		/// <summary>
		/// Number of NAVs skipped because they already exist in the database
		/// </summary>
		public int SkippedDuplicate { get; set; }

		/// <summary>
		/// Number of NAVs skipped because the SchemeCode was not found in the database
		/// </summary>
		public int SkippedUnknownFund { get; set; }
	}
}
