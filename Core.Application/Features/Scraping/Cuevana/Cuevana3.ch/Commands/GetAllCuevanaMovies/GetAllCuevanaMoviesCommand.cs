using AutoMapper;
using Core.Application.Helpers.Logger;
using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Application.Features.Scraping.Cuevana.Cuevana3.ch.Commands.GetAllCuevanaMovies
{
    public class GetAllCuevanaMoviesCommand : IRequest<bool>
    {

    }

    public class GetAllCuevanaMoviesCommandHandler(IMovieWebRepository movieWebRepository, IMovie_MovieWebRepository movie_MovieWebRepository, IMovieRepository movieRepository, GetTmdbData getTmdbData, IGenre_MovieRepository genre_MovieRepository, IGenreRepository genreRepository, IMapper mapper) : IRequestHandler<GetAllCuevanaMoviesCommand, bool>
    {
        private readonly IMovieWebRepository _movieWebRepository = movieWebRepository;
        private readonly IMovie_MovieWebRepository _movie_MovieWebRepository = movie_MovieWebRepository;
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly IGenre_MovieRepository _genre_MovieRepository = genre_MovieRepository;
        private readonly IGenreRepository _genreRepository = genreRepository;

        private readonly GetTmdbData _getTmdbData = getTmdbData;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Handle(GetAllCuevanaMoviesCommand request, CancellationToken cancellationToken)
        {
            LoggerHelper.CustomLog(CustomLogLevel.Scraping, "Starting Scraping From Cuevana.biz", LogLevels.Information);

            var _cuevanaService = new Services.WebScrapers.MovieESWebsites.Cuevana.Cuevana3Services(1, "https://cuevana.biz");

            var pagination = _cuevanaService.GetPagination();
            while (pagination > 0)
            {
                var movieList = _cuevanaService.GetMoviesFromPage(pagination);
                movieList = await _movieWebRepository.Exist(movieList);
                if (movieList != null)
                {
                    var data = await _getTmdbData.GetTMDBIdAsync(movieList);
                    List<Movie> uniqueMovies = [.. data.Movies.GroupBy(m => m.TMDBID).Select(g => g.First())];
                    await _movieRepository.AddAllAsync(await _movieRepository.Exist(uniqueMovies));
                    

                    foreach (var movie in movieList)
                    {
                        var movieWebAdd = await _movieWebRepository.AddAsync(_mapper.Map<MovieWeb>(movie));
                        var movieRepositoryId = await _movieRepository.GetIdByTmdbId(movie.TMDBTempID);
                        if (movieRepositoryId == 0)
                        {
                            continue;
                        }
                        await _movie_MovieWebRepository.AddAsync(new MovieMovieWeb
                        {
                            MovieID = movieRepositoryId,
                            MovieWebID = movieWebAdd.ID,
                            Verified = false
                        });

                        if (movie.Genres != null)
                        {
                            var Message = $"Adding Genres for {movie.Name}";
                            LoggerHelper.CustomLog(CustomLogLevel.Scraping, Message, LogLevels.Information);
                            foreach (var genre in movie.Genres)
                            {
                                var genreId = await _genreRepository.GetIdByTmdbId(genre);
                                await _genre_MovieRepository.AddAsync(new GenreMovie
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
