using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using ImageMagick;

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
            var response = new ProductPagedResponse();
            var metadata = new PageMetadata();
            var items = _productRepository.GetPaged(productGetPagedDto.Page, productGetPagedDto.PageSize, productGetPagedDto.Filter);

            List<ProductGetDto> itemsDTO = mapper.Map<List<Product>, List<ProductGetDto>>(items);
            response.Items = itemsDTO;

            int totalItems = _productRepository.GetFilteredCount(productGetPagedDto.Filter);
            int totalPages = (int)Math.Ceiling((double)totalItems / productGetPagedDto.PageSize);
            metadata.Page = productGetPagedDto.Page;
            metadata.PageSize = productGetPagedDto.PageSize;
            metadata.TotalPages = totalPages;

            response.Metadata = metadata;

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
