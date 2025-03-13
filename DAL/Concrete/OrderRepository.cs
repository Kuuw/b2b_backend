using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
    }
}
