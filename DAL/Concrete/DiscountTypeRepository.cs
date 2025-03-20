using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class DiscountTypeRepository : GenericRepository<DiscountType>, IDiscountTypeRepository
    {
    }
}
