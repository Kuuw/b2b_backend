using Entities.Models;

namespace Entities.DTO;

public partial class RolePostDto
{
    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<PermissionPostDto> Permissions { get; set; } = new List<PermissionPostDto>();
}
