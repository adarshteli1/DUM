using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManual.Domain.Entities
{
    [Table("audit_logs")]
    public class AuditLog
    {
        [Key] 
        public string AuditLogId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; } = string.Empty;

        public string EntityId { get; set; } = string.Empty;

        [Required]
        public string ActionType { get; set; } = string.Empty;

        public DateTime ActionTimestamp { get; set; } = DateTime.UtcNow;

        public string Details { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
