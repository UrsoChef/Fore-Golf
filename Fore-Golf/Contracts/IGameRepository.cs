﻿using Fore_Golf.Interfaces;
using Fore_Golf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Contracts
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<List<Game>> GetScoreByGolferandMatch(Guid id);
        Task<List<Game>> GetGamesInMatch(Guid id);
        Task<Game> GetPreviousGame(Guid id);
    }
}