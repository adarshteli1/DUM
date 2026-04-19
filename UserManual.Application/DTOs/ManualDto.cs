// ----------------------
// DTO: ManualDto.cs
// ----------------------
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UserManual.Application.DTOs
{
    public class ManualDto
    {
        public string ManualId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string UserId { get; set; }

        public int? TaskItemId { get; set; }
        public string NewTaskTitle { get; set; }

        public List<IFormFile> Images { get; set; }
        public List<string> Titles { get; set; }
        public List<string> Descriptions { get; set; }
    }
}