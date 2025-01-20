using Auth.Infraestructure.Identity.Entities;
using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;

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
