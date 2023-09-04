using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Relations
{
    public class Genre_Movie : AuditableBase
    {
        public int GenreID { get;set; }
        public int MovieID { get; set; }

        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}
