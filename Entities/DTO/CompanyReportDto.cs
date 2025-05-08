namespace Entities.DTO;

public class CompanyReportDto
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public int UserCount { get; set; }
    public double AverageSpent { get; set; }
    public double TotalSpent { get; set; }
    public int TotalOrders { get; set; }
}