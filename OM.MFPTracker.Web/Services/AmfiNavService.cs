using OM.MFPTracker.Web.Models.ViewModels;
using System.Globalization;
namespace OM.MFPTracker.Web.Services
{
	public class AmfiNavService
	{
		private readonly HttpClient _http;
		private readonly IConfiguration _configuration;
		public AmfiNavService(IConfiguration configuration, HttpClient http)
		{
			_configuration = configuration;
			_http = http;
		}

		public async Task<List<AmfiNavRow>> GetLatestNavAsync()
		{
			//Read URL from appsettings.json
			var url = _configuration["Amfi:NavUrl"];
			if (string.IsNullOrWhiteSpace(url))
				throw new InvalidOperationException("AMFI NAV URL not configured in appsettings.json");

			//var content = await _http.GetStringAsync("https://portal.amfiindia.com/spages/NAVOpen.txt");
			var content = await _http.GetStringAsync(url);

			var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

			var result = new List<AmfiNavRow>();

			foreach (var line in lines.Skip(1))
			{
				if (line.StartsWith("-")) continue;

				var cols = line.Split(';');
				if (cols.Length < 6) continue;

				if (!decimal.TryParse(cols[4], out var nav)) continue;
				if (!DateTime.TryParseExact(cols[5].Trim(),
					"dd-MMM-yyyy",
					CultureInfo.InvariantCulture,
					DateTimeStyles.None,
					out var navDate))
					continue;

				result.Add(new AmfiNavRow
				{
					SchemeCode = cols[0].Trim(),
					ISIN = string.IsNullOrWhiteSpace(cols[1]) ? null : cols[1].Trim(),
					SchemeName = cols[3].Trim(),
					NavValue = nav,
					NavDate = navDate
				});
			}

			return result;
		}
	}
}
