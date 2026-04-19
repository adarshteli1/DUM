using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;
using UserManual.Infrastructure.Persistence;

namespace UserManual.Infrastructure.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Department)
                .Select(u => new UserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName ?? "Unknown",
                    AccessRole = u.AccessRole ?? "None",
                    Email=u.Email,
                    DepartmentName = u.Department != null ? u.Department.Name ?? "No Department Name" : "No Department"
                }).ToListAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                AccessRole = user.AccessRole,
                DepartmentName = user.Department?.Name
            };
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.UserName + "@example.com",
                AccessRole = dto.AccessRole,
                DepartmentId = dto.DepartmentId
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, dto.AccessRole);
            }
            else
            {
                // 🚨 Throw error with full message details
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception("User creation failed: " + errorMessages);
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
