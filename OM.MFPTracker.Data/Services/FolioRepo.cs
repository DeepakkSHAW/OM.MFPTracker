using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
	public interface IFolioRepo
	{
		Task<(List<Folio> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
		Task<Folio?> GetByIdAsync(int id);
		Task AddAsync(Folio folio);
		Task UpdateAsync(Folio folio);
		Task DeleteAsync(int id);
	}
	public class FolioRepo : IFolioRepo
	{
		private readonly MFPTrackerDbContext _db;

		public FolioRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}

		public async Task<(List<Folio> Items, int TotalCount)>
			GetPagedAsync(int pageNumber, int pageSize)
		{
			var query = _db.Folios
				.Include(f => f.Amc)
				.Include(f => f.FolioHolder)
				.OrderByDescending(f => f.InDate);

			var total = await query.CountAsync();

			var items = await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.AsNoTracking()
				.ToListAsync();

			return (items, total);
		}

		public async Task<Folio?> GetByIdAsync(int id)
		{
			return await _db.Folios.FindAsync(id);
		}

		public async Task AddAsync(Folio folio)
		{
			if (await _db.Folios.AnyAsync(f => f.FolioName == folio.FolioName))
				throw new Exception("Folio name already exists.");

			_db.Folios.Add(folio);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(Folio folio)
		{
			folio.UpdateDate = DateTime.UtcNow;
			_db.Folios.Update(folio);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _db.Folios.FindAsync(id);
			if (entity == null) return;

			_db.Folios.Remove(entity);
			await _db.SaveChangesAsync();
		}
	}
}
