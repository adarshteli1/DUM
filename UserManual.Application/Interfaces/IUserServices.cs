using UserManual.Application.DTOs;

namespace UserManual.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(string userId);
    Task CreateUserAsync(CreateUserDto dto);
    Task DeleteUserAsync(string userId);

}
