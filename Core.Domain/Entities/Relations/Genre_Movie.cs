using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;

namespace Core.Domain.Entities.Relations
{
    public class Genre_Movie : AuditableBase
    {
        public int GenreID { get; set; }
        public int MovieID { get; set; }

        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}
