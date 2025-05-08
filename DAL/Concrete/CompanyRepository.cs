using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly B2bContext _context = new B2bContext();
        private readonly DbSet<Company> _company;

        public CompanyRepository(B2bContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _company = context.Set<Company>();
        }

        public Company? GetByEmail(string email)
        {
            return _company.FirstOrDefault(c => c.Email == email);
        }

        public int getUserCount(Guid companyId)
        {
            return _context.Users.Count(u => u.CompanyId == companyId);
        }

        public float getTotalSales(Guid companyId)
        {
            var orders = _context.Orders.Where(i => i.User.CompanyId == companyId);
            var totalSales = orders
                .SelectMany(o => o.OrderItems)
                .Sum(oi => (float)(oi.Quantity * oi.Product.Price));
            return totalSales;
        }

        public int getTotalOrders(Guid companyId)
        {
            return _context.Orders.Count(o => o.User.CompanyId == companyId);
        }

        public Double getAverageSpent(Guid companyId)
        {
            var orders = _context.Orders.Where(i => i.User.CompanyId == companyId);
            var totalSpent = orders
                .SelectMany(o => o.OrderItems)
                .Sum(oi => (float)(oi.Quantity * oi.Product.Price));
            var orderCount = orders.Count();
            return orderCount > 0 ? totalSpent / orderCount : 0;
        }

        public DateTime? lastOrderDate(Guid companyId)
        {
            var lastOrder = _context.Orders
                .Where(o => o.User.CompanyId == companyId)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();
            return lastOrder?.CreatedAt;
        }
    }
}
