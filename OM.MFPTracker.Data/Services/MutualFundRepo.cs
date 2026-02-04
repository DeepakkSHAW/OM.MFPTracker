using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using OM.MFPTracker.Data.Helper;
namespace OM.MFPTracker.Data.Services
{
	public interface IMutualFundRepo
	{
		Task<List<MutualFund>> GetAllAsync();
		Task<MutualFund?> GetByIdAsync(int id);
		Task AddAsync(MutualFund fund);
		Task UpdateAsync(MutualFund fund);
		Task DeleteAsync(int id);
		Task<bool> IsUniqueAsync(int id, string schemeCode, string isin);
	}

	public class MutualFundRepo : IMutualFundRepo
	{
		private readonly MFPTrackerDbContext _db;

		public MutualFundRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}

		public async Task<List<MutualFund>> GetAllAsync()
		{
			// Include MFCategory for dropdowns or display
			return await _db.MutualFunds
							.Include(f => f.MFCategory)
							.AsNoTracking()
							.ToListAsync();
		}

		public async Task<MutualFund?> GetByIdAsync(int id)
		{
			return await _db.MutualFunds
							.Include(f => f.MFCategory)
							.FirstOrDefaultAsync(f => f.Id == id);
		}

		public async Task AddAsync(MutualFund fund)
		{
			try
			{
				_db.MutualFunds.Add(fund);
				await _db.SaveChangesAsync();
			}
			catch (Exception ex) { throw ex; }
			//catch (DbUpdateException ex) when (DBValidation.IsUniqueConstraintViolation(ex))
			//{
			//	throw new DuplicateValueException("Scheme Code or ISIN already exists.");
			//}
		}

		public async Task UpdateAsync(MutualFund fund)
		{
			_db.MutualFunds.Update(fund);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var fund = await _db.MutualFunds.FindAsync(id);
			if (fund == null) return;

			_db.MutualFunds.Remove(fund);
			await _db.SaveChangesAsync();
		}
		public async Task<bool> IsUniqueAsync(int id, string schemeCode, string isin)
		{
			return !await _db.MutualFunds.AnyAsync(f => f.Id != id && (f.SchemeCode == schemeCode || f.ISIN == isin));
		}
	}

}
