using AutoMapper;
using Fore_Golf.Data;
using Fore_Golf.Models;
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
            CreateMap<GameGolfer, GameGolferViewModel>().ReverseMap();
            CreateMap<GameGolfer, SetScoreViewModel>().ReverseMap();
            CreateMap<GameGolfer, GolferScoreViewModel>().ReverseMap();
        }
    }
}
