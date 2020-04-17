using Fore_Golf.Interfaces;
using Fore_Golf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Contracts
{
    public interface IMatchRepository : IRepositoryBase<Match>
    {
        Task<ICollection<Match>> GetGolfersByGolferandGame(Guid id);
    }
}
