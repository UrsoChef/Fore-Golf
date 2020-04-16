using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForeGolf.Data
{
    public class Golfer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Handicap { get; set; }
        public DateTime DateJoined { get; set; }
        public string Email { get; set; }
    }
}
