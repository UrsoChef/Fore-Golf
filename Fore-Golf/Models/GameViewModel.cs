using Fore_Golf.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Models
{
    public class GameViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "Game Date")]
        public DateTime GameDate { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; }
        public Match Match { get; set; }
        public Guid MatchId { get; set; }
    }

    public class GameScoresViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MatchId { get; set; }
        [Display(Name = "Game Date")]
        public DateTime GameDate { get; set; }
        public bool Status { get; set; }
        public string Location { get; set; }
        public List<GameGolferViewModel> GameGolfers { get; set; }
    }

}