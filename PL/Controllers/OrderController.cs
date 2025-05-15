using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.ActionFilters;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [NeedsPermission("ViewOrderDetails", "Administrator")]
        public IActionResult OrderGet(Guid id)
        {
            return HandleServiceResult(_orderService.GetById(id));
        }

        [HttpPost]
        [NeedsPermission("CreateOrder", "Administrator")]
        public IActionResult OrderPost([FromBody] OrderPostDto data)
        {
            return HandleServiceResult(_orderService.Insert(data));
        }

        [HttpPut]
        [NeedsPermission("UpdateOrder", "Administrator")]
        public IActionResult OrderPut([FromBody] OrderPutDto data)
        {
            return HandleServiceResult(_orderService.Update(data));
        }

        [HttpDelete("{id}")]
        [NeedsPermission("DeleteOrder", "Administrator")]
        public IActionResult OrderDelete(Guid id)
        {
            return HandleServiceResult(_orderService.Delete(id));
        }

        [HttpPost("Create")]
        [NeedsPermission("COFC", "Administrator")]
        public IActionResult CreateOrder([FromBody] OrderPostDto data)
        {
            return HandleServiceResult(_orderService.SelfCreate(data));
        }

        [HttpGet]
        [NeedsPermission("GetOrders", "Administrator")]
        public IActionResult GetOrders()
        {
            return HandleServiceResult(_orderService.SelfGet());
        }

        [HttpGet("GetOne/{id}")]
        [NeedsPermission("GetOrders", "Administrator")]
        public IActionResult GetOrder(Guid id)
        {
            return HandleServiceResult(_orderService.SelfGetOne(id));
        }

        [HttpGet("GetPaged")]
        [NeedsPermission("Administrator")]
        public IActionResult GetPaged(int page, int pageSize, Guid StatusId)
        {
            return HandleServiceResult(_orderService.GetPaged(page, pageSize, StatusId));
        }
    }
}
