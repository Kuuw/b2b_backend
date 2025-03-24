namespace Entities.DTO;

public partial class RolePostDto
{
    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<PermissionGetDto> Permissions { get; set; } = new List<PermissionGetDto>();
}
