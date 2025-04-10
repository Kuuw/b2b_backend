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
        private readonly DbSet<ProductImage> _productImages;

        public ProductRepository(B2bContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _product = context.Set<Product>();
            _productImages = context.Set<ProductImage>();
        }

        public List<Product> GetPaged(int page, int pageSize, ProductFilter? productFilter)
        {
            var query = _product.AsQueryable();

            if (productFilter != null)
            {
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
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductStock)
                .Include(p => p.Status)
                .ToList();
        }

        public int GetFilteredCount(ProductFilter? productFilter)
        {
            var query = _product.AsQueryable();

            if (productFilter != null)
            {
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
            }

            return query.Count();
        }

        public bool AddImage(ProductImage image)
        {
            try
            {
                _productImages.Add(image);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

#pragma warning disable CS8603 // Possible null reference return.
        public new Product? GetById(Guid id)
        {
            var query = _product.AsQueryable();
            return query
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductStock)
                .Include(p => p.Status)
                .FirstOrDefault(p => p.ProductId == id);
        }
    }
}
