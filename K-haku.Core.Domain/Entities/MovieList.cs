using K_haku.Core.Domain.Entities.Cuevana;
using K_haku.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Domain.Entities
{
    public class MovieList
    {
        public string ID { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<Genre> genre_ids { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public DateTime release_date { get; set; }
        public string title { get; set; }
        public string video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedby { get; set; }
        public DateTime? LastModified { get; set; }

        public ICollection<CuevanaMovies> Cuevana { get; set; }
    }
}
