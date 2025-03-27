using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IProductService : IGenericService<Product, ProductPostDto, ProductGetDto, ProductPutDto>
    {
        ServiceResult<ProductPagedResponse> GetPaged(ProductGetPagedDto productGetPagedDto);
    }
}
