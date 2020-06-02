using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Data
{
    public class GameGolfer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        //[ForeignKey("GameId")]
        public Game Game { get; set; }
        public Guid GameId { get; set; }
        //[ForeignKey("GolferId")]
        public Golfer Golfer { get; set; }
        public Guid GolferId { get; set; }
        public int GameHandicap { get; set; }
        public int Score { get; set; }
    }
}
