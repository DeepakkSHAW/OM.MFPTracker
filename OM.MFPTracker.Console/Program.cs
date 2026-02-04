
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OM.MFPTracker.Data;
using OM.MFPTracker.Data.Models;
using OM.MFPTracker.Data.Services;

namespace OM.MFPTracker.Console
{
   
    internal class Program
    {
        private readonly static string title = "DK";
        static async Task Main(string[] args)
        {
            System.Console.WriteLine($"=========={title}================");

            var services = new ServiceCollection();
            var dbPath = Path.Combine(AppContext.BaseDirectory, "mfp.db");

            services.AddDbContext<MFPTrackerDbContext>(opt => opt.UseSqlite($"Data Source={dbPath}")); 
            //services.AddScoped<IPortfolioRepository, SqlitePortfolioRepository>();

            var provider = services.BuildServiceProvider();

            // Ensure DB exists (optional safety)
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MFPTrackerDbContext>();
            //db.Database.Migrate();

        }
    }
}
