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
        public Guid Id { get; set; }
        public DateTime GameDate { get; set; }
        public string Location { get; set; }
        public Match Match { get; set; }
        public Guid MatchId { get; set; }
    }
}
