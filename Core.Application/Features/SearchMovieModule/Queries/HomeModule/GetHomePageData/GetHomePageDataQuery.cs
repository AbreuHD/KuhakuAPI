using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Home;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Core.Application.Features.SearchMovieModule.Queries.HomeModule.GetHomePageData
{
    public class GetHomePageDataQuery : IRequest<GenericApiResponse<List<HomeDto>>>
    {
        public bool KidMode { get; set; } = false;
    }
    public class GetHomePageDataQueryHandler(IMovieRepository movieRepository, IMapper mapper) : IRequestHandler<GetHomePageDataQuery, GenericApiResponse<List<HomeDto>>>
    {
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly IMapper _mapper = mapper;

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
                var genreMovies = await _movieRepository.GetMoviesByGenresAsync(request.KidMode, 11);

                foreach (var (genre, movies) in genreMovies)
                {
                    response.Payload.Add(new HomeDto
                    {
                        Genre = _mapper.Map<TmdbGenreResponseDto>(genre),
                        Movies = _mapper.Map<List<PreviewSearchMovieDto>>(movies),
                    });
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
