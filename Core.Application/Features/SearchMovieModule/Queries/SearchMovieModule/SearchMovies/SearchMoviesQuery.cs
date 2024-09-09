using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.Movies;
using Core.Application.Interface.Repositories;
using MediatR;

namespace Core.Application.Features.SearchMovieModule.Queries.SearchMovieModule.SearchMovies
{
    public class SearchMoviesQuery : IRequest<GenericApiResponse<List<PreviewSearchMovieDTO>>>
    {
        public string Title { get; set; }
        public List<int>? Values { get; set; }
    }

    public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, GenericApiResponse<List<PreviewSearchMovieDTO>>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public SearchMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<List<PreviewSearchMovieDTO>>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
        {
            GenericApiResponse<List<PreviewSearchMovieDTO>> response = new();
            response.Payload = new List<PreviewSearchMovieDTO>();
            var movies = await _movieRepository.SearchMovies(request.Title, request.Values);
            response.Payload = _mapper.Map<List<PreviewSearchMovieDTO>>(movies.Item1);
            return response;
        }

    }

}
