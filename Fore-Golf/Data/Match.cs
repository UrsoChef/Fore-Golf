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
        public string Name { get; set; }
        public string Location { get; set; }
        public bool? Status { get; set; }
        //public List<Game> Games { get; set; } = new List<Game>();
        public IEnumerable<Game> Games { get; set; }
    }
}
