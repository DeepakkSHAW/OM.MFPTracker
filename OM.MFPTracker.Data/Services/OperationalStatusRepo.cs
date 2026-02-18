using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
	public interface IOperationalStatusRepo
	{
		Task<List<OperationalStatus>> GetAllAsync();
		Task<List<OperationalStatus>> GetAllOrderedAsync();
		Task<OperationalStatus?> GetByIdAsync(int id);
		Task<OperationalStatus?> GetByCodeAsync(string code);

		Task AddAsync(OperationalStatus status);
		Task UpdateAsync(OperationalStatus status);
		Task DeleteAsync(int id);

		Task<bool> IsUniqueAsync(int id, string name);
		Task<bool> IsUsedAsync(int id);
		Task<bool> ExistsCodeAsync(string code);
	}
	public class OperationalStatusRepo : IOperationalStatusRepo
	{
		//private readonly AppDbContext _context;
		private readonly MFPTrackerDbContext _db;
		public OperationalStatusRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}

		public async Task<List<OperationalStatus>> GetAllAsync()
		{
			return await _db.OperationalStatuses
				.OrderBy(x => x.Code)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<List<OperationalStatus>> GetAllOrderedAsync()
		{
			return await _db.OperationalStatuses
				.OrderBy(x => x.Code)
				.ToListAsync();
		}
		
		public async Task<OperationalStatus?> GetByIdAsync(int id)
		{
			return await _db.OperationalStatuses
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<OperationalStatus?> GetByCodeAsync(string code)
		{
			return await _db.OperationalStatuses
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Code == code);
		}

		public async Task<bool> ExistsCodeAsync(string code)
		{
			return await _db.OperationalStatuses
				.AnyAsync(x => x.Code == code);
		}

		public async Task AddAsync(OperationalStatus entity)
		{
			_db.OperationalStatuses.Add(entity);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(OperationalStatus entity)
		{
			_db.OperationalStatuses.Update(entity);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var existing = await _db.OperationalStatuses.FindAsync(id);
			if (existing == null) return;

			_db.OperationalStatuses.Remove(existing);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> IsUniqueAsync(int id, string code)
		{
			return !await _db.OperationalStatuses
				.AnyAsync(x => x.Code == code && x.Id != id);
		}

		public async Task<bool> IsUsedAsync(int id)
		{
			return await _db.MutualFunds
				.AnyAsync(x => x.OperationalStatusId == id);
		}
	}

}
