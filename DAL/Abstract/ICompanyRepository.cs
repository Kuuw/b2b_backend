using Entities.Models;

namespace DAL.Abstract
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        public Company? GetByEmail(string email);
        public int getUserCount(Guid companyId);
        public float getTotalSales(Guid companyId);
        public int getTotalOrders(Guid companyId);
        public Double getAverageSpent(Guid companyId);
    }
}
