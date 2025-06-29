using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly B2bContext _context;
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
            var query = GetCompanyQuery(companyId, false);

            if (filter.Users?.Any() == true)
                query = query.Select(c => new Company
                {
                    Users = c.Users.Where(u => filter.Users.Contains(u.UserId)).ToList()
                });

            return query.FirstOrDefault()?.Users?.Count ?? 0;
        }

        public float GetTotalSales(Guid companyId, ReportFilter filter)
        {
            var orders = GetFilteredOrders(companyId, filter, true);
            return orders.SelectMany(o => o.OrderItems)
                        .Sum(oi => (float)(oi.Quantity * oi.Product.Price));
        }

        public int GetTotalOrders(Guid companyId, ReportFilter filter)
        {
            return GetFilteredOrders(companyId, filter, false).Count();
        }

        public double GetAverageSpent(Guid companyId, ReportFilter filter)
        {
            var orders = GetFilteredOrders(companyId, filter, true);
            var orderSum = orders.SelectMany(o => o.OrderItems)
                                .Sum(oi => oi.Quantity * oi.Product.Price);
            var orderCount = orders.Count();
            return orderCount > 0 ? orderSum / orderCount : 0;
        }

        public DateTime? LastOrderDate(Guid companyId, ReportFilter filter)
        {
            return GetFilteredOrders(companyId, filter, false)
                   .OrderByDescending(o => o.CreatedAt)
                   .FirstOrDefault()?.CreatedAt;
        }

        public Double GetMaxTotalSpentAcrossAllCompanies(ReportFilter filter)
        {
            var orders = GetFilteredOrders(null, filter, true);
            return orders.SelectMany(o => o.OrderItems)
                        .Sum(oi => oi.Quantity * oi.Product.Price);
        }

        public int GetMaxOrderCountAcrossAllCompanies(ReportFilter filter)
        {
            return GetFilteredOrders(null, filter, false).Count();
        }

        public List<MonthlyStatsDto> GetMonthlyStats(Guid companyId, ReportFilter filter)
        {
            var orders = GetFilteredOrders(companyId, filter, true);
            return orders
                .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
                .Select(g => new MonthlyStatsDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSpent = g.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)),
                    OrderCount = g.Count(),
                    Average = g.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)) / g.Count()
                })
                .ToList();
        }

        private IQueryable<Company> GetCompanyQuery(Guid? companyId, bool includeProducts)
        {
            var query = companyId.HasValue
                ? _company.Where(c => c.CompanyId == companyId.Value)
                : _company;

            if (includeProducts)
            {
                query = query.Include(c => c.Users)
                    .ThenInclude(u => u.Orders)
                        .ThenInclude(o => o.OrderItems)
                            .ThenInclude(oi => oi.Product);
            }
            else
            {
                query = query.Include(c => c.Users)
                    .ThenInclude(u => u.Orders);
            }

            return query;
        }

        private IQueryable<Order> GetFilteredOrders(Guid? companyId, ReportFilter filter, bool includeProducts)
        {
            var users = GetCompanyQuery(companyId, includeProducts).SelectMany(c => c.Users);

            if (filter.Users?.Any() == true)
                users = users.Where(u => filter.Users.Contains(u.UserId));

            var orders = users.SelectMany(u => u.Orders);

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            return orders;
        }
    }
}