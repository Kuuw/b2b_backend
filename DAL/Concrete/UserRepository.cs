using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly B2bContext _context = new B2bContext();
        private readonly DbSet<User> _user;

        public UserRepository(B2bContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _user = context.Set<User>();
        }

        public User? GetByEmail(string email)
        {
            return _user
                .Include(x => x.Role)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefault(x => x.Email == email);
        }

        public float GetTotalSales(Guid userId, ReportFilter filter)
        {
            var query = _user
                .Where(u => u.UserId == userId)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .AsQueryable();

            var orders = query.SelectMany(u => u.Orders).AsQueryable();

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            var orderItems = orders.SelectMany(o => o.OrderItems);

            var totalSales = orderItems.Sum(oi => (float)(oi.Quantity * oi.Product.Price));
            return totalSales;
        }

        public int GetTotalOrders(Guid userId, ReportFilter filter)
        {
            var query = _user
                .Where(u => u.UserId == userId)
                .Include(u => u.Orders)
                .AsQueryable();

            var orders = query.SelectMany(u => u.Orders).AsQueryable();

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            return orders.Count();
        }

        public double GetAverageSpent(Guid userId, ReportFilter filter)
        {
            var query = _user
                .Where(u => u.UserId == userId)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .AsQueryable();

            var orders = query.SelectMany(u => u.Orders).AsQueryable();

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

        public DateTime? LastOrderDate(Guid userId, ReportFilter filter)
        {
            var query = _user
                .Where(u => u.UserId == userId)
                .Include(u => u.Orders)
                .AsQueryable();

            var orders = query.SelectMany(u => u.Orders).AsQueryable();

            if (filter.StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt <= filter.EndDate.Value);

            var lastOrder = orders.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
            return lastOrder?.CreatedAt;
        }
    }
}
