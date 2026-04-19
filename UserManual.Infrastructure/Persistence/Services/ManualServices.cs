// Service: ManualService.cs
// ----------------------
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;

namespace UserManual.Application.Services
{
    public class ManualService : IManualService
    {
        private readonly IManualRepository _manualRepository;

        public ManualService(IManualRepository manualRepository)
        {
            _manualRepository = manualRepository;
        }

        public async Task UploadManualAsync(ManualDto dto)
        {
            var manual = new Manual
            {
                ManualId = dto.ManualId,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                UploadDate = dto.UploadDate,
                TaskItemId = dto.TaskItemId?.ToString(),

                UserId = dto.UserId
            };

            await _manualRepository.UploadManualAsync(manual);
        }

        public async Task<List<ManualDto>> GetManualsByUserAsync(string userId)
        {
            var manuals = await _manualRepository.GetManualsByUserAsync(userId);

            return manuals.Select(m => new ManualDto
            {
                ManualId = m.ManualId,
                FileName = m.FileName,
                FilePath = m.FilePath,
                UploadDate = m.UploadDate,
                TaskItemId = int.TryParse(m.TaskItemId, out var id) ? id : (int?)null,

                UserId = m.UserId
            }).ToList();
        }

        public async Task DeleteManualAsync(string manualId)
        {
            await _manualRepository.DeleteManualAsync(manualId);
        }
    }
}