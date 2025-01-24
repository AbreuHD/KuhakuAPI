using AutoMapper;
using Core.Application.DTOs.Scraping;
using Core.Application.Interface.Repositories;
using MediatR;

namespace Core.Application.Features.Movies.GetAllMovieWebById
{
    public class GetAllMovieWebByIdCommand : IRequest<List<MovieWebDto>>
    {
        public required List<int> MovieWebId { get; set; }
    }

    public class GetAllMovieWebByIdCommandHandler(IMovieWebRepository movieWebRepository, IMapper mapper) : IRequestHandler<GetAllMovieWebByIdCommand, List<MovieWebDto>>
    {
        private readonly IMovieWebRepository _movieWebRepository = movieWebRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<MovieWebDto>> Handle(GetAllMovieWebByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new List<MovieWebDto>();

            foreach (var x in request.MovieWebId)
            {
                response.Add(_mapper.Map<MovieWebDto>(await _movieWebRepository.GetByIdAsync(x)));
            }

            return response;
        }
    }
}
