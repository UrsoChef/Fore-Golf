using Fore_Golf.Data;
using Fore_Golf.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Contracts
{
    public interface IGameGolferRepository : IRepositoryBase<GameGolfer>
    {
        Task<bool> CheckGolferInGame(Guid gameid, Guid golferid); 
        Task<bool> CheckGame(Guid gameid);
        Task<List<GameGolfer>> FindAllGolfersInGame(Guid gameid);
        Task<List<GameGolfer>> FindAllInMatch(Guid matchid);
        Task<List<GameGolfer>> FindGame(Guid gameid);
        Task<GameGolfer> FindGolferInGame(Guid gameid, Guid golferid);
        Task<List<GameGolfer>> FindLatestGameandGolfersInMatch(Guid matchid, Guid gameid);

    }
}
