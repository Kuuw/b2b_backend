using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class DiscountService : GenericService<Discount, DiscountPostDto, DiscountGetDto, DiscountPutDto>, IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository) : base(discountRepository)
        {
            _discountRepository = discountRepository;
        }
    }
}
