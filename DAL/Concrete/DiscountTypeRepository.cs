using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class DiscountTypeRepository : GenericRepository<DiscountType>, IDiscountTypeRepository
    {
    }
}
