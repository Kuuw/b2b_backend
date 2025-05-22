namespace Entities.DTO;

public class MonthlyStatsDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public double Average { get; set; }
    public int OrderCount { get; set; }
    public double TotalSpent { get; set; }
}
