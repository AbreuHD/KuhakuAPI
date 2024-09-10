namespace Core.Domain.Common
{
    public class AuditableBase
    {
        public int ID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedby { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
