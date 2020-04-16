using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForeGolf.Models
{
    public class GolferViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Handicap { get; set; }
        public DateTime DateJoined { get; set; }
        public string Email { get; set; }
    }
}