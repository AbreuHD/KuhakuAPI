using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Movies;
using Core.Application.Features.GenreModule.Commands.GetGenresFromAMovie;
using Core.Application.Features.Movies.GetAllMovieWebById;
using Core.Application.Interface.Repositories;
using MediatR;
using System.Net;

namespace Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMovieInfo
{
    public class SearchMovieInfoQuery : IRequest<GenericApiResponse<InfoSearchMovieDto>>
    {
        public int MovieId { get; set; }
    }

    public class SearchMovieInfoQueryHandler(IMovieRepository movieRepository, IMapper mapper, IMediator mediator) : IRequestHandler<SearchMovieInfoQuery, GenericApiResponse<InfoSearchMovieDto>>
    {
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public async Task<GenericApiResponse<InfoSearchMovieDto>> Handle(SearchMovieInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _movieRepository.GetMovieInfo(request.MovieId);
                var response = _mapper.Map<InfoSearchMovieDto>(data);
                response.Genres = await _mediator.Send(new GetGenresFromAMovieCommand { Genres = data.GenreMovie!.Select(x => x.GenreID).ToList() }, cancellationToken);
                response.Source = await _mediator.Send(new GetAllMovieWebByIdCommand { MovieWebId = data.MovieMovieWeb!.Select(x => x.MovieWebID).ToList() }, cancellationToken);

                return new GenericApiResponse<InfoSearchMovieDto>
                {
                    Payload = response,
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
                    Payload = new InfoSearchMovieDto { Title = string.Empty }
                };
            }
        }
    }
}
