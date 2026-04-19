using Microsoft.AspNetCore.Mvc;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;

namespace UserManual.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TasksController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAllUsersAsync(); // Or whatever service you're using
            ViewBag.Users = users;

            // ✅ Set the user role so the view can decide where "Cancel" goes
            ViewBag.UserRole = User.IsInRole("Admin") ? "Admin" : "User";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItemDto dto)
        {
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users;

            if (!ModelState.IsValid)
                return View(dto);

            // ✅ Check if UserId exists in the DB
            if (!users.Any(u => u.UserId == dto.UserId))
            {
                ModelState.AddModelError("UserId", "Selected user does not exist.");
                return View(dto);
            }

            await _taskService.CreateTaskAsync(dto);
            TempData["SuccessMessage"] = "Task created successfully!";
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(string id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}