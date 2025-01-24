using Core.Domain.Common;

namespace Core.Domain.Entities.WebScraping
{
    public class ScrapPage : AuditableBase
    {
        public required string Name { get; set; }
        public required string Img { get; set; }
        public required string Info { get; set; }
        public required string Url { get; set; }
        public required DateTime LastScrap { get; set; }
        public required bool IsOn { get; set; }

        public ICollection<MovieWeb>? MovieWeb { get; set; }
    }
}
