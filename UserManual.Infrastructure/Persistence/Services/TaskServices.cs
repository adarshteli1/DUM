using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Persistence.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<List<TaskItemDto>> GetAllTasksAsync()
        {
            // Dapper returns List<TaskItemDto> directly now
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<TaskItemDto?> GetTaskByIdAsync(string taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null) return null;

            return new TaskItemDto
            {
                TaskItemId = task.TaskItemId,
                TaskItemName = task.TaskItemName,
                Status = task.Status,
                DueDate = task.DueDate,
                Description = task.Description,
                UserId = task.UserId,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task CreateTaskAsync(TaskItemDto dto)
        {
            var task = new TaskItem
            {
                TaskItemId = Guid.NewGuid().ToString(),
                TaskItemName = dto.TaskItemName,
                Status = "Pending",
                DueDate = dto.DueDate,
                Description = dto.Description,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.CreateTaskAsync(task);
        }

        public async Task UpdateTaskStatusAsync(string taskId, string newStatus)
        {
            await _taskRepository.UpdateTaskStatusAsync(taskId, newStatus);
        }

        public async Task DeleteTaskAsync(string taskId)
        {
            await _taskRepository.DeleteTaskAsync(taskId);
        }
    }
}
