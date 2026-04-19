using UserManual.Domain.Entities;

public class TaskItem
{
    public string TaskItemId { get; set; }
    public string TaskItemName { get; set; } // <- this replaces "Title"
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    // Optional navigation
    public ApplicationUser? User { get; set; }
}
