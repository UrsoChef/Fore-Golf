using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fore_Golf.Data
{
    public class Golfer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Handicap { get; set; } = 0;
        public DateTime DateJoined { get; set; } = DateTime.Now;
        public string Email { get; set; }
    }
}
