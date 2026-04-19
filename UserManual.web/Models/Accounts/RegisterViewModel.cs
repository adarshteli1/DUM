using System.ComponentModel.DataAnnotations;

namespace UserManual.Web.Models.Accounts
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = "";

        [Required]
        [Display(Name = "Access Role")]
        public string AccessRole { get; set; } = "Employee"; // e.g., Admin, Manager, etc.

        [Display(Name = "Department")]
        public string? DepartmentId { get; set; }
    }
}
