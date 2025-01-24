using Core.Domain.Common;
using Core.Domain.Entities.Relations;

namespace Core.Domain.Entities.WebScraping
{
    public class MovieWeb : AuditableBase
    {
        public required string Name { get; set; }
        public string? Overview { get; set; }
        public required string Url { get; set; }
        public string? Img { get; set; }
        public required int ScrapPageID { get; set; }

        public ScrapPage? ScrapPage { get; set; }
        public ICollection<MovieMovieWeb>? Movie_MovieWeb { get; set; }
    }
}
