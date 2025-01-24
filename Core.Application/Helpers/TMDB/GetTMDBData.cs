using AutoMapper;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Scraping;
using Core.Application.DTOs.TMDB;
using Core.Domain.Entities.Movie;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace Core.Application.Helpers.TMDB
{
    public class GetTMDBData
    {
        public readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string TMDBAPIKEY;

        public GetTMDBData(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            TMDBAPIKEY = Environment.GetEnvironmentVariable("TMDBAPIKey") ?? _configuration["TMDBAPIKey"] ?? throw new ArgumentNullException(nameof(TMDBAPIKEY));
        }

        public CheckedList GetTMDBId(List<MovieWebDto> movies)
        {
            List<Movie> CheckData = [];
            List<MovieWebDto> MovieData = [];
            foreach (var data in movies)
            {
                try
                {
                    string TMDBData = new WebClient().DownloadString($"https://api.themoviedb.org/3/search/movie?api_key={TMDBAPIKEY}&language=es-MX&query={data.Name}&include_adult=true");
                    var result = JsonConvert.DeserializeObject<TmdbResponse>(TMDBData);
                    TmdbResult tmdb = result.Results.FirstOrDefault();
                    Console.WriteLine("Getting TMDB Data");
                    if (tmdb != null)
                    {
                        data.TMDBTempID = tmdb.ID;
                        data.Genres = tmdb.GenreIds;
                        var newMovie = _mapper.Map<Movie>(tmdb);
                        newMovie.TMDBID = tmdb.ID;
                        CheckData.Add(newMovie);
                    }
                    MovieData.Add(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return new CheckedList
            {
                Movies = CheckData,
                MovieWebDto = MovieData
            };
        }

        public TmdbGenreResponseListDto GetAllGenres()
        {
            var movies = new HttpClient().GetStringAsync($"https://api.themoviedb.org/3/genre/movie/list?api_key={TMDBAPIKEY}&language=en-us").Result;
            var series = new HttpClient().GetStringAsync($"https://api.themoviedb.org/3/genre/tv/list?api_key={TMDBAPIKEY}&language=en-us").Result;

            return new TmdbGenreResponseListDto
            {
                Movies = JsonConvert.DeserializeObject<TmdbGenreApiResponseDto>(movies).genres,
                Series = JsonConvert.DeserializeObject<TmdbGenreApiResponseDto>(series).genres
            };
        }
    }
}
