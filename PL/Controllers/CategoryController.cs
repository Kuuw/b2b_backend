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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult CategoryGet(Guid id)
        {
            return HandleServiceResult(_categoryService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("InsertCategory")]
        public IActionResult CategoryPost([FromBody] CategoryPostDto data)
        {
            return HandleServiceResult(_categoryService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdateCategory")]
        public IActionResult CategoryPut([FromBody] CategoryPutDto data)
        {
            return HandleServiceResult(_categoryService.Update(data));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [NeedsPermission("DeleteCategory")]
        public IActionResult CategoryDelete(Guid id)
        {
            return HandleServiceResult(_categoryService.Delete(id));
        }
    }
}