using System.Threading.Tasks;
using System.Collections.Generic;
using UserManual.Application.DTOs;
using UserManual.Domain.Entities;

namespace UserManual.Application.Interfaces
{
    public interface ITaskRepository
    {

        Task<List<TaskItemDto>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(string taskId);
        Task CreateTaskAsync(TaskItem task);
        Task UpdateTaskStatusAsync(string taskId, string newStatus);
        Task DeleteTaskAsync(string taskId);
    }
}
