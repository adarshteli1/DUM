using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> entity)
        {
            entity.ToTable("tasks");

            entity.HasKey(e => e.TaskItemId);

            entity.Property(e => e.TaskItemId).HasColumnName("task_item_id");
            entity.Property(e => e.TaskItemName).HasColumnName("task_item_name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Description).HasColumnName("description");

            // Optional: if there's a relationship with ApplicationUser
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        }
    }
}
