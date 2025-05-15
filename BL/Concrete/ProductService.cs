using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using ImageMagick;
using Microsoft.EntityFrameworkCore;

namespace BL.Concrete
{
    public class ProductService : GenericService<Product, ProductPostDto, ProductGetDto, ProductPutDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileRepository _fileRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public ProductService(IProductRepository productRepository, IFileRepository fileRepository) : base(productRepository)
        {
            _productRepository = productRepository;
            _fileRepository = fileRepository;
        }

        public ServiceResult<ProductPagedResponse> GetPaged(ProductGetPagedDto productGetPagedDto)
        {
            var query = _productRepository.Queryable()
                .Include(x => x.Status)
                .Include(x => x.ProductImages)
                .Include(x => x.Category)
                .Where(x => x.ProductName.Contains(productGetPagedDto.Filter.ProductName ?? ""))
                .Where(x => productGetPagedDto.Filter.CategoryId == null || x.CategoryId == productGetPagedDto.Filter.CategoryId)
                .Where(x => productGetPagedDto.Filter.MaxPrice == null || x.Price <= productGetPagedDto.Filter.MaxPrice)
                .Where(x => productGetPagedDto.Filter.MinPrice == null || x.Price >= productGetPagedDto.Filter.MinPrice);

            var response = new ProductPagedResponse();
            var items = _productRepository.GetPaged(productGetPagedDto.PageNumber, productGetPagedDto.PageSize, q => query);

            List<ProductGetDto> itemsDTO = mapper.Map<List<Product>, List<ProductGetDto>>(items);
            response.Items = itemsDTO;
            response.TotalPages = _productRepository.GetPageCount(productGetPagedDto.PageSize, q => query);
            response.PageSize = productGetPagedDto.PageSize;
            response.PageNumber = productGetPagedDto.PageNumber;

            return ServiceResult<ProductPagedResponse>.Ok(response);
        }

        public ServiceResult<string> UploadImage(Guid productId, Stream file)
        {
            var product = _productRepository.GetById(productId);
            if (product == null)
            {
                throw new ArgumentException("Product not found.");
            }

            var newGuid = Guid.NewGuid();
            ProductImage productImage = new();
            productImage.ProductId = productId;
            productImage.ProductImageId = newGuid;


            using (var seekableStream = new MemoryStream())
            {
                file.CopyToAsync(seekableStream);
                seekableStream.Position = 0;
                using (var img = new MagickImage(seekableStream))
                {
                    if (!(img.Format == MagickFormat.Jpeg || img.Format == MagickFormat.Png))
                        throw new InvalidOperationException("Uploaded file should be in png or jpeg.");

                    Console.WriteLine($"Image format detected: {img.Format}");

                    var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                    try
                    {
                        img.Resize(new MagickGeometry(720, 720) { IgnoreAspectRatio = false });
                        img.Write(tempFile, MagickFormat.Jpeg);

                        using (var tempFileStream = new FileStream(tempFile, FileMode.Open))
                        {
                            var path = _fileRepository.UploadFile("product-image", $"{newGuid}.jpg", tempFileStream);
                            productImage.ImageUrl = path;

                            _productRepository.AddImage(productImage);
                            return ServiceResult<string>.Ok(path);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    finally
                    {
                        if (File.Exists(tempFile))
                        {
                            File.Delete(tempFile);
                        }
                    }
                }
            }
        }
    }
}
