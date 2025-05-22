namespace Entities.DTO;

public class ReportSelfResponse
{
    public CompanyReportDto Report { get; set; } = default!;
    public List<MonthlyStatsDto> MonthlyStats { get; set; } = new List<MonthlyStatsDto>();
    public int UsersPageNumber { get; set; }
    public int UsersPageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalUsers { get; set; }
}
