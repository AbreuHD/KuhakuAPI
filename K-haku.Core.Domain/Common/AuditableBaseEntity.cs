using System;

namespace SocialNetwork.Domain.Common
{
    public class AuditableBaseEntity
    {
        public virtual int ID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedby { get; set;}
        public DateTime? LastModified { get; set; }
    }
}
