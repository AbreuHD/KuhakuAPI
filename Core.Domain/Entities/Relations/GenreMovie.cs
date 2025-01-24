using Core.Domain.Common;
using Core.Domain.Entities.Movie;

namespace Core.Domain.Entities.Relations
{
    public class GenreMovie : AuditableBase
    {
        public required int GenreID { get; set; }
        public required int MovieID { get; set; }

        public Genre? Genre { get; set; }
        public Movie.Movie? Movie { get; set; }
    }
}
