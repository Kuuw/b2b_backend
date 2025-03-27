using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetPaged(ProductGetPagedDto productGetPagedDto);
        int GetFilteredCount(ProductFilter productFilter);
    }
}
