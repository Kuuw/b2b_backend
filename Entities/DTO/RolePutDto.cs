using Entities.Models;

namespace Entities.DTO;

public partial class RolePutDto
{
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<PermissionGetDto> Permissions { get; set; } = new List<PermissionGetDto>();
}
