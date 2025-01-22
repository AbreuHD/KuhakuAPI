using Auth.Infraestructure.Identity.Entities;
using Core.Domain.Common;
using Core.Domain.Entities.Relations;

namespace Core.Domain.Entities.UserThings
{
    public class ShareList : AuditableBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string UserID { get; set; }
        public bool IsPublic { get; set; }

        public ICollection<MovieList_Movie> MovieList_Movie { get; set; }
    }
}
