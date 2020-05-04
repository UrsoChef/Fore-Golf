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
        Task<IEnumerable<GameGolfer>> FindAllGolfersInGame(Guid gameid);
        Task<IEnumerable<GameGolfer>> FindAllInMatch(Guid matchid);
        Task<ICollection<GameGolfer>> FindGame(Guid gameid);
        Task<GameGolfer> FindGolferInGame(Guid gameid, Guid golferid);
        Task<IEnumerable<GameGolfer>> FindLatestGameandGolfersInMatch(Guid matchid, Guid gameid);

    }
}
