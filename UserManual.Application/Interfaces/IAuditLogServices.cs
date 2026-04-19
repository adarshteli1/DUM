using UserManual.Application.DTOs;

namespace UserManual.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task<List<AuditLogDto>> GetAllLogsAsync();
        Task<List<AuditLogDto>> GetLogsByUserAsync(string userId);
        Task AddLogAsync(AuditLogDto dto);
       

    }
}
