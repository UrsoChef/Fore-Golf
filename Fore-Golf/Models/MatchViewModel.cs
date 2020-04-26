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
        public GolferViewModel Golfer { get; set; }
        public Guid GolferId { get; set; }
        public IEnumerable<SelectListItem> Golfers { get; set; }
        public GameViewModel Game { get; set; }
        public Guid GameId { get; set; }
        public IEnumerable<SelectListItem> Games { get; set; }
    }
    public class MatchSummaryViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool? Status { get; set; }
        public List<GameViewModel> Games { get; set; }
        public List<GolferViewModel> Golfers { get; set; }
    }
}