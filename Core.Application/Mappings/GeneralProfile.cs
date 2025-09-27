using AutoMapper;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;
using Core.Application.DTOs.Relations;
using Core.Application.DTOs.Scraping;
using Core.Application.DTOs.ShareList;
using Core.Application.DTOs.TMDB;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.UserThings;
using Core.Domain.Entities.WebScraping;
using Microsoft.FSharp.Collections;

namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<TmdbResult, Movie>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(dest => dest.TMDBID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Original_title, opt => opt.MapFrom(src => src.OriginalTitle))
                .ForMember(dest => dest.Vote_average, opt => opt.MapFrom(src => src.VoteAverage))
                .ForMember(dest => dest.Vote_count, opt => opt.MapFrom(src => src.VoteCount))
                .ForMember(dest => dest.Poster_path, opt => opt.MapFrom(src => src.PosterPath))
                .ForMember(dest => dest.Backdrop_path, opt => opt.MapFrom(src => src.BackdropPath))
                .ForMember(dest => dest.Release_date, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.ReleaseDate)
                        ? (DateTime?)null
                        : DateTime.Parse(src.ReleaseDate)))
                .ReverseMap()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.TMDBID))
                .ForMember(dest => dest.OriginalTitle, opt => opt.MapFrom(src => src.Original_title))
                .ForMember(dest => dest.VoteAverage, opt => opt.MapFrom(src => src.Vote_average))
                .ForMember(dest => dest.VoteCount, opt => opt.MapFrom(src => src.Vote_count))
                .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(src => src.Poster_path))
                .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.Backdrop_path))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src =>
                    src.Release_date.HasValue
                        ? src.Release_date.Value.ToString("yyyy-MM-dd")
                        : null));

            CreateMap<MovieWebDto, MovieWeb>()
                .ReverseMap()
                .ForMember(x => x.TMDBTempID, opt => opt.Ignore());

            CreateMap<MovieMovieWeb, MovieMovieWebDto>()
                .ReverseMap();

            CreateMap<Movie, PreviewSearchMovieDto>()
                .ReverseMap();

            CreateMap<Movie, InfoSearchMovieDto>()
                .ReverseMap();

            CreateMap<MovieWeb, MovieWebDto>()
                .ReverseMap();

            CreateMap<Genre, TmdbGenreResponseDto>()
                .ReverseMap();

            CreateMap<ShareList, PreviewShareListDto>()
                .ForMember(dest => dest.Movies,
                           opt => opt.MapFrom(src => src.MovieListMovie != null
                                ? src.MovieListMovie
                                      .Select(mlm => mlm.Movie)
                                      .OfType<Movie>()
                                      .ToList()
                                : new List<Movie>()));
        }
    }
}
