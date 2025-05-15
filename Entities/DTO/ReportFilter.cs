namespace Entities.DTO;

public class ReportFilter
{
    public string? Search { get; set; }
    public List<Guid> Users { get; set; } = [];
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Double? MinSpent { get; set; }
    public Double? MaxSpent { get; set; }
    public Double? MinOrder { get; set; }
    public Double? MaxOrder { get; set; }
}
