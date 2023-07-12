using Core.Domain.Common;
using Core.Domain.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.WebScraping
{
    public class MovieWeb : AuditableBase
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public int ScrapPageID { get; set; }

        public ScrapPage ScrapPage { get; set; }
        public ICollection<Movie_MovieWeb> Movie_MovieWeb { get; set; }
    }
}
