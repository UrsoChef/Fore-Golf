using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Data
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Score { get; set; }
        public DateTime GameDate { get; set; }
        //[ForeignKey]
        [ForeignKey("Id")]
        public Golfer Golfer { get; set; }
        //public Guid GolferId { get; set; }
    }
}
