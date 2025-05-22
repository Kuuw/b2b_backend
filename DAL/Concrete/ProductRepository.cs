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

        public Double GetMaxPrice()
        {
            var maxPrice = _product.Max(p => p.Price);
            return maxPrice;
        }

        public int GetMaxStock()
        {
            var maxStock = _product.Max(p => p.ProductStock != null ? p.ProductStock.StockQuantity : 0);
            return maxStock;
        }
    }
}
