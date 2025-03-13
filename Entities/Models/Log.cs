namespace Entities.Models;

public partial class Log
{
    public Guid LogId { get; set; }

    public Guid LogTypeId { get; set; }

    public Guid UserId { get; set; }

    public string LogMessage { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual LogType LogType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
