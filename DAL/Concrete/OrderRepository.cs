using DAL.Abstract;

namespace DAL.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
    }
}
