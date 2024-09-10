using AutoMapper;
using Core.Application.DTOs.Relations;
using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;
using MediatR;

namespace Core.Application.Features.Scraping.Cuevana.Cuevana3.ch.Commands.GetAllCuevanaMovies
{
    public class GetAllCuevanaMoviesCommand : IRequest<bool>
    {

    }

    public class GetAllCuevanaMoviesCommandHandler : IRequestHandler<GetAllCuevanaMoviesCommand, bool>
    {
        private readonly IMovieWebRepository _movieWebRepository;
        private readonly IMovie_MovieWebRepository _movie_MovieWebRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IGenre_MovieRepository _genre_MovieRepository;
        private readonly IGenreRepository _genreRepository;

        private readonly GetTMDBData _getTMDBData;
        private readonly IMapper _mapper;

        public GetAllCuevanaMoviesCommandHandler(IMovieWebRepository movieWebRepository, IMovie_MovieWebRepository movie_MovieWebRepository, IMovieRepository movieRepository, GetTMDBData getTMDBData, IGenre_MovieRepository genre_MovieRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _movieWebRepository = movieWebRepository;
            _movie_MovieWebRepository = movie_MovieWebRepository;
            _movieRepository = movieRepository;
            _getTMDBData = getTMDBData;
            _genre_MovieRepository = genre_MovieRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(GetAllCuevanaMoviesCommand request, CancellationToken cancellationToken)
        {
            var _cuevanaService = new Services.WebScrapers.MovieESWebsites.Cuevana.Cuevana3.ch.Cuevana3CHServices(1, "https://cuevana3.ch");

            var pagination = await _cuevanaService.GetCuevana3Pagination();
            while (pagination > 0)
            {
                Console.WriteLine($"Paginacion {pagination}");

                var movies = await _cuevanaService.GetCuevana3(pagination);
                if (movies != null)
                {
                    var data = await _getTMDBData.GetTMDBId(movies);
                    List<Movie> uniqueMovies = data.Movies.GroupBy(m => m.TMDBID).Select(g => g.First()).ToList();
                    await _movieRepository.AddAllAsync(await _movieRepository.Exist(uniqueMovies));
                    var movieWeb = await _movieWebRepository.Exist(data.MovieWebDTO);

                    foreach (var movie in movieWeb)
                    {
                        var movieWebAdd = await _movieWebRepository.AddAsync(_mapper.Map<MovieWeb>(movie));
                        var movieRepositoryId = await _movieRepository.GetIdByTmdbId(movie.TMDBTempID);

                        await _movie_MovieWebRepository.AddAsync(new Movie_MovieWeb
                        {
                            MovieID = movieRepositoryId,
                            MovieWebID = movieWebAdd.ID,
                            Verified = false
                        });

                        if(movie.Genres != null)
                        {
                            foreach (var genre in movie.Genres)
                            {
                                var genreId = await _genreRepository.GetIdByTmdbId(genre);
                                await _genre_MovieRepository.AddAsync(new Genre_Movie
                                {
                                    MovieID = movieRepositoryId,
                                    GenreID = genreId
                                });
                            }
                        }
                    }
                }
                pagination--;
            }

            return true;
        }
    }
}
