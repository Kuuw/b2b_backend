using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        bool AddImage(ProductImage image);
        new Product? GetById(Guid id);
    }
}
