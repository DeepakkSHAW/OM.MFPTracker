using Microsoft.EntityFrameworkCore;
using OM.MFPTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.MFPTracker.Data.Services
{
    public interface IPersonRepo
    {
        //Task<List <Person>> GetAllAsync();
        Task<List<Person>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default);
        Task UpdateAsync(Person person, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    }
    public class PersonRepo : IPersonRepo
    {
        private readonly MFPTrackerDbContext _db;
        public PersonRepo(MFPTrackerDbContext db)
        {
            _db = db;
        }

        //public async Task<List<Person>> GetAllAsync() => await _db.People.ToListAsync();

        // READ: All
        public async Task<List<Person>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _db.People
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);
        }

        // READ: By Id
        public async Task<Person?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _db.People
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        // CREATE
        public async Task<Person> AddAsync(
            Person person,
            CancellationToken cancellationToken = default)
        {
            _db.People.Add(person);
            await _db.SaveChangesAsync(cancellationToken);
            return person;
        }

        // UPDATE
        public async Task UpdateAsync(
            Person person,
            CancellationToken cancellationToken = default)
        {
            _db.People.Update(person);
            await _db.SaveChangesAsync(cancellationToken);
        }

        // DELETE
        public async Task DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var person = await _db.People
                                  .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (person is null)
                return;

            _db.People.Remove(person);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
