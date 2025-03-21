namespace Entities.DTO;

public partial class LogTypeGetDto
{
    public Guid LogTypeId { get; set; }

    public string LogTypeName { get; set; } = null!;
}
