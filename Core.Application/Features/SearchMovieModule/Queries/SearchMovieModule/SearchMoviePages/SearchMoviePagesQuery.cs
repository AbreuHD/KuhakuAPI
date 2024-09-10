using Core.Application.DTOs.General;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using MediatR;

namespace Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMoviePages
{
    public class SearchMoviePagesQuery : IRequest<GenericApiResponse<MoviePageResponseDTO>>
    {
        public int MovieId { get; set; }
    }

    public class SearchMoviePagesQueryHandler : IRequestHandler<SearchMoviePagesQuery, GenericApiResponse<MoviePageResponseDTO>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieWebRepository _movieWebRepository;

        public SearchMoviePagesQueryHandler(IMovieRepository movieRepository, IMovieWebRepository movieWebRepository)
        {
            _movieRepository = movieRepository;
            _movieWebRepository = movieWebRepository;
        }

        public async Task<GenericApiResponse<MoviePageResponseDTO>> Handle(SearchMoviePagesQuery request, CancellationToken cancellationToken)
        {
            var data = await _movieRepository.GetMovieWebPage(request.MovieId);
            throw new NotImplementedException();
        }
    }
}
