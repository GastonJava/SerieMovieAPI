using AutoMapper;
using SerieMovieAPI.Models.DTOs.Characters;
using SerieMovieAPI.Models.DTOs.Movieseries;
using System.Collections.Generic;

namespace SerieMovieAPI.Models.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CharacterModel, CharactersDTO>().ReverseMap();
            CreateMap<MovieserieModel, MovieseriesDTO>().ReverseMap();
        }
    }
}
