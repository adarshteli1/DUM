// Configuration: ManualConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Persistence.Configurations
{
    public class ManualConfiguration : IEntityTypeConfiguration<Manual>
    {
        public void Configure(EntityTypeBuilder<Manual> entity)
        {
            entity.ToTable("manuals");

            entity.HasKey(x => x.ManualId);
            entity.Property(x => x.ManualId).HasColumnName("manual_id");
            entity.Property(x => x.FileName).HasColumnName("file_name");
            entity.Property(x => x.FilePath).HasColumnName("file_path");
            entity.Property(x => x.TaskItemId).HasColumnName("task_item_id");
            entity.Property(x => x.UploadDate).HasColumnName("upload_date");
            entity.Property(x => x.UserId).HasColumnName("user_id");

            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_manuals_users");

            entity.HasOne(x => x.Task)
                .WithMany() .HasForeignKey(x => x.TaskItemId)
                .HasForeignKey(x => x.TaskItemId)
                .HasConstraintName("FK_manuals_tasks");
        }
    }
}