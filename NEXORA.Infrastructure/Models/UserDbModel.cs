namespace NEXORA.Infrastructure.Models;

using Microsoft.AspNetCore.Identity;

public class UserDbModel : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool MustChangePassword { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; set; }
}
