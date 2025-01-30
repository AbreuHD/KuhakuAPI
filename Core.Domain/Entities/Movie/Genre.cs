using Core.Domain.Common;
using Core.Domain.Entities.Relations;

namespace Core.Domain.Entities.Movie
{
    public class Genre : AuditableBase
    {
        public required string Name { get; set; }
        public required int GenreID { get; set; }
        public required bool IsMovie { get; set; } = false;

        public ICollection<GenreMovie>? GenreMovie { get; set; }
    }
}
