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

        public int GetUserCount(Guid companyId, ReportFilter filter)
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

        public float GetTotalSales(Guid companyId, ReportFilter filter)
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

        public int GetTotalOrders(Guid companyId, ReportFilter filter)
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

        public double GetAverageSpent(Guid companyId, ReportFilter filter)
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

        public DateTime? LastOrderDate(Guid companyId, ReportFilter filter)
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

        public Double GetMaxTotalSpentAcrossAllCompanies(ReportFilter filter)
        {
            var query = _company
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
            var totalSpent = orderItems.Sum(oi => oi.Quantity * oi.Product.Price);
            return totalSpent;
        }

        public int GetMaxOrderCountAcrossAllCompanies(ReportFilter filter)
        {
            var query = _company
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
            var maxOrderCount = orders.Count();
            return maxOrderCount;
        }

        public List<MonthlyStatsDto> GetMonthlyStats(Guid companyId, ReportFilter filter)
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
            var monthlyStats = orders
                .GroupBy(o => new { Year = o.CreatedAt.Year, Month = o.CreatedAt.Month })
                .Select(g => new MonthlyStatsDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSpent = g.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)),
                    OrderCount = g.Count(),
                    Average = g.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)) / g.Count()
                })
                .ToList();
            return monthlyStats;
        }
    }
}
