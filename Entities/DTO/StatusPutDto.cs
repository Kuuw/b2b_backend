using Entities.Models;

namespace Entities.DTO;

public partial class StatusPutDto
{
    public Guid StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }
}
