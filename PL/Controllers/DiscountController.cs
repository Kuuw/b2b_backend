using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        // TODO: Implement permission filters.
        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        public IActionResult DiscountGet(Guid id)
        {
            return HandleServiceResult(_discountService.GetById(id));
        }

        [HttpPost]
        public IActionResult DiscountPost([FromBody] DiscountPostDto data)
        {
            return HandleServiceResult(_discountService.Insert(data));
        }

        [HttpPut]
        public IActionResult DiscountPut([FromBody] DiscountPutDto data)
        {
            return HandleServiceResult(_discountService.Update(data));
        }

        [HttpDelete]
        public IActionResult DiscountDelete(Guid id)
        {
            return HandleServiceResult(_discountService.Delete(id));
        }
    }
}
