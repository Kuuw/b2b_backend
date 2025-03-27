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
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{id}")]
        [Authorize]
        [NeedsPermission("ViewRoleDetails", "Administrator")]
        public IActionResult RoleGet(Guid id)
        {
            return HandleServiceResult(_roleService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("InsertRole", "Administrator")]
        public IActionResult RolePost([FromBody] RolePostDto data)
        {
            return HandleServiceResult(_roleService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdateRole", "Administrator")]
        public IActionResult RolePut([FromBody] RolePutDto data)
        {
            return HandleServiceResult(_roleService.Update(data));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [NeedsPermission("DeleteRole", "Administrator")]
        public IActionResult RoleDelete(Guid id)
        {
            return HandleServiceResult(_roleService.Delete(id));
        }
    }
}
