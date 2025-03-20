using DAL.Abstract;

namespace DAL.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
    }
}
