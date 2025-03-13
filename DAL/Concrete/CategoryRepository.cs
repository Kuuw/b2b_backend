using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
    }
}
