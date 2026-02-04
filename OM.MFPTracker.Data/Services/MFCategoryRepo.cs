using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
	public interface IMFCategoryRepo
	{
		Task<List<MFCategory>> GetAllAsync();
		Task<MFCategory?> GetByIdAsync(int id);
		Task AddAsync(MFCategory category);
		Task UpdateAsync(MFCategory category);
		Task DeleteAsync(int id);
	}
	public class MFCategoryRepo : IMFCategoryRepo
	{
		private readonly MFPTrackerDbContext _db;


		public MFCategoryRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}


		public async Task<List<MFCategory>> GetAllAsync() => 
		await _db.MFCategories.AsNoTracking().ToListAsync();


		public async Task<MFCategory?> GetByIdAsync(int id) =>
		await _db.MFCategories.FindAsync(id);


		public async Task AddAsync(MFCategory category)
		{
			_db.MFCategories.Add(category);
			await _db.SaveChangesAsync();
		}


		public async Task UpdateAsync(MFCategory category)
		{
			_db.MFCategories.Update(category);
			await _db.SaveChangesAsync();
		}


		public async Task DeleteAsync(int id)
		{
			var entity = await _db.MFCategories.FindAsync(id);
			if (entity == null) return;


			_db.MFCategories.Remove(entity);
			await _db.SaveChangesAsync();
		}
	}
}
