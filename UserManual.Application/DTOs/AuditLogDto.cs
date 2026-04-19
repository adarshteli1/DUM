//AudtiLogDto.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManual.Application.DTOs
{
    public class AuditLogDto
    {
        public string LogId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public DateTime ActionTimestamp { get; set; }
        public string Details { get; set; } = string.Empty;
    }
}
