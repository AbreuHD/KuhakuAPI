using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
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
            List<TmdbGenreResponseDto> genres = new();
            try
            {
                var movies = await _movieRepository.SearchMovies(request.Title);

                if (request.Values != null && request.Values.Count > 0)
                {
                    foreach (var genreFilter in request.Values)
                    {
                        movies = movies.FindAll(x => x.Genre_Movie.Any(m => m.GenreID == genreFilter));
                    }
                }

                foreach (var movie in movies)
                {
                    foreach (var genre in movie.Genre_Movie)
                    {
                        if (genres.Find(x => x.Id == genre.GenreID) == null)
                        {
                            var requestGenre = await _genreRepository.GetByIdAsync(genre.GenreID);
                            genres.Add(new TmdbGenreResponseDto
                            {
                                Id = requestGenre.ID,
                                Name = requestGenre.Name
                            });
                        }
                    }
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
