namespace Entities.DTO;

public partial class PermissionPostDto
{
    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string? Description { get; set; }
}
