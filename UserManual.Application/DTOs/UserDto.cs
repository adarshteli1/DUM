namespace UserManual.Application.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? AccessRole { get; set; } = string.Empty;
        public string? DepartmentName { get; set; }
    }
}

