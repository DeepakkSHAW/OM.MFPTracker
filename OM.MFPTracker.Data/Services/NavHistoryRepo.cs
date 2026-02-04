using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
	public interface INavHistoryRepo
	{
		Task<List<NavHistory>> GetByFundAsync(int mutualFundId);
		Task<NavHistory?> GetAsync(int mutualFundId, DateTime navDate);

		Task AddAsync(NavHistory nav);
		Task DeleteAsync(int id);
	}
	public class NavHistoryRepo : INavHistoryRepo
	{
		private readonly MFPTrackerDbContext _db;

		public NavHistoryRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}

		public async Task<List<NavHistory>> GetByFundAsync(int mutualFundId)
		{
			return await _db.NavHistories
				.Where(n => n.MutualFundId == mutualFundId)
				.OrderByDescending(n => n.NavDate)
				.ToListAsync();
		}

		public async Task<NavHistory?> GetAsync(int mutualFundId, DateTime navDate)
		{
			return await _db.NavHistories
				.FirstOrDefaultAsync(n =>
					n.MutualFundId == mutualFundId &&
					n.NavDate == navDate.Date);
		}

		public async Task AddAsync(NavHistory nav)
		{
			_db.NavHistories.Add(nav);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _db.NavHistories.FindAsync(id);
			if (entity == null)
				return;

			_db.NavHistories.Remove(entity);
			await _db.SaveChangesAsync();
		}
	}

}
