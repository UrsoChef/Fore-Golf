﻿using Fore_Golf.Contracts;
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
            var golfers = await _db.GameGolfers.AnyAsync(q => q.Game.Id == gameid && q.Golfer.Id == golferid);
            return golfers;
        }
        public async Task<bool> CheckGame(Guid gameid)
        {
            var golfers = await _db.GameGolfers.AnyAsync(q => q.Game.Id == gameid);
            return golfers;
        }

        public async Task<bool> Create(GameGolfer entity)
        {
            await _db.GameGolfers.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(GameGolfer entity)
        {
            _db.GameGolfers.Remove(entity);
            return await Save();
        }

        public async Task<List<GameGolfer>> FindAll()
        {
            var golfers = await _db.GameGolfers.ToListAsync();
            return golfers;
        }

        public async Task<GameGolfer> FindByID(Guid id)
        {
            var golfer = await _db.GameGolfers.FindAsync(id);
            return golfer;
        }

        public async Task<GameGolfer> FindGolferInGame(Guid gameid, Guid golferid)
        {
            return await _db.GameGolfers.Include(g => g.Golfer).FirstOrDefaultAsync(q => q.GameId == gameid && q.GolferId == golferid);
            //var golfers = await FindAll();
            //var golfer = golfers.FirstOrDefault(q => q.Game.Id == gameid && q.Golfer.Id == golferid);
            //return golfer;
        }

        public async Task<List<GameGolfer>> FindAllGolfersInGame(Guid gameid)
        {
            return await _db.GameGolfers.Include(g => g.Golfer).Include(g => g.Game).Where(q => q.Game.Id == gameid).ToListAsync();
            //var golfers = await FindAll();
            //var golfersInGame = golfers.Where(q => q.Game.Id == gameid).ToList();
            //return golfersInGame;
        }

        public async Task<List<GameGolfer>> FindGame(Guid gameid)
        {
            return await _db.GameGolfers.Where(q => q.Game.Id == gameid).ToListAsync();
        }

        public async Task<bool> IsExists(Guid id)
        {
            var exists = await _db.GameGolfers.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(GameGolfer entity)
        {
            _db.GameGolfers.Update(entity);
            return await Save();
        }

        public async Task<List<GameGolfer>> FindAllInMatch(Guid matchid)
        {
            return await _db.GameGolfers.Include(g => g.Golfer).Where(q => q.Game.MatchId == matchid).ToListAsync();
        }

        public async Task<List<GameGolfer>> FindLatestGameandGolfersInMatch(Guid matchid, Guid gameid)
        {
            //objective of this function is to 
            //> take gameid input (consider taking matchid instead of game?)
            //> find all the games under the match with this gameid 
            //> find last game amongst list
            //> return latest gamegolfer list, ket is golfers

            Game game = await _db.Games.FindAsync(gameid);
            List<Game> gamesPerMatch = await _db.Games.Include(m => m.Match).ToListAsync();
            gamesPerMatch.Sort((x, y) => DateTime.Compare(x.GameDate, y.GameDate));
            int thisGame = gamesPerMatch.FindIndex(g => g.Id == gameid);
            Game lastGame = game;
            if (thisGame > 0)
            {
                lastGame = gamesPerMatch[thisGame - 1];
            }
            List<GameGolfer> gameGolfers = await _db.GameGolfers.Where(q => q.Game == lastGame).Include(g => g.Golfer).ToListAsync();
            return gameGolfers;


            //List<GameGolfer> gameGolfers = await _db.GameGolfers.Include(g => g.Golfer).Where(q => q.Game.MatchId == matchid).ToListAsync();
            //GameGolfer latestGame = gameGolfers.Where(g => g.GameId != gameid).Last();
            //return gameGolfers.Where(gg => gg.GameId == latestGame.GameId);
        }
    }
}