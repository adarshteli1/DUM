using System.ComponentModel.DataAnnotations.Schema;

namespace UserManual.Domain.Entities
{
    [Table("departments")] // Map table
    public class Department
    {
        [Column("department_id")]
        public string DepartmentId { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }

}

