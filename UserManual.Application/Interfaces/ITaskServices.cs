using Microsoft.EntityFrameworkCore;
using UserManual.Application.DTOs;

namespace UserManual.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItemDto>> GetAllTasksAsync();
        Task<TaskItemDto?> GetTaskByIdAsync(string taskId);
        Task CreateTaskAsync(TaskItemDto dto);
        Task UpdateTaskStatusAsync(string taskId, string newStatus);
        Task DeleteTaskAsync(string taskId);
    }
   
}
