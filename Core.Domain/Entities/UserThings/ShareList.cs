using Core.Domain.Common;
using Core.Domain.Entities.Relations;

namespace Core.Domain.Entities.UserThings
{
    public class ShareList : AuditableBase
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Img { get; set; }
        public string? UserID { get; set; }
        public required bool IsPublic { get; set; }

        public ICollection<MovieListMovie>? MovieListMovie { get; set; }
    }
}
