namespace Entities.DTO;

public partial class LogPostDto
{
    public Guid LogTypeId { get; set; }

    public Guid UserId { get; set; }

    public string LogMessage { get; set; } = null!;
}
