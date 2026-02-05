namespace OM.MFPTracker.Web.Models.ViewModels
{
	public class NavCsvRowVm
	{
		public int RowNo { get; set; }
		public DateTime? NavDate { get; set; }
		public decimal? NavValue { get; set; }

		public bool IsValid { get; set; }
		public string? Error { get; set; }
	}
}
