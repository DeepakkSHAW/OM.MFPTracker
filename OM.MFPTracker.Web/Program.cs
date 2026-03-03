using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data;
using OM.MFPTracker.Data.Services;
using OM.MFPTracker.Web.Components;
using OM.MFPTracker.Web.Options;
using OM.MFPTracker.Web.Services;
using System;

namespace OM.MFPTracker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /////////////
            var dbFolder = builder.Configuration["Database:Folder"] ?? "data";
            var dbFile = builder.Configuration["Database:FileName"] ?? "portfolio.db";

            var dbPath = Path.Combine(builder.Environment.ContentRootPath, dbFolder, dbFile);
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

			builder.Services.Configure<AmfiOptions>(builder.Configuration.GetSection(AmfiOptions.SectionName));
			builder.Services.AddHttpClient<AmfiNavService>();

			// Register DbContext
			builder.Services.AddDbContext<MFPTrackerDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));
            
            // Register repository
            builder.Services.AddScoped<IFolioHolder, FolioHolderRepo>();
			builder.Services.AddScoped<IMFCategoryRepo, MFCategoryRepo>();
			builder.Services.AddScoped<IMutualFundRepo, MutualFundRepo>();
			builder.Services.AddScoped<INavHistoryRepo, NavHistoryRepo>();
			builder.Services.AddScoped<ISpecialEventRepo, SpecialEventRepo>();
			builder.Services.AddScoped<IAmcRepo, AmcRepo>();
			builder.Services.AddScoped<IOperationalStatusRepo, OperationalStatusRepo>();
			builder.Services.AddScoped<IFolioRepo, FolioRepo>();
            builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
			builder.Services.AddScoped<IBuyTransactionRepo, BuyTransactionRepo>();
            builder.Services.AddScoped<IMFTransactionRepo, MFTransactionRepo>();
			// Add services to the container.
			builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
