using Fore_Golf.Interfaces;
using ForeGolf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForeGolf.Contracts
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<ICollection<Game>> GetScoreByGolferandMatch(Guid id);
    }
}
