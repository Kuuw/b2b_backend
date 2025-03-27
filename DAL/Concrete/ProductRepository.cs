using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly B2bContext _context = new B2bContext();
        private readonly DbSet<Product> _product;

        public ProductRepository(B2bContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _product = context.Set<Product>();
        }

        public List<Product> GetPaged(int page, int pageSize, ProductFilter productFilter)
        {
            var query = _product.AsQueryable();

            if (!string.IsNullOrEmpty(productFilter.ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(productFilter.ProductName));
            }

            if (productFilter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == productFilter.CategoryId.Value);
            }

            if (productFilter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= productFilter.MinPrice.Value);
            }

            if (productFilter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= productFilter.MaxPrice.Value);
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetFilteredCount(ProductFilter productFilter)
        {
            var query = _product.AsQueryable();

            if (!string.IsNullOrEmpty(productFilter.ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(productFilter.ProductName));
            }

            if (productFilter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == productFilter.CategoryId.Value);
            }

            if (productFilter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= productFilter.MinPrice.Value);
            }

            if (productFilter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= productFilter.MaxPrice.Value);
            }

            return query.Count();
        }
    }
}
