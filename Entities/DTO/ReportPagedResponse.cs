namespace Entities.DTO;

public class ReportPagedResponse: GenericPagedResponse<CompanyReportDto>
{
    public Double MaxSpent { get; set; }
    public int MaxOrderCount { get; set; }
}
