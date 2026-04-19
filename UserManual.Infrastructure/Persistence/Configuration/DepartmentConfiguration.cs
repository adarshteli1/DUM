using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.ToTable("departments");
            entity.HasKey(x => x.DepartmentId);
            entity.Property(x => x.DepartmentId).HasColumnName("Department_id");
            entity.Property(x => x.Name).HasColumnName("name").HasMaxLength(100);
        }
    }
}
