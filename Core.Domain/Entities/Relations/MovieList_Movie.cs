using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.UserThings;

namespace Core.Domain.Entities.Relations
{
    public class MovieList_Movie : AuditableBase
    {
        public int MovieListID { get; set; }
        public int MovieID { get; set; }

        public Movie Movie { get; set; }
        public MovieList MovieList { get; set; }
    }
}
