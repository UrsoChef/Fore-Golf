using AutoMapper;
using ForeGolf.Data;
using ForeGolf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForeGolf.Mapping
{
    public class MappingTable : Profile
    {
        public MappingTable()
        {
            CreateMap<Game, GameViewModel>().ReverseMap();
            CreateMap<Match, MatchViewModel>().ReverseMap();
            CreateMap<Golfer, GolferViewModel>().ReverseMap();
        }
    }
}
