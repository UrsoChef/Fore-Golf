using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Data
{
    public class Match
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int StartingPosition { get; set; }
        public DateTime MatchStartDate { get; set; }
        public DateTime MatchEndDate { get; set; }
        [ForeignKey("Id")]
        public Game Game { get; set; }
        //public Guid GameId { get; set; }
    }
}
