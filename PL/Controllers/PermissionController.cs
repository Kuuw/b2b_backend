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
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult PermissionGet(Guid id)
        {
            return HandleServiceResult(_permissionService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("InsertPermission", "Administrator")]
        public IActionResult PermissionPost([FromBody] PermissionPostDto data)
        {
            return HandleServiceResult(_permissionService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdatePermission", "Administrator")]
        public IActionResult PermissionPut([FromBody] PermissionPutDto data)
        {
            return HandleServiceResult(_permissionService.Update(data));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [NeedsPermission("DeletePermission", "Administrator")]
        public IActionResult PermissionDelete(Guid id)
        {
            return HandleServiceResult(_permissionService.Delete(id));
        }
    }
}
