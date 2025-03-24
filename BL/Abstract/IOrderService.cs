using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IOrderService : IGenericService<Order, OrderPostDto, OrderGetDto, OrderPutDto>
    {
    }
}
