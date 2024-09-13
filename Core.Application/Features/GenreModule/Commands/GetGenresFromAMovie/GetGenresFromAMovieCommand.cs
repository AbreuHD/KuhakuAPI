using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.GenreModule.Commands.GetGenresFromAMovie
{
    public class GetGenresFromAMovieCommand : IRequest<List<TmdbGenreResponseDto>>
    {
        public required List<int> Genres { get; set; }
    }
    public class GetGenresFromAMovieCommandHandler : IRequestHandler<GetGenresFromAMovieCommand, List<TmdbGenreResponseDto>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetGenresFromAMovieCommandHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<List<TmdbGenreResponseDto>> Handle(GetGenresFromAMovieCommand request, CancellationToken cancellationToken)
        {
            var response = new List<TmdbGenreResponseDto>();
            foreach (var genre in request.Genres)
            {
                response.Add(_mapper.Map<TmdbGenreResponseDto>(await _genreRepository.GetByIdAsync(genre)));
            }
            return response;
        }
    }
}
