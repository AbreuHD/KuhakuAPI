using K_haku.Core.Application.Dtos.TMDB;
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
        public GetTMDBInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetTMDBId(string tittle)
        {
            try
            {
                var TMDBApiKey = _configuration.GetValue<string>("TMDBAPIKey");
                string TMDBData = new WebClient().DownloadString($"https://api.themoviedb.org/3/search/movie?api_key={TMDBApiKey}&query={tittle}");
                var result = JsonConvert.DeserializeObject<TMDBResponse>(TMDBData);
                return result.results.FirstOrDefault().id.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
