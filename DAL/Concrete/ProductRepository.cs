using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class ProductRepository : GenericRepository<Product>, IProductRepository
    {
    }
}
