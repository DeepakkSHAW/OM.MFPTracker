using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;

namespace OM.MFPTracker.Data.Services
{
	public interface ISpecialEventRepo
	{
		Task<List<SpecialEvent>> GetAllAsync();
		Task<SpecialEvent?> GetByIdAsync(int id);
		Task AddAsync(SpecialEvent evt);
		Task UpdateAsync(SpecialEvent evt);
		Task DeleteAsync(int id);

		//* Required for pagination *//
		Task<int> GetCountAsync();
		Task<List<SpecialEvent>> GetPagedAsync(int page, int pageSize);

	}

	public class SpecialEventRepo : ISpecialEventRepo
	{
		private readonly MFPTrackerDbContext _db;
		public SpecialEventRepo(MFPTrackerDbContext db) => _db = db;

		public async Task<List<SpecialEvent>> GetAllAsync()
		{
			return await _db.SpecialEvents
							.OrderByDescending(e => e.EventDate)
							.ToListAsync();
		}

		public async Task<SpecialEvent?> GetByIdAsync(int id)
		{
			return await _db.SpecialEvents.FindAsync(id);
		}

		public async Task AddAsync(SpecialEvent evt)
		{
			_db.SpecialEvents.Add(evt);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(SpecialEvent evt)
		{
			_db.SpecialEvents.Update(evt);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var evt = await _db.SpecialEvents.FindAsync(id);
			if (evt != null)
			{
				_db.SpecialEvents.Remove(evt);
				await _db.SaveChangesAsync();
			}
		}
		public async Task<int> GetCountAsync()
		{
			return await _db.SpecialEvents.CountAsync();
		}

		public async Task<List<SpecialEvent>> GetPagedAsync(int page, int pageSize)
		{
			return await _db.SpecialEvents
				.OrderByDescending(e => e.EventDate)
				.Skip((page - 1) * pageSize)   
				.Take(pageSize)               
				.AsNoTracking()
				.ToListAsync();
		}

	}
}
