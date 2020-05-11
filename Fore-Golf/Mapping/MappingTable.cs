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
            CreateMap<Match, MatchViewModel>().ReverseMap();
            CreateMap<Match, MatchScoresViewModel>().ReverseMap();
            CreateMap<Game, GameViewModel>().ReverseMap();
            CreateMap<Game, GameScoresViewModel>().ReverseMap();
            CreateMap<Golfer, GolferViewModel>().ReverseMap(); 
            CreateMap<GameGolfer, GameGolferViewModel>().ReverseMap();
            CreateMap<GameGolfer, SetScoreViewModel>().ReverseMap();
            CreateMap<GameGolfer, GameGolferScoreViewModel>().ReverseMap();
            CreateMap<GameGolfer, GameScoresViewModel>().ReverseMap();
        }
    }
}
