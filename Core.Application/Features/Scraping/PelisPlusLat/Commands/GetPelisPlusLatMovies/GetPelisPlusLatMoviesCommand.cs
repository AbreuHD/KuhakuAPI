using AutoMapper;
using Core.Application.DTOs.Relations;
using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Features.Scraping.PelisPlusLat.Commands.GetPelisPlusLatMovies
{
    public class GetPelisPlusLatMoviesCommand : IRequest<bool>
    {
    }
    public class GetPelisPlusLatMoviesCommandHandler(
        IMovieWebRepository movieWebRepository, 
        IMovie_MovieWebRepository movie_MovieWebRepository, 
        IMovieRepository movieRepository, 
        GetTmdbData getTmdbData, 
        ILogger<GetPelisPlusLatMoviesCommandHandler> logger,
        IMapper mapper) : IRequestHandler<GetPelisPlusLatMoviesCommand, bool>
    {
        private readonly int DB_WEB_ID = 1;
        private readonly string ORIGINAL_URI = "https://www12.pelisplushd.lat";
        private readonly IMovieWebRepository _movieWebRepository = movieWebRepository;
        private readonly IMovie_MovieWebRepository _movie_MovieWebRepository = movie_MovieWebRepository;
        private readonly IMovieRepository _movieRepository = movieRepository;
        private readonly GetTmdbData _getTMDBData = getTmdbData;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<GetPelisPlusLatMoviesCommandHandler> _logger = logger;

        public async Task<bool> Handle(GetPelisPlusLatMoviesCommand request, CancellationToken cancellationToken)
        {
            var PelisPlusLatMovies = new Services.WebScrapers.MovieESWebsites.PelisPluslat.GetPelisPlusLatMovies(DB_WEB_ID, ORIGINAL_URI);
            try
            {
                int count = PelisPlusLatMovies.GetPelisplushdPagination();
                int i = 0;
                while (i <= count)
                {
                    i++;
                    Console.WriteLine($"Paginacion {i}");
                    List<MovieMovieWebDto> relations = [];
                    var movieRaw = PelisPlusLatMovies.GetPelisplushd(i);
                    if (movieRaw != null)
                    {
                        var data = _getTMDBData.GetTMDBId(movieRaw);
                        List<Movie> uniqueMovies = data.Movies.GroupBy(m => m.TMDBID).Select(g => g.First()).ToList();
                        await _movieRepository.AddAllAsync(await _movieRepository.Exist(uniqueMovies)); //Movie Added if not exist
                        var movies = await _movieWebRepository.Exist(data.MovieWebDto);
                        foreach (var movie in movies)
                        {
                            var movieWeb = await _movieWebRepository.AddAsync(_mapper.Map<MovieWeb>(movie)); //MovieWeb Added if not exist
                            relations.Add(new MovieMovieWebDto
                            {
                                MovieID = movie.TMDBTempID,
                                MovieWebID = movieWeb.ID,
                                Verified = false
                            });
                        }
                        var dataMovieEndRelations = await _movieRepository.GetId(_mapper.Map<List<MovieMovieWeb>>(relations));
                        foreach (var endRelations in dataMovieEndRelations)
                        {
                            try
                            {
                                if (endRelations.MovieID != 0)
                                {
                                    await _movie_MovieWebRepository.AddAsync(endRelations); //Movie_MovieWeb Added
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Error adding movie relation for MovieID {MovieID}", endRelations.MovieID);
                            }
                        }
                    }

                }
                ;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
