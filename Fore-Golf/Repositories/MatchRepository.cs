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
    public class MatchRepository : IMatchRepository

    {
        private readonly ApplicationDbContext _db;
        public MatchRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Match entity)
        {
            await _db.Matches.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Match entity)
        {
            _db.Matches.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Match>> FindAll()
        {
            var matches = await _db.Matches.ToListAsync();
            return matches;
        }

        public async Task<Match> FindByID(Guid id)
        {
            var match = await _db.Matches.FindAsync(id);
            return match;
        }

        public async Task<Match> FindGamesAndGolfersInMatch(Guid id)
        {
            var matches = await _db.Matches.Include(m => m.Games).ThenInclude(g => g.GameGolfers).ThenInclude(gg => gg.Golfer).FirstOrDefaultAsync(m => m.Id == id);
            return matches;
        }
        public async Task<IEnumerable<Golfer>> FindGolfersInMatch(Guid id)
        {
            var games = await _db.Games.Include(g => g.GameGolfers).ThenInclude(gg => gg.Golfer).FirstOrDefaultAsync(g => g.MatchId == id);
            var golfersInMatch = games.GameGolfers.Select(m => m.Golfer);
            return golfersInMatch;
        }

        public async Task<bool> IsExists(Guid id)
        {
            var exists = await _db.Matches.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Match entity)
        {
            _db.Matches.Update(entity);
            return await Save();
        }
    }
}
