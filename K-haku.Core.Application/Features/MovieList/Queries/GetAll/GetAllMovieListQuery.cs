using AutoMapper;
using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace K_haku.Core.Application.Features.MovieList.Queries.GetAll
{
    public class GetAllMovieListQuery : IRequest<List<MovieListResponse>>
    {
        /// <example>
        /// 10/06/2022
        /// </example>
        [SwaggerParameter(Description = "Movie Release Date")]
        public DateTime? ReleaseDate { get; set; }

        /// <example>
        /// Avengers
        /// </example>
        [SwaggerParameter(Description = "Movie Name")]
        public string? MovieName { get; set; }

        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description = "Movie List Page")]
        public int? Skip { get; set; }
    }

    public class GetAllMovieListQueryHandler : IRequestHandler<GetAllMovieListQuery, List<MovieListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieListRepository _movieListRepository;
        public GetAllMovieListQueryHandler(IMapper mapper, IMovieListRepository movieListRepository)
        {
            _mapper = mapper;
            _movieListRepository = movieListRepository;
        }
        public async Task<List<MovieListResponse>> Handle(GetAllMovieListQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<GetAllMovieListParameters>(request);
            var movieList = await GetAllWithIncludes(parameters);
            return movieList;
        }

        public async Task<List<MovieListResponse>> GetAllWithIncludes(GetAllMovieListParameters parameters)
        {
            List<Domain.Entities.MovieList> movies;
            if (parameters.Skip != null)
            {
                parameters.Skip = parameters.Skip * 30;

            }
            if(parameters.Skip == null)
            {
                parameters.Skip = 30;
            }
            movies = await _movieListRepository.GetAllMoviListWithNames(parameters.Skip.Value, parameters.MovieName);


            if (parameters.ReleaseDate != null)
            {
                movies = movies.Where(x => x.release_date.Year == parameters.ReleaseDate.Value.Year).ToList();
            }

            return _mapper.Map<List<MovieListResponse>>(movies);

        }
    }

}
