//CreateUerDto.cs

namespace UserManual.Application.DTOs
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccessRole { get; set; } = "Employee";
        public string? DepartmentId { get; set; }
    }
}