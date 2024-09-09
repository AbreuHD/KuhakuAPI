using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.User;

namespace Core.Domain.Entities.UserThings
{
    public class Recents : AuditableBase
    {
        public DateTime Date { get; set; }
        public int UserEntityID { get; set; }
        public int MovieID { get; set; }

        public Movie Movie { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}
