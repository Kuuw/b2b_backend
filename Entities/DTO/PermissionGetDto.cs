namespace Entities.DTO;

public partial class PermissionGetDto
{
    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string? Description { get; set; }
}
