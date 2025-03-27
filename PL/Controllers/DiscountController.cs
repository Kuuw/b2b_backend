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
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult DiscountGet(Guid id)
        {
            return HandleServiceResult(_discountService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("CreateDiscount", "Administrator")]
        public IActionResult DiscountPost([FromBody] DiscountPostDto data)
        {
            return HandleServiceResult(_discountService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdateDiscount", "Administrator")]
        public IActionResult DiscountPut([FromBody] DiscountPutDto data)
        {
            return HandleServiceResult(_discountService.Update(data));
        }

        [HttpDelete]
        [Authorize]
        [NeedsPermission("DeleteDiscount", "Administrator")]
        public IActionResult DiscountDelete(Guid id)
        {
            return HandleServiceResult(_discountService.Delete(id));
        }
    }
}
