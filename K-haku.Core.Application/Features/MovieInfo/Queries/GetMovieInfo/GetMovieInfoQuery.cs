using AutoMapper;
using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.MovieInfo.Queries.GetMovieInfo
{
    public class GetMovieInfoQuery : IRequest<MovieInfoResponse>
    {
        public string TMBDId { get; set; }
    }

    public class GetMovieInfoQueryHandler : IRequestHandler<GetMovieInfoQuery, MovieInfoResponse>
    {
        ICuevanaMoviesRepository _cuevanaMoviesRepository;
        IMovieListRepository _movieListRepository;
        IMapper _mapper;
        public GetMovieInfoQueryHandler(ICuevanaMoviesRepository cuevanaMoviesRepository, IMovieListRepository movieListRepository, IMapper mapper)
        {
            _cuevanaMoviesRepository = cuevanaMoviesRepository;
            _movieListRepository = movieListRepository;
            _mapper = mapper;
        }
        
        public async Task<MovieInfoResponse> Handle(GetMovieInfoQuery request, CancellationToken cancellationToken)
        {
            MovieInfoResponse MovieResponse = _mapper.Map<MovieInfoResponse>(await _movieListRepository.GetByStringIdAsync(request.TMBDId));
            var movie = await _cuevanaMoviesRepository.GetByTMDBIdAsync(request.TMBDId);
            MovieResponse.MovieLinks = new();
            MovieResponse.MovieLinks.Add(movie);
            return MovieResponse;
        }
    }
}
