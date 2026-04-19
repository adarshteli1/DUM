using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;
using UserManual.Infrastructure.Persistence;

namespace UserManual.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IManualService _manualService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(
            IUserService userService,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IManualService manualService)
        {
            _userService = userService;
            _context = context;
            _userManager = userManager;
            _manualService = manualService;
        }

        // ✅ GET: /Users/CreateUser
        public async Task<IActionResult> CreateUser()
        {
            await LoadDropdownsAsync();
            return View();
        }

        // ✅ POST: /Users/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            await _userService.CreateUserAsync(dto);
            TempData["SuccessMessage"] = "User created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ✅ GET: /Users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // ✅ GET: /Users/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);
            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ✅ USER DASHBOARD
        [Authorize]
        public async Task<IActionResult> UserLoggedIn()
        {
            var userId = _userManager.GetUserId(User);
            var manuals = await _manualService.GetManualsByUserAsync(userId);

            ViewBag.UserName = User.Identity?.Name ?? "User";
            ViewBag.ManualCount = manuals.Count();
            ViewBag.LastUpload = manuals
                .OrderByDescending(m => m.UploadDate)
                .FirstOrDefault()?.UploadDate.ToString("dd MMM yyyy HH:mm") ?? "N/A";

            return View(manuals); // View: Views/Users/UserLoggedIn.cshtml
        }

        // 🔧 Load dropdowns for roles/departments
        private async Task LoadDropdownsAsync()
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Employee", Text = "Employee" },
                new SelectListItem { Value = "Intern", Text = "Intern" },
                new SelectListItem { Value = "Manager", Text = "Manager" }
            };

            ViewBag.Departments = await _context.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId,
                    Text = d.Name
                }).ToListAsync();
        }
    }
}
