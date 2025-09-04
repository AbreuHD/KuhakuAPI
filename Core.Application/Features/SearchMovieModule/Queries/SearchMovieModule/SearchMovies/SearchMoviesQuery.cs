using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using MediatR;
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
            List<TmdbGenreResponseDto> genres = [];
            try
            {
                var movies = await _movieRepository.SearchMovies(request.Title, request.PageNumber, request.PageSize);

                if (request.Values != null && request.Values.Count > 0)
                {
                    foreach (var genreFilter in request.Values)
                    {
                        movies = movies.FindAll(x => x.GenreMovie != null && x.GenreMovie.Any(m => m.GenreID == genreFilter));
                    }
                }

                var genreIds = movies.SelectMany(movie => movie.GenreMovie ?? Enumerable.Empty<GenreMovie>())
                                     .Select(genre => genre.GenreID)
                                     .Distinct();

                foreach (var genreId in genreIds)
                {
                    var requestGenre = await _genreRepository.GetByIdAsync(genreId);
                    genres.Add(new TmdbGenreResponseDto
                    {
                        Id = requestGenre.ID,
                        Name = requestGenre.Name
                    });
                }

                return new GenericApiResponse<MovieSearchModuleDto>
                {
                    Payload = new MovieSearchModuleDto
                    {
                        Movies = _mapper.Map<List<PreviewSearchMovieDto>>(movies),
                        Genres = genres
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
                        Movies = new List<PreviewSearchMovieDto>()
                    },
                    Message = e.Message,
                    Success = false,
                    Statuscode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
