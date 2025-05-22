using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        public Company? GetByEmail(string email);
        public int GetUserCount(Guid CompanyId, ReportFilter Filter);
        public float GetTotalSales(Guid CompanyId, ReportFilter Filter);
        public int GetTotalOrders(Guid CompanyId, ReportFilter Filter);
        public Double GetAverageSpent(Guid CompanyId, ReportFilter Filter);
        public DateTime? LastOrderDate(Guid CompanyId, ReportFilter Filter);
        public Double GetMaxTotalSpentAcrossAllCompanies(ReportFilter filter);
        public int GetMaxOrderCountAcrossAllCompanies(ReportFilter filter);
        public List<MonthlyStatsDto> GetMonthlyStats(Guid companyId, ReportFilter filter);
    }
}
