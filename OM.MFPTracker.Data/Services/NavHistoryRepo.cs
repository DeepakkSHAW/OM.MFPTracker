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
		Task<NavHistory?> GetByIdAsync(int id);
		Task AddAsync(NavHistory nav);
		Task UpdateAsync(NavHistory nav);
		Task BulkInsertAsync(IEnumerable<NavHistory> navHistories);
		Task<List<NavHistory>> GetPagedByFundAsync(int mutualFundId, int pageNumber, int pageSize, bool orderByDescending = false);
		Task<int> GetCountByFundAsync(int mutualFundId);
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

		public async Task<NavHistory?> GetByIdAsync(int id)
		{
			return await _db.NavHistories
							.Include(n => n.MutualFund) // optional, if you need fund info
							.FirstOrDefaultAsync(n => n.Id == id);
		}
		public async Task UpdateAsync(NavHistory nav)
		{
			_db.NavHistories.Update(nav);
			await _db.SaveChangesAsync();
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

		//public async Task BulkInsertAsync(IEnumerable<NavHistory> navHistories)
		//{
		//	if (navHistories == null || !navHistories.Any())
		//		return;

		//	await _db.NavHistories.AddRangeAsync(navHistories);
		//	await _db.SaveChangesAsync();
		//}
		public async Task BulkInsertAsync(IEnumerable<NavHistory> navHistories)
		{
			if (navHistories == null)
				return;

			var items = navHistories
				.GroupBy(n => new { n.MutualFundId, Date = n.NavDate.Date })
				.Select(g => g.First())
				.ToList();

			await _db.NavHistories.AddRangeAsync(items);
			await _db.SaveChangesAsync();
		}

		//public async Task<List<NavHistory>> GetPagedByFundAsync(int mutualFundId, int pageNumber, int pageSize)
		//{
		//	return await _db.NavHistories
		//		.Where(n => n.MutualFundId == mutualFundId)
		//		.OrderByDescending(n => n.NavDate)
		//		.Skip((pageNumber - 1) * pageSize)
		//		.Take(pageSize)
		//		.ToListAsync();
		//}
		public async Task<List<NavHistory>> GetPagedByFundAsync(int fundId, int page, int pageSize, bool orderByDescending = false)
		{
			var query = _db.NavHistories.Where(n => n.MutualFundId == fundId);

			query = orderByDescending ? query.OrderByDescending(n => n.NavDate)
									  : query.OrderBy(n => n.NavDate);

			return await query
						 .Skip((page - 1) * pageSize)
						 .Take(pageSize)
						 .ToListAsync();
		}

		public async Task<int> GetCountByFundAsync(int mutualFundId)
		{
			return await _db.NavHistories
				.CountAsync(n => n.MutualFundId == mutualFundId);
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
