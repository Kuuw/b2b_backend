using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IOrderService : IGenericService<Order, OrderPostDto, OrderGetDto, OrderPutDto>
    {
        public ServiceResult<bool> SelfCreate(OrderPostDto orderDto);
        public ServiceResult<List<OrderGetDto>?> SelfGet();
        public ServiceResult<OrderGetDto?> SelfGetOne(Guid id);
        public ServiceResult<List<OrderGetDto?>> GetPaged(int page, int pageSize, Guid? StatusId);
    }
}
