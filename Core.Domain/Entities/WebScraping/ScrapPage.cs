using Core.Domain.Common;
using Core.Domain.Entities.UserThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
