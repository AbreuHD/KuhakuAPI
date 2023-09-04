using Core.Domain.Common;
using Core.Domain.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.GeneralMovie
{
    public class Genre : AuditableBase
    {
        public string Name { get;set; }

        public ICollection<Genre_Movie> Genre_Movie { get; set; }
    }
}
