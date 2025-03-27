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
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [Authorize]
        [NeedsPermission("ViewOrderDetails")]
        public IActionResult OrderGet(Guid id)
        {
            return HandleServiceResult(_orderService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("CreateOrder")]
        public IActionResult OrderPost([FromBody] OrderPostDto data)
        {
            return HandleServiceResult(_orderService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdateOrder")]
        public IActionResult OrderPut([FromBody] OrderPutDto data)
        {
            return HandleServiceResult(_orderService.Update(data));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [NeedsPermission("DeleteOrder")]
        public IActionResult OrderDelete(Guid id)
        {
            return HandleServiceResult(_orderService.Delete(id));
        }
    }
}
