using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using OM.MFPTracker.Data.Helper;
namespace OM.MFPTracker.Data.Services
{
	//public interface IMutualFundRepo
	//{
	//	Task<List<MutualFund>> GetAllAsync();
	//	Task<List<MutualFund>> GetAllOrderedAsync();
	//	Task<MutualFund?> GetByIdAsync(int id);
	//	Task AddAsync(MutualFund fund);
	//	Task UpdateAsync(MutualFund fund);
	//	Task DeleteAsync(int id);
	//	Task<bool> IsUniqueAsync(int id, string schemeCode, string isin);
	//}

	//public class MutualFundRepo : IMutualFundRepo
	//{
	//	private readonly MFPTrackerDbContext _db;

	//	public MutualFundRepo(MFPTrackerDbContext db)
	//	{
	//		_db = db;
	//	}

	//	public async Task<List<MutualFund>> GetAllAsync()
	//	{
	//		// Include MFCategory for dropdowns or display
	//		return await _db.MutualFunds
	//						.Include(f => f.MFCategory)
	//						.AsNoTracking()
	//						.ToListAsync();
	//	}

	//	public async Task<List<MutualFund>> GetAllOrderedAsync()
	//	{
	//		return await _db.MutualFunds
	//			.Include(f => f.MFCategory)
	//			.OrderBy(f => f.SchemeName)
	//			.ThenBy(f => f.SchemeCode)
	//			.AsNoTracking()
	//			.ToListAsync();
	//	}
	//	public async Task<MutualFund?> GetByIdAsync(int id)
	//	{
	//		return await _db.MutualFunds
	//						.Include(f => f.MFCategory)
	//						.FirstOrDefaultAsync(f => f.Id == id);
	//	}

	//	public async Task AddAsync(MutualFund fund)
	//	{
	//		try
	//		{
	//			_db.MutualFunds.Add(fund);
	//			await _db.SaveChangesAsync();
	//		}
	//		catch (Exception ex) { throw ex; }
	//		//catch (DbUpdateException ex) when (DBValidation.IsUniqueConstraintViolation(ex))
	//		//{
	//		//	throw new DuplicateValueException("Scheme Code or ISIN already exists.");
	//		//}
	//	}

	//	public async Task UpdateAsync(MutualFund fund)
	//	{
	//		_db.MutualFunds.Update(fund);
	//		await _db.SaveChangesAsync();
	//	}

	//	public async Task DeleteAsync(int id)
	//	{
	//		var fund = await _db.MutualFunds.FindAsync(id);
	//		if (fund == null) return;

	//		_db.MutualFunds.Remove(fund);
	//		await _db.SaveChangesAsync();
	//	}
	//	public async Task<bool> IsUniqueAsync(int id, string schemeCode, string isin)
	//	{
	//		return !await _db.MutualFunds.AnyAsync(f => f.Id != id && (f.SchemeCode == schemeCode || f.ISIN == isin));
	//	}
	//}

	//////////NEW Implimentation using Generic Repo Pattern ///////////
	public interface IMutualFundRepo
	{
		Task<List<MutualFund>> GetAllAsync();
		Task<List<MutualFund>> GetAllOrderedAsync();
		Task<MutualFund?> GetByIdAsync(int id);
		Task<List<MutualFund>> GetByAmcAsync(int amcId);
		Task AddAsync(MutualFund fund);
		Task UpdateAsync(MutualFund fund);
		Task DeleteAsync(int id);
		Task<bool> IsUniqueAsync(int id, string schemeCode, string isin);
		Task<List<MutualFund>> SearchAsync(string? search, int? statusId, bool activeOnly);

		// NEW → used in NAV & Transactions
		Task<List<MutualFund>> GetActiveAsync();
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
			return await _db.MutualFunds
				.Include(f => f.Amc)
				.Include(f => f.MFCategory)
				.Include(f => f.OperationalStatus)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<List<MutualFund>> GetAllOrderedAsync()
		{
			return await _db.MutualFunds
				.Include(f => f.Amc)
				.Include(f => f.MFCategory)
				.Include(f => f.OperationalStatus)
				.OrderBy(f => f.Amc.Name)
				.ThenBy(f => f.SchemeName)
				.ThenBy(f => f.SchemeCode)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<MutualFund?> GetByIdAsync(int id)
		{
			return await _db.MutualFunds
				.Include(f => f.Amc)
				.Include(f => f.MFCategory)
				.Include(f => f.OperationalStatus)
				.FirstOrDefaultAsync(f => f.Id == id);
		}
		public async Task<List<MutualFund>> GetByAmcAsync(int amcId)
		{
			return await _db.MutualFunds
				.Where(f => f.AmcId == amcId)
				.OrderBy(f => f.SchemeName)
				.ToListAsync();
		}
		public async Task AddAsync(MutualFund fund)
		{
			fund.InDate = DateTime.Now;

			_db.MutualFunds.Add(fund);
			await _db.SaveChangesAsync();
		}
		//public async Task AddAsync(MutualFund fund)
		//{
		//	//fund.InDate = DateTime.Now;
		//	//// VERY IMPORTANT — avoid EF trying to insert lookup tables
		//	//fund.Amc = null!;
		//	//fund.MFCategory = null!;
		//	//fund.OperationalStatus = null!;

		//	//_db.MutualFunds.Add(fund);
		//	//await _db.SaveChangesAsync();
		//	var entity = new MutualFund
		//	{
		//		SchemeCode = fund.SchemeCode,
		//		SchemeName = fund.SchemeName,
		//		ISIN = fund.ISIN,
		//		MFCategoryId = fund.MFCategoryId,
		//		AmcId = fund.AmcId,
		//		OperationalStatusId = fund.OperationalStatusId,
		//		InDate = DateTime.Now
		//	};

		//_db.MutualFunds.Add(entity);
		//	await _db.SaveChangesAsync();
		//}

		public async Task UpdateAsync(MutualFund fund)
		{
			var existing = await _db.MutualFunds.FindAsync(fund.Id);
			if (existing == null) return;

			_db.Entry(existing).CurrentValues.SetValues(fund);

			await _db.SaveChangesAsync();
		}
		//public async Task UpdateAsync(MutualFund fund)
		//{
		//	var existing = await _db.MutualFunds.FindAsync(fund.Id);
		//	if (existing == null) return;

		//	fund.Amc = null!;
		//	fund.MFCategory = null!;
		//	fund.OperationalStatus = null!;

		//	_db.MutualFunds.Update(fund);
		//	await _db.SaveChangesAsync();
		//}

		//public async Task DeleteAsync(int id)
		//{
		//	var fund = await _db.MutualFunds.FindAsync(id);
		//	if (fund == null) return;

		//	_db.MutualFunds.Remove(fund);
		//	await _db.SaveChangesAsync();
		//}
		public async Task DeleteAsync(int id)
		{
			var fund = await _db.MutualFunds
				.Include(f => f.NavHistories)
				.FirstOrDefaultAsync(f => f.Id == id);

			if (fund == null)
				return;

			if (fund.NavHistories.Any())
				throw new InvalidOperationException("Cannot delete fund with NAV history.");

			_db.MutualFunds.Remove(fund);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> IsUniqueAsync(int id, string schemeCode, string isin)
		{
			return !await _db.MutualFunds
				.AnyAsync(f => f.Id != id &&
							   (f.SchemeCode == schemeCode || f.ISIN == isin));
		}
		public async Task<List<MutualFund>> GetActiveAsync()
		{
			return await _db.MutualFunds
				.Include(f => f.Amc)
				.Include(f => f.MFCategory)
				.Include(f => f.OperationalStatus)
				.Where(f => f.OperationalStatus.Code == "Active/Open-ended") // TODO: Use Enum or Constants for status codes
				.OrderBy(f => f.Amc.Name)
				.ThenBy(f => f.SchemeName)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<List<MutualFund>> SearchAsync(string? search, int? statusId, bool activeOnly)
		{
			var query = _db.MutualFunds
				.Include(f => f.Amc)
				.Include(f => f.MFCategory)
				.Include(f => f.OperationalStatus)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(search))
			{
				search = search.ToLower();

				query = query.Where(f =>
					f.SchemeName.ToLower().Contains(search) ||
					f.SchemeCode.ToLower().Contains(search) ||
					f.ISIN.ToLower().Contains(search) ||
					f.Amc.Name.ToLower().Contains(search));
			}

			if (statusId.HasValue)
				query = query.Where(f => f.OperationalStatusId == statusId);

			if (activeOnly)
				query = query.Where(f => f.OperationalStatus.IsTransactionAllowed);

			return await query
				.OrderBy(f => f.SchemeName)
				.AsNoTracking()
				.ToListAsync();
		}

	}
}
