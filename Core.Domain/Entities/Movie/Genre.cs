using Core.Domain.Common;
using Core.Domain.Entities.Relations;

namespace Core.Domain.Entities.GeneralMovie
{
    public class Genre : AuditableBase
    {
        public required string Name { get; set; }
        public required int GenreID { get; set; }

        public ICollection<Genre_Movie> Genre_Movie { get; set; }
    }
}
