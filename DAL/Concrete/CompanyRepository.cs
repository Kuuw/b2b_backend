using DAL.Abstract;
using Entities.DTO;
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

        public int getUserCount(Guid companyId, ReportFilter filter)
        {
            var query = _company
                .Where(c => c.CompanyId == companyId)
                .Include(c => c.Users)
                .AsQueryable();

            if (filter.Users != null && filter.Users.Any())
                query = query.Select(c => new Company
                {
                    Users = c.Users.Where(u => filter.Users.Contains(u.UserId)).ToList()
                }).AsQueryable();

            var company = query.FirstOrDefault();
            return company?.Users?.Count ?? 0;
        }

        public float getTotalSales(Guid companyId, ReportFilter filter)
        {
            var query = _company
                .Where(c => c.CompanyId == companyId)
                .Include(c => c.Users)
                    .ThenInclude(u => u.Orders)
                        .ThenInclude(o => o.OrderItems)
                            .ThenInclude(oi => oi.Product)
                .AsQueryable();

            var users = query.SelectMany(c => c.Users).AsQueryable();

            if (filter.Users != null && filter.Users.Any())
                users = users.Where(u => filter.Users.Contains(u.UserId));

            var orders = users.SelectMany(u => u.Orders).AsQueryable();

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            var orderItems = orders.SelectMany(o => o.OrderItems);

            var totalSales = orderItems.Sum(oi => (float)(oi.Quantity * oi.Product.Price));
            return totalSales;
        }

        public int getTotalOrders(Guid companyId, ReportFilter filter)
        {
            var query = _company
                .Where(c => c.CompanyId == companyId)
                .Include(c => c.Users)
                    .ThenInclude(u => u.Orders)
                .AsQueryable();

            var users = query.SelectMany(c => c.Users).AsQueryable();

            if (filter.Users != null && filter.Users.Any())
                users = users.Where(u => filter.Users.Contains(u.UserId));

            var orders = users.SelectMany(u => u.Orders).AsQueryable();

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            return orders.Count();
        }

        public double getAverageSpent(Guid companyId, ReportFilter filter)
        {
            var query = _company
                .Where(c => c.CompanyId == companyId)
                .Include(c => c.Users)
                    .ThenInclude(u => u.Orders)
                        .ThenInclude(o => o.OrderItems)
                            .ThenInclude(oi => oi.Product)
                .AsQueryable();

            var users = query.SelectMany(c => c.Users).AsQueryable();

            if (filter.Users != null && filter.Users.Any())
                users = users.Where(u => filter.Users.Contains(u.UserId));

            var orders = users.SelectMany(u => u.Orders).AsQueryable();

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            var orderSum = orders
                .SelectMany(o => o.OrderItems)
                .Sum(oi => oi.Quantity * oi.Product.Price);

            var orderCount = orders.Count();

            return orderCount > 0 ? orderSum / orderCount : 0;
        }

        public DateTime? lastOrderDate(Guid companyId, ReportFilter filter)
        {
            var query = _company
                .Where(c => c.CompanyId == companyId)
                .Include(c => c.Users)
                    .ThenInclude(u => u.Orders)
                .AsQueryable();

            var users = query.SelectMany(c => c.Users).AsQueryable();

            if (filter.Users != null && filter.Users.Any())
                users = users.Where(u => filter.Users.Contains(u.UserId));

            var orders = users.SelectMany(u => u.Orders).AsQueryable();

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            var lastOrder = orders.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
            return lastOrder?.CreatedAt;
        }
    }
}
