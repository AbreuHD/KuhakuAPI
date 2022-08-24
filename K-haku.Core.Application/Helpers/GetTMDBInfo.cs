using AutoMapper;
using K_haku.Core.Application.Dtos.TMDB;
using K_haku.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace K_haku.Core.Application.Helpers
{
    public class GetTMDBInfo
    {
        public readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public GetTMDBInfo(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<MovieList> GetTMDBId(string tittle)
        {
            try
            {
                var TMDBApiKey = _configuration.GetValue<string>("TMDBAPIKey");
                string TMDBData = new WebClient().DownloadString($"https://api.themoviedb.org/3/search/movie?api_key={TMDBApiKey}&query={tittle}");
                var result = JsonConvert.DeserializeObject<TMDBResponse>(TMDBData);
                TMDBResult tmdb = result.results.FirstOrDefault();
                return _mapper.Map<MovieList>(tmdb);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /*public async Task<MovieList> GetTMDBMovieInfo(string ID)
        {
            try
            {
                var TMDBApiKey = _configuration.GetValue<string>("TMDBAPIKey");
                string TMDBData = new WebClient().DownloadString($"https://api.themoviedb.org/3/movie/{ID}?api_key={TMDBApiKey}");
                var result = JsonConvert.DeserializeObject<TMDBResult>(TMDBData);
                return _mapper.Map<MovieList>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }*/

    }
}
