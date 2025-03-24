using Entities.Models;

namespace Entities.DTO;

public partial class LogGetDto
{
    public Guid LogId { get; set; }

    public Guid UserId { get; set; }

    public string LogMessage { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual LogTypeGetDto LogType { get; set; } = null!;
}
