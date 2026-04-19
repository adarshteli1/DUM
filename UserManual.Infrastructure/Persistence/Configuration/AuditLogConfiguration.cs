using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManual.Domain.Entities;

namespace UserManual.Infrastructure.Persistence.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> entity)
        {
            entity.ToTable("audit_logs");
            entity.HasKey(x => x.AuditLogId);
            entity.Property(x => x.AuditLogId).HasColumnName("log_id");
            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.EntityId).HasColumnName("entity_id");
            entity.Property(x => x.ActionType).HasColumnName("action_type");
            entity.Property(x => x.ActionTimestamp).HasColumnName("action_timestamp");
            entity.Property(x => x.Details).HasColumnName("details");

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);
        }
    }
}
