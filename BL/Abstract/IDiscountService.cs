using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IDiscountService : IGenericService<Discount, DiscountPostDto, DiscountGetDto, DiscountPutDto>
    {
    }
}
