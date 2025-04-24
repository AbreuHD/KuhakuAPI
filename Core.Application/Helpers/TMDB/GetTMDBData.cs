using AutoMapper;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Scraping;
using Core.Application.DTOs.TMDB;
using Core.Application.Helpers.Logger;
using Core.Domain.Entities.Movie;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Core.Application.Helpers.TMDB
{
    public class GetTmdbData
    {
        public readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string TMDBAPIKEY;

        public GetTmdbData(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            TMDBAPIKEY = Environment.GetEnvironmentVariable("TMDBAPIKey") ?? _configuration["TMDBAPIKey"] ?? throw new ArgumentNullException(nameof(configuration), "TMDBAPIKey is not set.");
        }

        public async Task<CheckedList> GetTMDBIdAsync(List<MovieWebDto> movies)
        {
            List<Movie> CheckData = [];
            List<MovieWebDto> MovieData = [];
            string Message;

            foreach (var data in movies)
            {
                try
                {
                    Message = $"Getting TMDB data for {data.Name}";
                    LoggerHelper.CustomLog(CustomLogLevel.Scraping, Message, LogLevels.Information);

                    using var client = new HttpClient();
                    var response = await client.GetStringAsync($"https://api.themoviedb.org/3/search/movie?api_key={TMDBAPIKEY}&language=es-MX&query={data.Name}&include_adult=true");
                    var result = JsonConvert.DeserializeObject<TmdbResponse>(response);
                    if (result?.Results != null)
                    {
                        TmdbResult tmdb = result.Results.FirstOrDefault()!;

                        Message = $"TMDB data found for {data.Name}";
                        LoggerHelper.CustomLog(CustomLogLevel.Scraping, Message, LogLevels.Information);
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
                }
                catch (Exception ex)
                {
                    Message = $"Error getting TMDB data for {data.Name}";
                    LoggerHelper.CustomLog(CustomLogLevel.Scraping, Message, LogLevels.Error, ex.Message);
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

            var movieGenres = JsonConvert.DeserializeObject<TmdbGenreApiResponseDto>(movies)?.genres ?? [];
            var seriesGenres = JsonConvert.DeserializeObject<TmdbGenreApiResponseDto>(series)?.genres ?? [];

            return new TmdbGenreResponseListDto
            {
                Movies = movieGenres,
                Series = seriesGenres
            };
        }
    }
}
