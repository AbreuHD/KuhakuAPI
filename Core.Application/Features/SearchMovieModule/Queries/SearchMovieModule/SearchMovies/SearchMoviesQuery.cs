using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using MediatR;
using System.Diagnostics;
using System.Net;

namespace Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMovies
{
    public class SearchMoviesQuery : IRequest<GenericApiResponse<MovieSearchModuleDto>>
    {
        public string? Title { get; set; }
        public List<int>? Values { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }

    public class SearchMoviesQueryHandler(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper) : IRequestHandler<SearchMoviesQuery, GenericApiResponse<MovieSearchModuleDto>>
    {
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GenericApiResponse<MovieSearchModuleDto>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movies = await _movieRepository.SearchMovies(request.Title, request.Values, request.PageNumber, request.PageSize);

                var genreIds = movies
                    .SelectMany(m => m.GenreMovie ?? Enumerable.Empty<GenreMovie>())
                    .Select(gm => gm.GenreID)
                    .Distinct()
                    .ToList();

                var genres = await _genreRepository.GetAllByIdsAsync(genreIds);

                return new GenericApiResponse<MovieSearchModuleDto>
                {
                    Payload = new MovieSearchModuleDto
                    {
                        Movies = _mapper.Map<List<PreviewSearchMovieDto>>(movies),
                        Genres = _mapper.Map<List<TmdbGenreResponseDto>>(genres)
                    },
                    Message = $"{movies.Count} Movies found",
                    Success = true,
                    Statuscode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new GenericApiResponse<MovieSearchModuleDto>
                {
                    Payload = new MovieSearchModuleDto
                    {
                        Movies = []
                    },
                    Message = e.Message,
                    Success = false,
                    Statuscode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
