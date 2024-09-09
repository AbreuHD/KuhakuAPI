using AutoMapper;
using Core.Application.DTOs.Relations;
using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;
using MediatR;

namespace Core.Application.Features.Scraping.PelisPlusLat.Commands.GetPelisPlusLatMovies
{
    public class GetPelisPlusLatMoviesCommand : IRequest<bool>
    {
    }
    public class GetPelisPlusLatMoviesCommandHandler : IRequestHandler<GetPelisPlusLatMoviesCommand, bool>
    {
        private int DB_WEB_ID = 1;
        private string ORIGINAL_URI = "https://www12.pelisplushd.lat";
        private readonly IMovieWebRepository _movieWebRepository;
        private readonly IMovie_MovieWebRepository _movie_MovieWebRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly GetTMDBData _getTMDBData;
        private readonly IMapper _mapper;

        public GetPelisPlusLatMoviesCommandHandler(IMovieWebRepository movieWebRepository, IMovie_MovieWebRepository movie_MovieWebRepository, IMovieRepository movieRepository, GetTMDBData getTMDBData, IMapper mapper)
        {
            _movieWebRepository = movieWebRepository;
            _movie_MovieWebRepository = movie_MovieWebRepository;
            _movieRepository = movieRepository;
            _getTMDBData = getTMDBData;
            _mapper = mapper;
        }

        public async Task<bool> Handle(GetPelisPlusLatMoviesCommand request, CancellationToken cancellationToken)
        {
            var PelisPlusLatMovies = new Services.WebScrapers.MovieESWebsites.PelisPluslat.GetPelisPlusLatMovies(DB_WEB_ID, ORIGINAL_URI);
            try
            {
                int count = await PelisPlusLatMovies.GetPelisplushdPagination();
                int i = 0;
                while (i <= count)
                {
                    i++;
                    Console.WriteLine($"Paginacion {i}");
                    List<Movie_MovieWebDTO> relations = new List<Movie_MovieWebDTO>();
                    var movieRaw = await PelisPlusLatMovies.GetPelisplushd(i);
                    if (movieRaw != null)
                    {
                        var data = await _getTMDBData.GetTMDBId(movieRaw);
                        List<Movie> uniqueMovies = data.Movies.GroupBy(m => m.TMDBID).Select(g => g.First()).ToList();
                        await _movieRepository.AddAllAsync(await _movieRepository.Exist(uniqueMovies)); //Movie Added if not exist
                        var movies = await _movieWebRepository.Exist(data.MovieWebDTO);
                        foreach (var movie in movies)
                        {
                            var movieWeb = await _movieWebRepository.AddAsync(_mapper.Map<MovieWeb>(movie)); //MovieWeb Added if not exist
                            relations.Add(new Movie_MovieWebDTO
                            {
                                MovieID = movie.TMDBTempID,
                                MovieWebID = movieWeb.ID,
                                Verified = false
                            });
                        }
                        var dataMovieEndRelations = await _movieRepository.GetId(_mapper.Map<List<Movie_MovieWeb>>(relations));
                        foreach (var endRelations in dataMovieEndRelations)
                        {
                            try
                            {
                                if (endRelations.MovieID != 0 && endRelations.MovieID != null)
                                {
                                    await _movie_MovieWebRepository.AddAsync(endRelations); //Movie_MovieWeb Added
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }

                }
                ;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
