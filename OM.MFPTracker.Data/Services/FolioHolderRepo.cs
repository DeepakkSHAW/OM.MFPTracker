using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.MFPTracker.Data.Services
{
    public interface IFolioHolder
	{
        //Task<List <Person>> GetAllAsync();
        Task<List<FolioHolder>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<FolioHolder?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<FolioHolder> AddAsync(FolioHolder person, CancellationToken cancellationToken = default);
        Task UpdateAsync(FolioHolder person, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    }
    public class FolioHolderRepo : IFolioHolder
	{
        private readonly MFPTrackerDbContext _db;
        public FolioHolderRepo(MFPTrackerDbContext db)
        {
            _db = db;
        }

        //public async Task<List<Person>> GetAllAsync() => await _db.People.ToListAsync();

        // READ: All
        public async Task<List<FolioHolder>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _db.FolioHolders
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);
        }

        // READ: By Id
        public async Task<FolioHolder?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _db.FolioHolders
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        // CREATE
        public async Task<FolioHolder> AddAsync(
            FolioHolder person,
            CancellationToken cancellationToken = default)
        {
            _db.FolioHolders.Add(person);
            await _db.SaveChangesAsync(cancellationToken);
            return person;
        }

        // UPDATE
        public async Task UpdateAsync(
            FolioHolder person,
            CancellationToken cancellationToken = default)
        {
            _db.FolioHolders.Update(person);
            await _db.SaveChangesAsync(cancellationToken);
        }

        // DELETE
        public async Task DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var person = await _db.FolioHolders
                                  .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (person is null)
                return;

            _db.FolioHolders.Remove(person);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
