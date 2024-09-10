using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.WebScraping;

namespace Core.Domain.Entities.Relations
{
    public class Movie_MovieWeb : AuditableBase
    {
        public int MovieID { get; set; }
        public int MovieWebID { get; set; }
        public bool Verified { get; set; }

        public Movie Movie { get; set; }
        public MovieWeb MovieWeb { get; set; }
    }
}
