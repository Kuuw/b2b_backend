using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetPaged(int page, int pageSize, ProductFilter productFilter);
        int GetFilteredCount(ProductFilter productFilter);
    }
}
