// ✅ IManualService.cs (Updated)
using UserManual.Application.DTOs;

namespace UserManual.Application.Interfaces
{
    public interface IManualService
    {
        Task<List<ManualDto>> GetManualsByUserAsync(string userId);
        Task UploadManualAsync(ManualDto dto);
        Task DeleteManualAsync(string manualId);
    }
}
