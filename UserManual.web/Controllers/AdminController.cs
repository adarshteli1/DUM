using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace UserManual.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult CreateManual()
        {
            return View();
        }
    }
}
