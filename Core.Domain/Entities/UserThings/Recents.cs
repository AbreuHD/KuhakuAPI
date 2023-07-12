using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.UserThings
{
    public class Recents : AuditableBase
    {
        public DateTime Date { get; set; }
        public int GenreID { get; set; }
        public int MovieID { get; set; }

        public Movie Movie { get; set; }
    }
}
