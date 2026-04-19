using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("Appuser_id");
            entity.Property(x => x.UserName).HasColumnName("username").HasMaxLength(100);
            entity.Property(x => x.PasswordHash).HasColumnName("password_hash");
            entity.Property(x => x.AccessRole).HasColumnName("access_role");
            entity.Property(x => x.DepartmentId).HasColumnName("department_id");

            entity.HasOne(x => x.Department)
                  .WithMany()
                  .HasForeignKey(x => x.DepartmentId);
        }
    }
}
