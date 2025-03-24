namespace Entities.DTO;

public partial class StatusGetDto
{
    public Guid StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }
}
