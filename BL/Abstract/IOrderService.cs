using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IOrderService : IGenericService<Order, OrderPostDto, OrderGetDto, OrderPutDto>
    {
        public ServiceResult<bool> SelfCreate(OrderPostDto orderDto);
        public ServiceResult<List<OrderGetDto>?> SelfGet();
    }
}
