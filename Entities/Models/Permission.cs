namespace Entities.Models;

public partial class Permission
{
    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
