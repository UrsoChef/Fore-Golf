using Fore_Golf.Interfaces;
using Fore_Golf.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fore_Golf.Contracts;

namespace Fore_Golf.Repositories
{
    public class GolferRepository : IGolferRepository

    {
        private readonly ApplicationDbContext _db;
        public GolferRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Golfer entity)
        {
            await _db.Golfers.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Golfer entity)
        {
            _db.Golfers.Remove(entity);
            return await Save();
        }

        public async Task<List<Golfer>> FindAll()
        {
            var golfers =await _db.Golfers.ToListAsync();
            return golfers;
        }

        public async Task<Golfer> FindByID(Guid id)
        {
            var golfer = await _db.Golfers.FindAsync(id);
            return golfer;
        }

        public async Task<bool> IsExists(Guid id)
        {
            var exists = await _db.Golfers.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Golfer entity)
        {
            _db.Golfers.Update(entity);
            return await Save();
        }
    }
}
