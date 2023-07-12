using Core.Domain.Common;
using Core.Domain.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.UserThings
{
    public class MovieList : AuditableBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string UserID { get; set; }

        public ICollection<MovieList_Movie> MovieList_Movie { get; set; }
    }
}
