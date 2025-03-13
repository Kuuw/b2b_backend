namespace Entities.Models;

public partial class LogType
{
    public Guid LogTypeId { get; set; }

    public string LogTypeName { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
