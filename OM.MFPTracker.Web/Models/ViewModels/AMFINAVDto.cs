namespace OM.MFPTracker.Web.Models.ViewModels
{
	public class AmfiNavRow
	{
		public string SchemeCode { get; set; } = "";
		public string? ISIN { get; set; }
		public string SchemeName { get; set; } = "";
		public decimal NavValue { get; set; }
		public DateTime NavDate { get; set; }
	}
}
