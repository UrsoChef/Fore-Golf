using Fore_Golf.Contracts;
using Fore_Golf.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Repositories
{
    public class GameRepository : IGameRepository 

    {
        private readonly ApplicationDbContext _db;
        public GameRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Game entity)
        {
            await _db.Games.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Game entity)
        {
            _db.Games.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Game>> FindAll()
        {
            var games = await _db.Games.ToListAsync();
            return games;
        }

        public async Task<Game> FindByID(Guid id)
        {
            var game = await _db.Games.FindAsync(id);
            return game;
        }

        public async Task<ICollection<Game>> GetGamesInMatch(Guid matchid)
        {
            return await _db.Games.Include(g => g.MatchId).Where(q => q.MatchId == matchid).ToListAsync();
        }

        public Task<ICollection<Game>> GetScoreByGolferandMatch(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExists(Guid id)
        {
            var exists = await _db.Games.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Game entity)
        {
            _db.Games.Update(entity);
            return await Save();
        }
    }
}
