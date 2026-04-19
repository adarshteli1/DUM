//TaskItemDto.cs

namespace UserManual.Application.DTOs
{
    public class TaskItemDto
    {
        public string TaskItemId { get; set; } = Guid.NewGuid().ToString();

        public string TaskItemName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; } 


        public string? Description { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
    }
}
