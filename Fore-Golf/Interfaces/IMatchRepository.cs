using Fore_Golf.Interfaces;
using ForeGolf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForeGolf.Contracts
{
    public interface IMatchRepository : IRepositoryBase<Match>
    {
        Task<ICollection<Match>> GetGolfersByGolferandGame(Guid id);
    }
}
