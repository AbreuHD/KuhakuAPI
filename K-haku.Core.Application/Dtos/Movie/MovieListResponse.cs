using K_haku.Core.Application.Dtos.Pages.Cuevana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Dtos.Movie
{
    public class MovieListResponse
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int ID { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public DateTime release_date { get; set; }
        public string title { get; set; }
        public string video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }

        public List<CuevanaMovieResponse> Cuevana { get; set; }
    }
}
