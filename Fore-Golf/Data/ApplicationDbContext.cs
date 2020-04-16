using System;
using System.Collections.Generic;
using System.Text;
using ForeGolf.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fore_Golf.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
