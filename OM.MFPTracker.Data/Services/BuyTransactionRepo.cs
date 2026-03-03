using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
    public interface IBuyTransactionRepo
    {
		Task<List<FundTransaction>> GetByFolioAsync(int folioId);

		Task<FundTransaction?> GetByIdAsync(int id);

		Task<int> CreateAsync(FundTransaction transaction);

		Task UpdateAsync(FundTransaction transaction);

		Task DeleteAsync(FundTransaction transaction);
	}



	public class BuyTransactionRepo : IBuyTransactionRepo
	{
		private readonly MFPTrackerDbContext _db;

		public BuyTransactionRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}
		public async Task<List<FundTransaction>> GetByFolioAsync(int folioId)
		{
			return await _db.FundTransactions
				.Where(t => t.FolioId == folioId &&
							t.Type == TransactionType.Buy)
				.OrderByDescending(t => t.TransactionDate)
				.ToListAsync();
		}

		public async Task<FundTransaction?> GetByIdAsync(int id)
		{
			return await _db.FundTransactions
				.FirstOrDefaultAsync(t => t.FundTransactionId == id &&
										  t.Type == TransactionType.Buy);
		}

		public async Task<int> CreateAsync(FundTransaction transaction)
		{
			// 🔥 enforce Buy rules here
			transaction.Type = TransactionType.Buy;
			transaction.UnitsLeft = transaction.Units;
			transaction.Amount = transaction.Units * transaction.Nav;
			transaction.InDate = DateTime.UtcNow;
			transaction.UpdateDate = DateTime.UtcNow;

			_db.FundTransactions.Add(transaction);
			await _db.SaveChangesAsync();

			return transaction.FundTransactionId;
		}

		public async Task UpdateAsync(FundTransaction transaction)
		{
			// 🚨 Prevent edit if partially sold
			if (transaction.UnitsLeft != transaction.Units)
				throw new Exception("Cannot edit buy transaction that is partially sold.");

			transaction.Amount = transaction.Units * transaction.Nav;
			transaction.UpdateDate = DateTime.UtcNow;

			_db.FundTransactions.Update(transaction);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(FundTransaction transaction)
		{
			// 🚨 Prevent delete if partially sold
			if (transaction.UnitsLeft != transaction.Units)
				throw new Exception("Cannot delete buy transaction that is partially sold.");

			_db.FundTransactions.Remove(transaction);
			await _db.SaveChangesAsync();
		}
	}
}
