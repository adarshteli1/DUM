using UserManual.Domain.Entities;

public class Manual
{
    public string ManualId { get; set; } = string.Empty;
    public string TaskItemId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }
    public string? Description { get; set; }
    public TaskItem? Task { get; set; }
    public ApplicationUser User { get; set; }
}
