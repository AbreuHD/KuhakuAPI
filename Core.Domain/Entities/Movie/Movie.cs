using Core.Domain.Common;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.UserThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.GeneralMovie
{
    public class Movie : AuditableBase
    {
        public string Title { get; set; }
        public string Original_title { get; set; }
        public bool Adult { get; set; }
        public double Vote_average { get; set; }
        public int Vote_count { get; set; }
        public string Overview { get; set; }
        public string Video { get; set; }
        public string Poster_path { get; set; }
        public string Backdrop_path { get; set; }
        public DateTime Release_date { get; set; }

        public ICollection<Genre_Movie> Genre_Movie { get; set; }
        public ICollection<Recents> Recents { get; set; }
        public ICollection<MovieList_Movie> MovieList_Movie { get; set; }
        public ICollection<Movie_MovieWeb> Movie_MovieWeb { get; set; }

    }
}
