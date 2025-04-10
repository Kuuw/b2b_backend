using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace BL.Abstract
{
    public interface IProductService : IGenericService<Product, ProductPostDto, ProductGetDto, ProductPutDto>
    {
        ServiceResult<ProductPagedResponse> GetPaged(ProductGetPagedDto productGetPagedDto);
        ServiceResult<string> UploadImage(Guid productId, Stream file);
    }
}
