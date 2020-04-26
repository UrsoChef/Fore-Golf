using Fore_Golf.Contracts;
using Fore_Golf.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Repositories
{
    public class GameGolferRepository : IGameGolferRepository

    {
        private readonly ApplicationDbContext _db;
        public GameGolferRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckGolferInGame(Guid gameid, Guid golferid)
        {
            var golfers = await _db.GolferGames.AnyAsync(q => q.Game.Id == gameid && q.Golfer.Id == golferid);
            return golfers;
        }

        public async Task<bool> Create(GameGolfer entity)
        {
            await _db.GolferGames.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(GameGolfer entity)
        {
            _db.GolferGames.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<GameGolfer>> FindAll()
        {
            var golfers = await _db.GolferGames.ToListAsync();
            return golfers;
        }

        public async Task<GameGolfer> FindByID(Guid id)
        {
            var golfer = await _db.GolferGames.FindAsync(id);
            return golfer;
        }

        public async Task<GameGolfer> FindGolferInGame(Guid gameid, Guid golferid)
        {
            var golfers = await FindAll();
            var golfer = golfers.FirstOrDefault(q => q.Game.Id == gameid && q.Golfer.Id == golferid);
            return golfer;
        }

        public async Task<IEnumerable<GameGolfer>> FindAllGolfersInGame(Guid gameid)
        {
            var golfers = await FindAll();
            return golfers.Where(q => q.Game.Id == gameid).ToList();
        }

        public async Task<bool> IsExists(Guid id)
        {
            var exists = await _db.GolferGames.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(GameGolfer entity)
        {
            _db.GolferGames.Update(entity);
            return await Save();
        }
    }
}