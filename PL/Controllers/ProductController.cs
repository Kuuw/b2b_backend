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
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult ProductGet(Guid id)
        {
            return HandleServiceResult(_productService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("CreateProduct")]
        public IActionResult ProductPost([FromBody] ProductPostDto data)
        {
            return HandleServiceResult(_productService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdateProduct")]
        public IActionResult ProductPut([FromBody] ProductPutDto data)
        {
            return HandleServiceResult(_productService.Update(data));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [NeedsPermission("DeleteProduct")]
        public IActionResult ProductDelete(Guid id)
        {
            return HandleServiceResult(_productService.Delete(id));
        }
    }
}
