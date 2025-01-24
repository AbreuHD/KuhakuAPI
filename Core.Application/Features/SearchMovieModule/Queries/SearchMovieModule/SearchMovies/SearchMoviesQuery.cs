using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using MediatR;
using System.Net;

namespace Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMovies
{
    public class SearchMoviesQuery : IRequest<GenericApiResponse<MovieSearchModuleDto>>
    {
        public string Title { get; set; }
        public List<int>? Values { get; set; }
    }

    public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, GenericApiResponse<MovieSearchModuleDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public SearchMoviesQueryHandler(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<MovieSearchModuleDto>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
        {
            List<TmdbGenreResponseDto> genres = [];
            try
            {
                var movies = await _movieRepository.SearchMovies(request.Title);

                if (request.Values != null && request.Values.Count > 0)
                {
                    foreach (var genreFilter in request.Values)
                    {
                        movies = movies.FindAll(x => x.GenreMovie.Any(m => m.GenreID == genreFilter));
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
                    Payload = null,
                    Message = e.Message,
                    Success = false,
                    Statuscode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
