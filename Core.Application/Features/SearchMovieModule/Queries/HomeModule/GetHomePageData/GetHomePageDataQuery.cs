using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Home;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using MediatR;
using Microsoft.AspNetCore.Http;

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
                Success = true,
                Statuscode = StatusCodes.Status200OK,
                Message = "Successfully retrieved data",
                Payload = []
            };
            try
            {
                var genres = await _genreRepository.GetAllWithIncludes(["GenreMovie"]);

                foreach (var genre in genres)
                {
                    var selectedMovies = new List<Movie>();
                    foreach (var x in genre.GenreMovie ?? [])
                    {
                        var movieToAdd = await _movieRepository.GetByIdAsync(x.MovieID);

                        if (request.KidMode && movieToAdd.Adult is false || !request.KidMode)
                        {
                            selectedMovies.Add(movieToAdd);
                        }

                        if (selectedMovies.Count == 6) break;
                    }

                    if (genre.GenreMovie?.Count is not 0)
                    {
                        response.Payload.Add(new HomeDto
                        {
                            Genre = _mapper.Map<TmdbGenreResponseDto>(genre),
                            Movies = _mapper.Map<List<PreviewSearchMovieDto>>(selectedMovies),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Statuscode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }
    }
}
