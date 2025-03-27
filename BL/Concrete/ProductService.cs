using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class ProductService : GenericService<Product, ProductPostDto, ProductGetDto, ProductPutDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
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
    }
}
