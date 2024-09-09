using Core.Domain.Common;

namespace Core.Domain.Entities.WebScraping
{
    public class ScrapPage : AuditableBase
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string Info { get; set; }
        public string Url { get; set; }
        public DateTime LastScrap { get; set; }
        public bool IsOn { get; set; }

        public ICollection<MovieWeb> MovieWeb { get; set; }
    }
}
