using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        public Company? GetByEmail(string email);
        public int getUserCount(Guid CompanyId, ReportFilter Filter);
        public float getTotalSales(Guid CompanyId, ReportFilter Filter);
        public int getTotalOrders(Guid CompanyId, ReportFilter Filter);
        public Double getAverageSpent(Guid CompanyId, ReportFilter Filter);
        public DateTime? lastOrderDate(Guid CompanyId, ReportFilter Filter);
    }
}
