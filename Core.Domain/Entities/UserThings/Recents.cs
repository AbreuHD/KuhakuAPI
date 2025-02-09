using Core.Domain.Common;

namespace Core.Domain.Entities.UserThings
{
    public class Recents : AuditableBase
    {
        public required DateTime Date { get; set; }
        public required string UserID { get; set; }
        public required int MovieID { get; set; }

        public required Movie.Movie Movie { get; set; }
    }
}
