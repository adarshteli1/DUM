using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManual.Domain.Entities;
using UserManual.Infrastructure.Persistence.Configurations;

namespace UserManual.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // ✅ Declare DbSets for your entities
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Manual> Manuals { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🔧 Fix User table
            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("AspNetUsers");
                b.Property(u => u.Id).HasColumnName("UserId");
            });

            // ✅ Fix Role table
            builder.Entity<IdentityRole>(b =>
            {
                b.ToTable("AspNetRoles");
                b.Property(r => r.Id).HasColumnName("RoleId"); // 👈 this line is crucial!
            });

            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("AspNetUserClaims");
                b.Property(c => c.Id).HasColumnName("UserClaimId");
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("AspNetRoleClaims");
                b.Property(c => c.Id).HasColumnName("RoleClaimId");
            });
            builder.Entity<Manual>()
           .HasOne(m => m.User)
           .WithMany()
           .HasForeignKey(m => m.UserId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<IdentityUserLogin<string>>(b => b.ToTable("AspNetUserLogins"));
            builder.Entity<IdentityUserRole<string>>(b => b.ToTable("AspNetUserRoles"));
            builder.Entity<IdentityUserToken<string>>(b => b.ToTable("AspNetUserTokens"));
            builder.ApplyConfiguration(new ManualConfiguration());
            builder.ApplyConfiguration(new TaskItemConfiguration());

        }

    }
}
