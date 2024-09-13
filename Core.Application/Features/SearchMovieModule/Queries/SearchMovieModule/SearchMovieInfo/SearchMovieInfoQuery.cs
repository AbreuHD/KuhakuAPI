using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Movies;
using Core.Application.Features.GenreModule.Commands.GetGenresFromAMovie;
using Core.Application.Features.Movies.GetAllMovieWebById;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using MediatR;
using System.Net;

namespace Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMoviePages
{
    public class SearchMovieInfoQuery : IRequest<GenericApiResponse<InfoSearchMovieDto>>
    {
        public int MovieId { get; set; }
    }

    public class SearchMovieInfoQueryHandler : IRequestHandler<SearchMovieInfoQuery, GenericApiResponse<InfoSearchMovieDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieWebRepository _movieWebRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SearchMovieInfoQueryHandler(IMovieRepository movieRepository, IMovieWebRepository movieWebRepository, IMapper mapper, IMediator mediator)
        {
            _movieRepository = movieRepository;
            _movieWebRepository = movieWebRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<GenericApiResponse<InfoSearchMovieDto>> Handle(SearchMovieInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _movieRepository.GetMovieInfo(request.MovieId);
                var response = _mapper.Map<InfoSearchMovieDto>(data);
                response.Genres = await _mediator.Send(new GetGenresFromAMovieCommand { Genres = data.Genre_Movie.Select(x => x.GenreID).ToList() });
                response.Source = await _mediator.Send(new GetAllMovieWebByIdCommand { MovieWebId = data.Movie_MovieWeb.Select(x => x.MovieWebID).ToList() });

                return new GenericApiResponse<InfoSearchMovieDto>
                {
                    Payload = _mapper.Map<InfoSearchMovieDto>(response),
                    Message = "OK",
                    Success = true,
                    Statuscode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new GenericApiResponse<InfoSearchMovieDto>
                {
                    Success = false,
                    Message = ex.Message,
                    Statuscode = (int)HttpStatusCode.InternalServerError,
                    Payload = null
                };
            }
        }
    }
}
