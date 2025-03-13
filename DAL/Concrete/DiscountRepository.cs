using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
    }
}
