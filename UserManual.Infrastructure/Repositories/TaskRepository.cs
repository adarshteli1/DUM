using Dapper;
using System.Data;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection _dbConnection;

        public TaskRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<TaskItemDto>> GetAllTasksAsync()
        {
            var sql = @"
                SELECT 
                    task_item_id AS TaskItemId,
                    task_item_name AS TaskItemName,
                    status,
                    due_date AS DueDate,
                    description,
                    user_id AS UserId,
                    CreatedAt
                FROM tasks";

            var tasks = await _dbConnection.QueryAsync<TaskItemDto>(sql);
            return tasks.ToList();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(string taskId)
        {
            var sql = "SELECT * FROM tasks WHERE task_item_id = @TaskId";
            return await _dbConnection.QueryFirstOrDefaultAsync<TaskItem>(sql, new { TaskId = taskId });
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            var sql = @"INSERT INTO tasks 
                        (task_item_id, task_item_name, status, due_date, description, user_id, CreatedAt)
                        VALUES (@TaskItemId, @TaskItemName, @Status, @DueDate, @Description, @UserId, @CreatedAt)";
            await _dbConnection.ExecuteAsync(sql, task);
        }

        public async Task UpdateTaskStatusAsync(string taskId, string newStatus)
        {
            var sql = "UPDATE tasks SET status = @Status WHERE task_item_id = @TaskId";
            await _dbConnection.ExecuteAsync(sql, new { Status = newStatus, TaskId = taskId });
        }

        public async Task DeleteTaskAsync(string taskId)
        {
            var sql = "DELETE FROM tasks WHERE task_item_id = @TaskId";
            await _dbConnection.ExecuteAsync(sql, new { TaskId = taskId });
        }
    }
}
