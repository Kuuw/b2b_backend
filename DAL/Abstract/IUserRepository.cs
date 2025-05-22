using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetByEmail(string email);
        float GetTotalSales(Guid userId, ReportFilter filter);
        int GetTotalOrders(Guid userId, ReportFilter filter);
        double GetAverageSpent(Guid userId, ReportFilter filter);
        DateTime? LastOrderDate(Guid userId, ReportFilter filter);
    }
}
