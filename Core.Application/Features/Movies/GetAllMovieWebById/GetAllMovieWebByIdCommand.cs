using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Scraping;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.WebScraping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Movies.GetAllMovieWebById
{
    public class GetAllMovieWebByIdCommand : IRequest<List<MovieWebDTO>>
    {
        public List<int> MovieWebId { get; set; }
    }

    public class GetAllMovieWebByIdCommandHandler : IRequestHandler<GetAllMovieWebByIdCommand, List<MovieWebDTO>>
    {
        private readonly IMovieWebRepository _movieWebRepository;
        private readonly IMapper _mapper;

        public GetAllMovieWebByIdCommandHandler(IMovieWebRepository movieWebRepository, IMapper mapper)
        {
            _movieWebRepository = movieWebRepository;
            _mapper = mapper;
        }

        public async Task<List<MovieWebDTO>> Handle(GetAllMovieWebByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new List<MovieWebDTO>();

            foreach (var x in request.MovieWebId)
            {
                response.Add(_mapper.Map<MovieWebDTO>(await _movieWebRepository.GetByIdAsync(x)));
            }

            return response;
        }
    }
}
