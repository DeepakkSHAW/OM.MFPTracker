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
		Task<(List<Folio> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string searchTerm, string sortColumn, bool sortAscending);
		Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
		Task<Folio?> GetByIdAsync(int id);
		Task<List<Folio>> GetAllAsync();
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
		public async Task<(List<Folio> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string searchTerm, string sortColumn, bool sortAscending)
		{
			var query = _db.Folios
				.Include(f => f.Amc)
				.Include(f => f.FolioHolder)
				.AsQueryable();

			// ✅ SAFE SEARCH
			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				searchTerm = searchTerm.Trim().ToLower();

				query = query.Where(f =>
					f.FolioName.ToLower().Contains(searchTerm) ||
					(f.Amc != null && f.Amc.Code.ToLower().Contains(searchTerm)) ||
					(f.FolioHolder != null && f.FolioHolder.FirstName.ToLower().Contains(searchTerm))
				);
			}

			// ✅ SORTING
			query = sortColumn switch
			{
				"Amc" => sortAscending
					? query.OrderBy(f => f.Amc.Code)
					: query.OrderByDescending(f => f.Amc.Code),

				"Holder" => sortAscending
					? query.OrderBy(f => f.FolioHolder.FirstName)
					: query.OrderByDescending(f => f.FolioHolder.FirstName),

				_ => sortAscending
					? query.OrderBy(f => f.FolioName)
					: query.OrderByDescending(f => f.FolioName)
			};

			var totalCount = await query.CountAsync();

			var items = await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return (items, totalCount);
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

		public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null)
		{
			return await _db.Folios
				.AnyAsync(f =>
					f.FolioName.ToLower() == name.ToLower()
					&& (!excludeId.HasValue || f.FolioId != excludeId.Value));
		}

		public async Task<Folio?> GetByIdAsync(int id)
		{
			return await _db.Folios.FindAsync(id);
		}
		public async Task<List<Folio>> GetAllAsync()
		{
			return await _db.Folios
				.AsNoTracking()
				.OrderBy(x => x.FolioName)
				.ToListAsync();
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
