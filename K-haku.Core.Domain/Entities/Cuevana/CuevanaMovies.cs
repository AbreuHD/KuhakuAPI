using K_haku.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Domain.Entities.Cuevana
{
    public class CuevanaMovies : AuditableBaseMovies
    {
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Link { get; set; }
        public string Age { get; set; }
        public bool Confirmed { get; set; }
        
        public string TMDBId { get; set; }
        public MovieList Movie { get; set; }
    }
}
