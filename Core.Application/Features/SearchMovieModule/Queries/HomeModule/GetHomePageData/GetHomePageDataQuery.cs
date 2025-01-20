using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Home;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using MediatR;

namespace Core.Application.Features.SearchMovieModule.Queries.HomeModule.GetHomePageData
{
    public class GetHomePageDataQuery : IRequest<GenericApiResponse<List<HomeDto>>>
    {
        public bool KidMode { get; set; } = false;
    }
    public class GetHomePageDataQueryHandler : IRequestHandler<GetHomePageDataQuery, GenericApiResponse<List<HomeDto>>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetHomePageDataQueryHandler(IMovieRepository movieRepository, IGenreRepository genre, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _genreRepository = genre;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<List<HomeDto>>> Handle(GetHomePageDataQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<List<HomeDto>>
            {
                Payload = []
            };

            var genres = await _genreRepository.GetAllWithIncludes(["Genre_Movie"]);

            foreach (var genre in genres)
            {
                var selectedMovies = new List<Movie>();
                foreach (var x in genre.Genre_Movie)
                {
                    var movieToAdd = await _movieRepository.GetByIdAsync(x.MovieID);

                    if (request.KidMode && movieToAdd.Adult is false || !request.KidMode)
                    {
                        selectedMovies.Add(movieToAdd);
                    }

                    if (selectedMovies.Count == 6) break;
                }

                if (genre.Genre_Movie.Count is not 0)
                {
                    response.Payload.Add(new HomeDto
                    {
                        Genre = _mapper.Map<TmdbGenreResponseDto>(genre),
                        Movies = _mapper.Map<List<PreviewSearchMovieDto>>(selectedMovies),
                    });
                }
            }
            return response;
        }
    }
}
