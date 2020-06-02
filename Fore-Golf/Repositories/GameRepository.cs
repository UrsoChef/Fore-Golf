using Fore_Golf.Contracts;
using Fore_Golf.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        public async Task<List<Game>> FindAll()
        {
            var games = await _db.Games.ToListAsync();
            return games;
        }

        public async Task<Game> FindByID(Guid id)
        {
            var game = await _db.Games.FindAsync(id);
            return game;
        }

        public async Task<List<Game>> GetGamesInMatch(Guid matchid)
        {
            return await _db.Games.Include(g => g.Match).Where(q => q.MatchId == matchid).ToListAsync();
        }

        public async Task<Game> GetPreviousGame(Guid id)
        {
            Game game = await _db.Games.FindAsync(id);
            List<Game> gamesPerMatch = await _db.Games.Include(m => m.MatchId == game.MatchId).ToListAsync();
            int thisGame = gamesPerMatch.FindIndex(g => g.Id == id);
            if (thisGame > 0)
            {
                Game lastGame = gamesPerMatch[thisGame - 1];
                return lastGame;
            }
            else
            {
                return game;
            }
        }

        public Task<List<Game>> GetScoreByGolferandMatch(Guid id)
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
