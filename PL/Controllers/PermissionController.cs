using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // TODO: Implement permission filters.
        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        public IActionResult PermissionGet(Guid id)
        {
            return HandleServiceResult(_permissionService.GetById(id));
        }

        [HttpPost]
        public IActionResult PermissionPost([FromBody] PermissionPostDto data)
        {
            return HandleServiceResult(_permissionService.Insert(data));
        }

        [HttpPut]
        public IActionResult PermissionPut([FromBody] PermissionPutDto data)
        {
            return HandleServiceResult(_permissionService.Update(data));
        }

        [HttpDelete("{id}")]
        public IActionResult PermissionDelete(Guid id)
        {
            return HandleServiceResult(_permissionService.Delete(id));
        }
    }
}
