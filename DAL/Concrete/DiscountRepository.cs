using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
    }
}
