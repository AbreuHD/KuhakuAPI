using Auth.Infraestructure.Identity.Entities;
using Core.Domain.Common;

namespace Core.Domain.Entities.UserThings
{
    public class Recents : AuditableBase
    {
        public DateTime Date { get; set; }
        public string UserID { get; set; }
        public int MovieID { get; set; }

        public Movie Movie { get; set; }
    }
}
