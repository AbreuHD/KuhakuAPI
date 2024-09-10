using Core.Domain.Common;
using Core.Domain.Entities.UserThings;

namespace Core.Domain.Entities.User
{
    public class UserEntity : AuditableBase
    {
        public string UserID { get; set; }
        public string Name { get; set; }

        public ICollection<Recents> Recents { get; set; }
        public ICollection<MovieList> MovieLists { get; set; }

    }
}
