using Fore_Golf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Models
{
    public class GameGolferViewModel
    {
        public Guid Id { get; set; }
        public Game Game { get; set; }
        public Golfer Golfer { get; set; }
        public int Score { get; set; }
        public int NetScore { get => Score - Golfer.Handicap; }
    }

    public class GameGolferStatusViewModel
    {
        public Guid MatchId { get; set; }
        public Guid GameId { get; set; }
        public GolferViewModel Golfer { get; set; }
        public bool IsInGame { get; set; } = false;
    }

    public class SetScoreViewModel
    {
        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public Guid GameId { get; set; }
        public Golfer Golfer { get; set; }
        public int Score { get; set; }
        public int NetScore { get => Score - Golfer.Handicap; }
    }
    public class GameGolferScoreViewModel
    {
        public Guid MatchId { get; set; }
        public Game Games { get; set; }
        public IEnumerable<GameGolferViewModel> GolferScores { get; set; }
    }

    public class GolferScoreViewModel
    {
        public Golfer Golfer { get; set; }
        public int Score { get; set; }
        public int NetScore { get => Score - Golfer.Handicap; }
    }
}
