using Dapper;
using System.Data;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Repositories
{
    public class ManualRepository : IManualRepository
    {
        private readonly IDbConnection _dbConnection;

        public ManualRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task UploadManualAsync(Manual manual)
        {
            var sql = @"
        INSERT INTO manuals 
        (manual_id, file_name, file_path, upload_date, task_item_id, user_id, Description)
        VALUES 
        (@ManualId, @FileName, @FilePath, @UploadDate, @TaskItemId, @UserId, @Description);
    ";

            await _dbConnection.ExecuteAsync(sql, new
            {
                ManualId = manual.ManualId,
                FileName = manual.FileName,
                FilePath = manual.FilePath,
                UploadDate = manual.UploadDate,
                TaskItemId = manual.TaskItemId,
                UserId = manual.UserId,
                Description = manual.Description
            });
        }


        public async Task<IEnumerable<Manual>> GetManualsByUserAsync(string userId)
        {
            var sql = @"
            SELECT 
            manual_id AS ManualId,
            file_name AS FileName,
            file_path AS FilePath,
            upload_date AS UploadDate,
            task_item_id AS TaskItemId,
            user_id AS UserId,
            Description
            FROM manuals
            WHERE user_id = @UserId
            ORDER BY upload_date DESC";
            return await _dbConnection.QueryAsync<Manual>(sql, new { UserId = userId });
        }

        public async Task DeleteManualAsync(string manualId)
        {
            var sql = "DELETE FROM Manuals WHERE ManualId = @ManualId";
            await _dbConnection.ExecuteAsync(sql, new { ManualId = manualId });
        }
    }
}
