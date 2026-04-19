using Microsoft.EntityFrameworkCore;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;
using UserManual.Infrastructure.Persistence;

namespace UserManual.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context;

        public AuditLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuditLogDto>> GetAllLogsAsync()
        {
            return await _context.AuditLogs
                .Include(l => l.User)
                .Select(l => new AuditLogDto
                {
                    LogId = l.AuditLogId,
                    UserId = l.UserId,
                    EntityId = l.EntityId,
                    ActionType = l.ActionType,
                    ActionTimestamp = l.ActionTimestamp,
                    Details = l.Details
                }).ToListAsync();
        }

        public async Task<List<AuditLogDto>> GetLogsByUserAsync(string userId)
        {
            return await _context.AuditLogs
                .Where(l => l.UserId == userId)
                .Select(l => new AuditLogDto
                {
                    LogId = l.AuditLogId,
                    UserId = l.UserId,
                    EntityId = l.EntityId,
                    ActionType = l.ActionType,
                    ActionTimestamp = l.ActionTimestamp,
                    Details = l.Details
                }).ToListAsync();
        }

        public async Task AddLogAsync(AuditLogDto dto)
        {
            var log = new AuditLog
            {
                AuditLogId = Guid.NewGuid().ToString(),
                UserId = dto.UserId,
                EntityId = dto.EntityId,
                ActionType = dto.ActionType,
                ActionTimestamp = dto.ActionTimestamp,
                Details = dto.Details
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
