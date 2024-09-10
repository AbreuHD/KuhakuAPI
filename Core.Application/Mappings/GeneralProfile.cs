using AutoMapper;
using Core.Application.DTOs.Movies;
using Core.Application.DTOs.Relations;
using Core.Application.DTOs.Scraping;
using Core.Application.DTOs.TMDB;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;

namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Movie, TMDBResult>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.ID, opt => opt.Ignore());

            CreateMap<MovieWebDTO, MovieWeb>()
                .ReverseMap()
                .ForMember(x => x.TMDBTempID, opt => opt.Ignore());

            CreateMap<Movie_MovieWeb, Movie_MovieWebDTO>()
                .ReverseMap();

            CreateMap<Movie, PreviewSearchMovieDto>()
                .ReverseMap();
        }
    }
}
