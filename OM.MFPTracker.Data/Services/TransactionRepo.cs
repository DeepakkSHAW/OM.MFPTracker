using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
	public interface ITransactionRepo
	{
		Task<(List<FundTransaction> Data, int TotalCount)> GetPagedAsync(int folioId, string? search, int pageNumber, int pageSize);
		Task<PagedResult<FundTransaction>> GetPagedAsync(int? folioId, int? fundId, TransactionType? type, string? search, int page, int pageSize);
		Task<FundTransaction?> GetByIdAsync(int id);

		Task<List<FundTransaction>> GetByFolioAsync(int folioId);

		Task<List<FundTransaction>> GetByFundAsync(int fundId);

		Task<List<FundTransaction>> GetOpenBuyLotsAsync(int folioId, int fundId);

		Task<List<FundTransaction>> GetSellGroupAsync(Guid sellGroupId);

		Task AddAsync(FundTransaction transaction);

		Task AddRangeAsync(IEnumerable<FundTransaction> transactions);
		Task<int> CreateAsync(FundTransaction transaction);
		Task UpdateAsync(FundTransaction transaction);

		Task DeleteAsync(FundTransaction transaction);

		Task SaveChangesAsync();



		Task DeleteAsync(int id);

	}

	public class TransactionRepo : ITransactionRepo
	{
		private readonly MFPTrackerDbContext _db;

		public TransactionRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}
		public async Task DeleteAsync(int id)
		{
			var transaction = await _db.FundTransactions
				.FirstOrDefaultAsync(x => x.FundTransactionId == id);

			if (transaction == null)
				throw new Exception("Transaction not found");

			_db.FundTransactions.Remove(transaction);

			await _db.SaveChangesAsync();
		}
		public async Task<PagedResult<FundTransaction>> GetPagedAsync(int? folioId, int? fundId, TransactionType? type, string? search, int page, int pageSize)
		{
			var query = _db.FundTransactions.Include(x => x.Folio).Include(x => x.Fund).AsQueryable();

			// Filters

			if (folioId.HasValue)
				query = query.Where(x => x.FolioId == folioId.Value);

			if (fundId.HasValue)
				query = query.Where(x => x.FundId == fundId.Value);

			if (type.HasValue)
				query = query.Where(x => x.Type == type.Value);

			if (!string.IsNullOrWhiteSpace(search))
				query = query.Where(x => x.Remarks!.Contains(search));

			var totalCount = await query.CountAsync();

			var data = await query
				.OrderByDescending(x => x.TransactionDate)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PagedResult<FundTransaction>
			{
				Data = data,
				TotalCount = totalCount
			};
		}
		public async Task<(List<FundTransaction>, int)> GetPagedAsync(int folioId, string? search, int pageNumber, int pageSize)
		{
			var query = _db.FundTransactions
				.Where(t => t.FolioId == folioId &&
							t.Type == TransactionType.Buy);

			if (!string.IsNullOrWhiteSpace(search))
			{
				query = query.Where(t =>
					t.Remarks!.Contains(search));
			}

			var totalCount = await query.CountAsync();

			var data = await query
				.OrderByDescending(t => t.TransactionDate)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return (data, totalCount);
		}
		public async Task<int> CreateAsync(FundTransaction transaction)
		{
			transaction.Type = TransactionType.Buy;

			transaction.UnitsLeft = transaction.Units;

			transaction.Amount = transaction.Units * transaction.Nav;

			//transaction.InDate = DateTime.UtcNow;
			transaction.UpdateDate = DateTime.UtcNow;

			_db.FundTransactions.Add(transaction);

			await _db.SaveChangesAsync();

			return transaction.FundTransactionId;
		}
		public async Task<FundTransaction?> GetByIdAsync(int id)
		{
			return await _db.FundTransactions
				.Include(t => t.ConsumedBuyTransactionId)
				.FirstOrDefaultAsync(t => t.FundTransactionId == id);
		}

		public async Task<List<FundTransaction>> GetByFolioAsync(int folioId)
		{
			return await _db.FundTransactions
				.Where(t => t.FolioId == folioId)
				.OrderBy(t => t.TransactionDate)
				.ToListAsync();
		}

		public async Task<List<FundTransaction>> GetByFundAsync(int fundId)
		{
			return await _db.FundTransactions
				.Where(t => t.FundId == fundId)
				.OrderBy(t => t.TransactionDate)
				.ToListAsync();
		}

		// 🔥 Critical for FIFO logic
		public async Task<List<FundTransaction>> GetOpenBuyLotsAsync(int folioId, int fundId)
		{
			return await _db.FundTransactions
				.Where(t => t.FolioId == folioId &&
							t.FundId == fundId &&
							t.Type == TransactionType.Buy &&
							t.UnitsLeft > 0)
				.OrderBy(t => t.TransactionDate) // FIFO
				.ToListAsync();
		}

		public async Task<List<FundTransaction>> GetSellGroupAsync(Guid sellGroupId)
		{
			return await _db.FundTransactions
				.Where(t => t.SellGroupId == sellGroupId)
				.ToListAsync();
		}

		public async Task AddAsync(FundTransaction transaction)
		{
			await _db.FundTransactions.AddAsync(transaction);
		}

		public async Task AddRangeAsync(IEnumerable<FundTransaction> transactions)
		{
			await _db.FundTransactions.AddRangeAsync(transactions);
		}

		public async Task UpdateAsync(FundTransaction transaction)
		{
			var existing = await _db.FundTransactions
				.FirstOrDefaultAsync(x => x.FundTransactionId == transaction.FundTransactionId);

			if (existing == null)
				throw new Exception("Transaction not found");

			existing.TransactionDate = transaction.TransactionDate;
			existing.Units = transaction.Units;
			existing.Nav = transaction.Nav;
			existing.Remarks = transaction.Remarks;
			existing.Charges = transaction.Charges;

			existing.Amount = transaction.Units * transaction.Nav;

			existing.UpdateDate = DateTime.UtcNow;

			await _db.SaveChangesAsync();
		}

		public Task DeleteAsync(FundTransaction transaction)
		{
			_db.FundTransactions.Remove(transaction);
			return Task.CompletedTask;
		}

		public async Task SaveChangesAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
