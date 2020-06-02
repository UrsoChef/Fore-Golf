using Fore_Golf.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Models
{
    public class MatchViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool? Status { get; set; }
        public IEnumerable<GolferViewModel> Golfers { get; set; } = new List<GolferViewModel>();
        public List<GameViewModel> Games { get; set; } = new List<GameViewModel>();
        public int NumberOfGames { get; set; }
        public int NumberOfPlayers { get; set; }
    }

    public class MatchScoresViewModel
    {
        public Guid Id { get; set; }
        public int FinalGross { get; set; }
        public int FinalNet { get; set; }
        public IEnumerable<GolferViewModel> Golfers { get; set; } = new List<GolferViewModel>();
        public List<GameScoresViewModel> Games { get; set; } = new List<GameScoresViewModel>();
    }
}