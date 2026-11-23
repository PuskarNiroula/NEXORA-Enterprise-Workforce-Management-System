
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NEXORA.Infrastructure.Models;

namespace NEXORA.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<UserDbModel, IdentityRole<Guid>, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserDbModel>(entity =>
        {
            entity.ToTable("Users");

            entity.Property(x => x.Id).HasColumnName("user_id");
            entity.Property(x => x.UserName).HasColumnName("username");
            entity.Property(x => x.NormalizedUserName).HasColumnName("normalized_username");
            entity.Property(x => x.Email).HasColumnName("email");
            entity.Property(x => x.NormalizedEmail).HasColumnName("normalized_email");
            entity.Property(x => x.EmailConfirmed).HasColumnName("email_confirmed");
            entity.Property(x => x.PasswordHash).HasColumnName("password_hash");
            entity.Property(x => x.SecurityStamp).HasColumnName("security_stamp");
            entity.Property(x => x.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            entity.Property(x => x.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            entity.Property(x => x.LockoutEnd).HasColumnName("lockout_end");
            entity.Property(x => x.LockoutEnabled).HasColumnName("lockout_enabled");
            entity.Property(x => x.AccessFailedCount).HasColumnName("access_failed_count");

            entity.Property(x => x.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            entity.Property(x => x.MustChangePassword)
                .HasColumnName("must_change_password")
                .HasDefaultValue(true);

            entity.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            entity.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");
        });
    }
}
