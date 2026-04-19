using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Drawing;
using System.Drawing.Imaging;
using UserManual.Application.DTOs;
using UserManual.Application.Interfaces;
using UserManual.Domain.Entities;

namespace UserManual.Web.Controllers
{
    [Authorize]
    public class ManualsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITaskService _taskService;
        private readonly IManualService _manualService;
        private readonly ILogger<ManualsController> _logger;

        public ManualsController(
            UserManager<ApplicationUser> userManager,
            ITaskService taskService,
            IManualService manualService,
            ILogger<ManualsController> logger)
        {
            _userManager = userManager;
            _taskService = taskService;
            _manualService = manualService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> CreateManuals()
        {
            var tasks = await _taskService.GetAllTasksAsync();

            ViewBag.Tasks = tasks.Select(t => new SelectListItem
            {
                Value = t.TaskItemId.ToString(),
                Text = t.Title
            }).ToList();

            ViewBag.UserId = _userManager.GetUserId(User);

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.UserRole = roles.FirstOrDefault();

            return View("CreateManuals");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [FromForm] ManualDto dto,
            [FromForm] List<IFormFile> Images,
            [FromForm] List<string> Titles,
            [FromForm] List<string> Descriptions)
        {
            dto.Images = Images;
            dto.Titles = Titles;
            dto.Descriptions = Descriptions;

            if (Images == null || Images.Count < 1 || Images.Count > 3)
                return BadRequest("Please upload between 1 to 3 images.");

            dto.ManualId = Guid.NewGuid().ToString();
            dto.UploadDate = DateTime.UtcNow;
            dto.UserId = _userManager.GetUserId(User);
            dto.FilePath = $"manuals/{dto.ManualId}.pdf";

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            bool isAdmin = roles.Contains("Admin");

            var tasks = await _taskService.GetAllTasksAsync();
            var task = tasks.FirstOrDefault(t => t.TaskItemId.ToString() == dto.TaskItemId?.ToString());

            bool isAssigned = task != null && task.UserId == dto.UserId;

            if (!isAdmin && !isAssigned)
                return Forbid("");

            try
            {
                using var pdf = new PdfDocument();

                for (int i = 0; i < Images.Count; i++)
                {
                    var image = Images[i];
                    var title = Titles.ElementAtOrDefault(i) ?? $"Image {i + 1}";
                    var desc = Descriptions.ElementAtOrDefault(i) ?? "No description.";

                    var page = pdf.AddPage();
                    var gfx = XGraphics.FromPdfPage(page);

                    var headingFont = new XFont("Arial", 16, XFontStyle.Bold);
                    gfx.DrawString("SP Solutions", headingFont, XBrushes.Black,
                        new XRect(0, 20, page.Width, 30), XStringFormats.TopCenter);

                    using var stream = new MemoryStream();
                    using (var imgStream = image.OpenReadStream())
                    {
                        using var sysImg = System.Drawing.Image.FromStream(imgStream);
                        sysImg.Save(stream, ImageFormat.Png);
                    }

                    stream.Position = 0;
                    var xImg = XImage.FromStream(() => stream);

                    double maxWidth = page.Width - 80;
                    double maxHeight = page.Height - 250;
                    double scale = Math.Min(maxWidth / xImg.PixelWidth, maxHeight / xImg.PixelHeight);
                    double imgW = xImg.PixelWidth * scale;
                    double imgH = xImg.PixelHeight * scale;

                    gfx.DrawImage(xImg, (page.Width - imgW) / 2, 60, imgW, imgH);

                    gfx.DrawString($"📌 {title}", new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black,
                        new XRect(40, 70 + imgH, page.Width - 80, 20), XStringFormats.TopLeft);

                    gfx.DrawString(desc, new XFont("Arial", 10), XBrushes.Black,
                        new XRect(40, 95 + imgH, page.Width - 80, 50), XStringFormats.TopLeft);
                }

                using var pdfStream = new MemoryStream();
                pdf.Save(pdfStream, false);
                pdfStream.Position = 0;

                await _manualService.UploadManualAsync(dto);

                string downloadName = string.IsNullOrWhiteSpace(dto.FileName) ? "Manual" : dto.FileName.Trim();
                return File(pdfStream.ToArray(), "application/pdf", $"{downloadName}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ PDF generation failed.");
                return BadRequest("Error generating PDF.");
            }
        }
    }
}
