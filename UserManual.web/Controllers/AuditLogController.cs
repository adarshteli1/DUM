using Microsoft.AspNetCore.Mvc;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;

namespace UserManual.Web.Controllers
{
    public class AuditLogsController : Controller
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogsController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        // GET: /AuditLogs
        public async Task<IActionResult> Index()
        {
            var logs = await _auditLogService.GetAllLogsAsync();
            return View(logs);
        }
    }
}
