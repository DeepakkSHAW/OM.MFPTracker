using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
	public interface IAmcRepo
	{
		Task<List<Amc>> GetAllAsync();
		Task<Amc?> GetByIdAsync(int id);

		Task AddAsync(Amc amc);
		Task UpdateAsync(Amc amc);
		Task DeleteAsync(int id);

		Task<bool> IsUniqueAsync(int id, string name);
		Task<bool> IsUsedAsync(int id);   // important (FK used by MutualFund)
	}
	public class AmcRepo : IAmcRepo
	{
		private readonly MFPTrackerDbContext _db;

		public AmcRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}
		public async Task<List<Amc>> GetAllAsync_redundent()
		{
			return await _db.Amcs
				.OrderBy(a => a.Name)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<List<Amc>> GetAllAsync()
		{
			return await _db.Amcs
				.Select(a => new Amc
				{
					Id = a.Id,
					Name = a.Name,
					Code = a.Code,
					FundCount = _db.MutualFunds.Count(f => f.AmcId == a.Id)
				})
				.OrderBy(a => a.Name)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<Amc?> GetByIdAsync(int id)
		{
			return await _db.Amcs
				.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task AddAsync(Amc amc)
		{
			_db.Amcs.Add(amc);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(Amc amc)
		{
			var existing = await _db.Amcs.FindAsync(amc.Id);
			if (existing == null) return;

			_db.Entry(existing).CurrentValues.SetValues(amc);

			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var amc = await _db.Amcs.FindAsync(id);
			if (amc == null) return;

			_db.Amcs.Remove(amc);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> IsUniqueAsync(int id, string name)
		{
			return !await _db.Amcs
				.AnyAsync(a => a.Id != id && a.Name == name);
		}

		public async Task<bool> IsUsedAsync(int id)
		{
			return await _db.MutualFunds
				.AnyAsync(f => f.AmcId == id);
		}
	}
}
