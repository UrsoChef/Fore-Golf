using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForeGolf.Models
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public DateTime GameDate { get; set; }
        public GolferViewModel Golfer { get; set; }
        public Guid GolferId { get; set; }
        public IEnumerable<SelectListItem> Golfers { get; set; }

    }
}