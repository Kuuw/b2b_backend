using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public class OrderService : GenericService<Order, OrderPostDto, OrderGetDto, OrderPutDto>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Mapper _mapper = MapperConfig.InitializeAutomapper();
        private readonly IUserContext _userContext;

        public OrderService(IOrderRepository orderRepository, IUserContext userContext) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _userContext = userContext;
        }

        public ServiceResult<bool> SelfCreate(OrderPostDto orderDto) {
            var order = _mapper.Map<Order>(orderDto);
            Console.WriteLine(order.OrderItems.Count);

            order.UserId = _userContext.UserId;

            var orderData = _orderRepository.Insert(order);
            if (orderData == null)
            {
                return ServiceResult<bool>.InternalServerError("Order could not be created.");
            }

            return ServiceResult<bool>.Ok(orderData);
        }

        public ServiceResult<List<OrderGetDto>?> SelfGet()
        {
            var orders = _orderRepository.Where(x => x.UserId == _userContext.UserId);
            if (orders == null)
            {
                return ServiceResult<List<OrderGetDto>?>.NotFound("Orders not found.");
            }
            return ServiceResult<List<OrderGetDto>?>.Ok(_mapper.Map<List<OrderGetDto>>(orders));
        }
        public ServiceResult<List<OrderGetDto?>> GetPaged(int page, int pageSize, Guid? StatusId)
        {
            var orders = _orderRepository.Where(
                [x => !StatusId.HasValue || x.StatusId == StatusId.Value],
                q => q.Include(x => x.Status)
                      .Include(x => x.OrderItems)
                      .ThenInclude(x => x.Product)
                      .Include(x => x.User)
                      .ThenInclude(x => x.Company)
                      .Include(x => x.Invoices));

            if (orders == null || !orders.Any())
            {
                return ServiceResult<List<OrderGetDto?>>.NotFound("Orders not found.");
            }

            return ServiceResult<List<OrderGetDto?>>.Ok(_mapper.Map<List<OrderGetDto?>>(orders));
        }
    }
}
