using Fore_Golf.Interfaces;
using Fore_Golf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Contracts
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<ICollection<Game>> GetScoreByGolferandMatch(Guid id);
    }
}
