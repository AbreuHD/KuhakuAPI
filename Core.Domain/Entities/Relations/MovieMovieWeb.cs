using Core.Domain.Common;
using Core.Domain.Entities.WebScraping;

namespace Core.Domain.Entities.Relations
{
    public class MovieMovieWeb : AuditableBase
    {
        public required int MovieID { get; set; }
        public required int MovieWebID { get; set; }
        public required bool Verified { get; set; }

        public Movie.Movie? Movie { get; set; }
        public MovieWeb? MovieWeb { get; set; }
    }
}
