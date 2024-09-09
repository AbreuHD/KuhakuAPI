using AutoMapper;
using Core.Application.DTOs.Genres;
using Core.Application.DTOs.Scraping;
using Core.Application.DTOs.TMDB;
using Core.Application.Enums;
using Core.Domain.Entities.GeneralMovie;
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
            TMDBAPIKEY = _configuration["TMDBAPIKey"] ?? "false";
        }

        public async Task<CheckedList> GetTMDBId(List<MovieWebDTO> movies)
        {
            List<Movie> CheckData = new List<Movie>();
            List<MovieWebDTO> MovieData = new List<MovieWebDTO>();
            foreach (var data in movies)
            {
                try
                {
                    string TMDBData = new WebClient().DownloadString($"https://api.themoviedb.org/3/search/movie?api_key={TMDBAPIKEY}&language=es-MX&query={data.Name}&include_adult=true");
                    var result = JsonConvert.DeserializeObject<TMDBResponse>(TMDBData);
                    TMDBResult tmdb = result.results.FirstOrDefault();
                    Console.WriteLine("Getting TMDB Data");
                    if (tmdb != null)
                    {
                        data.TMDBTempID = tmdb.ID;
                        data.Genres = tmdb.genre_ids;
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
                MovieWebDTO = MovieData
            };
        }

        public async Task<TmdbGenreResponseListDto> GetAllGenres()
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
