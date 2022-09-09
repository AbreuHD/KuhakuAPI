using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.MovieList.Queries.GetAll
{
    public class GetAllMovieListParameters
    {
        public DateTime? ReleaseDate { get; set; }
        public string? MovieName { get; set; }
        public int? Skip { get; set; }
    }
}
