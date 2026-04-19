
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManual.Domain.Entities;

namespace UserManual.Application.Interfaces
{
    public interface IManualRepository
    {
        Task<IEnumerable<Manual>> GetManualsByUserAsync(string userId);
        Task UploadManualAsync(Manual manual);
        Task DeleteManualAsync(string manualId);

    }
}