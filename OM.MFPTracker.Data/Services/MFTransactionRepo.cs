using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.MFPTracker.Data.Services
{
	public interface IMFTransactionRepo
	{

		Task<MutualFundTransaction?> GetByIdAsync(int id, CancellationToken ct = default);

		Task<(IReadOnlyList<MutualFundTransaction> Items, int TotalCount)> GetAsync(
			string? folio = null,
			string? fundNameContains = null,
			DateTime? from = null,
			DateTime? to = null,
			int skip = 0,
			int take = 50,
			CancellationToken ct = default);

		Task<int> AddAsync(MutualFundTransaction entity, CancellationToken ct = default);
		Task UpdateAsync(MutualFundTransaction entity, CancellationToken ct = default);
		Task DeleteAsync(int id, CancellationToken ct = default);

		Task<bool> ExistsAsync(int id, CancellationToken ct = default);

	}
	public class MFTransactionRepo: IMFTransactionRepo
	{
		private readonly MFPTrackerDbContext _db;

		public MFTransactionRepo(MFPTrackerDbContext db)
		{
			_db = db;
		}

		public async Task<MutualFundTransaction?> GetByIdAsync(int id, CancellationToken ct = default)
		{
			return await _db.Set<MutualFundTransaction>()
							.AsNoTracking()
							.FirstOrDefaultAsync(x => x.Id == id, ct);
		}

		public async Task<(IReadOnlyList<MutualFundTransaction> Items, int TotalCount)> GetAsync(
			string? folio = null,
			string? fundNameContains = null,
			DateTime? from = null,
			DateTime? to = null,
			int skip = 0,
			int take = 50,
			CancellationToken ct = default)
		{
			var query = _db.Set<MutualFundTransaction>().AsNoTracking();

			if (!string.IsNullOrWhiteSpace(folio))
				query = query.Where(x => x.Folio == folio);

			if (!string.IsNullOrWhiteSpace(fundNameContains))
				query = query.Where(x => x.FundName.Contains(fundNameContains));

			if (from is not null)
				query = query.Where(x => x.Date >= from);

			if (to is not null)
				query = query.Where(x => x.Date <= to);

			var total = await query.CountAsync(ct);

			var items = await query
				.OrderByDescending(x => x.Date)
				.ThenBy(x => x.FundName)
				.Skip(skip)
				.Take(take)
				.ToListAsync(ct);

			return (items, total);
		}

		public async Task<int> AddAsync(MutualFundTransaction entity, CancellationToken ct = default)
		{
			// Defensive server-side validation (complements DataAnnotations)
			if (entity.NAV <= 0m)
				throw new ArgumentOutOfRangeException(nameof(entity.NAV), "NAV must be greater than 0.");

			// Business logic: compute AmountPaid and audit fields
			entity.AmountPaid = ComputeAmountPaid(entity.Units, entity.NAV);
			var utcNow = DateTime.UtcNow;
			entity.CreatedUtc = utcNow;
			entity.UpdatedUtc = utcNow;

			_db.Set<MutualFundTransaction>().Add(entity);
			await _db.SaveChangesAsync(ct);

			return entity.Id;
		}

		public async Task UpdateAsync(MutualFundTransaction entity, CancellationToken ct = default)
		{
			// Recompute AmountPaid in case Units/NAV changed
			entity.AmountPaid = ComputeAmountPaid(entity.Units, entity.NAV);
			entity.UpdatedUtc = DateTime.UtcNow;

			_db.Set<MutualFundTransaction>().Update(entity);
			await _db.SaveChangesAsync(ct);
		}

		public async Task DeleteAsync(int id, CancellationToken ct = default)
		{
			var existing = await _db.Set<MutualFundTransaction>().FindAsync(new object?[] { id }, ct);
			if (existing is null) return;

			_db.Set<MutualFundTransaction>().Remove(existing);
			await _db.SaveChangesAsync(ct);
		}

		public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
		{
			return _db.Set<MutualFundTransaction>().AnyAsync(x => x.Id == id, ct);
		}

		private static decimal ComputeAmountPaid(decimal units, decimal nav)
		{
			// Banker's rounding to 2 decimals; adjust if you need another policy
			return Math.Round(units * nav, 2, MidpointRounding.ToEven);
		}
	}

}

