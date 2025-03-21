namespace Entities.DTO;

public partial class LogTypePutDto
{
    public Guid LogTypeId { get; set; }

    public string LogTypeName { get; set; } = null!;
}
