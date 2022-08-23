using System;

namespace K_haku.Domain.Common
{
    public class AuditableBaseMovies
    {
        public virtual int ID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedby { get; set;}
        public DateTime? LastModified { get; set; }
    }
}
