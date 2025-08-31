using Core.Domain.Common;

namespace Core.Domain.Entities.WebScraping
{
    public class ScrapPage : AuditableBase
    {
        public required string Name { get; set; }
        public required string Img { get; set; }
        public required string Info { get; set; }
        public required string Url { get; set; }
        public DateTime? LastScrapStart { get; set; }
        public DateTime? LastScrapEnd { get; set; }
        public bool IsOn { get; set; } = false;
        public bool Disabled { get; set; } = false;

        public ICollection<MovieWeb>? MovieWeb { get; set; }
    }
}
