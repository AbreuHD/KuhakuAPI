namespace Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public int ID { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime Created { get; set; }
        public required string LastModifiedby { get; set; }
        public required DateTime? LastModified { get; set; }
    }
}
