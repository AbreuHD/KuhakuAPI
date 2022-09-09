using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Dtos.Pages.Cuevana
{
    public class CuevanaMovieResponse
    {
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Link { get; set; }
        public string Age { get; set; }
        public bool Confirmed { get; set; }

        public string TMDBId { get; set; }
        public MovieListResponse Movie { get; set; }
    }
}
