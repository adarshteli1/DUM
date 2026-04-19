using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManual.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {

        [Column("access_role")]
        public string? AccessRole { get; set; }

        [Column("department_id")]
        public string? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        [Table("users")]
        public class User
        {
            [Key]
            [Column("user_id")]
            public string UserId { get; set; } = string.Empty;

            [Column("username")]
            public string UserName { get; set; } = string.Empty;

            [Column("password_hash")]
            public string PasswordHash { get; set; } = string.Empty;

            [Column("department_id")]
            public string DepartmentId { get; set; } = string.Empty;

            [Column("acess_role")]
            public string AccessRole { get; set; } = string.Empty;
        }
    }

}
