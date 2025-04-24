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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet("GetPaged")]
        [NeedsPermission("Administrator")]
        public IActionResult GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            return HandleServiceResult(_userService.GetPaged(page, pageSize));
        }

        [HttpGet]
        public IActionResult User()
        {
            return HandleServiceResult(_userService.GetSelf());
        }

        [HttpGet("{id}")]
        [NeedsPermission("GetUserDetail", "Administrator")]
        public IActionResult User(Guid id)
        {
            return HandleServiceResult(_userService.GetById(id));
        }

        [HttpGet("CompanyId/{id}")]
        [NeedsPermission("GetUserDetail", "Administrator")]
        public IActionResult GetByCompany(Guid companyId)
        {
            return HandleServiceResult(_userService.GetByCompanyId(companyId));
        }

        [HttpPost("AdminInsert")]
        [NeedsPermission("InsertUser", "Administrator")]
        public IActionResult User([FromBody] UserPostDto data)
        {
            return HandleServiceResult(_userService.AdminInsert(data));
        }

        [HttpPost]
        [Authorize]
        public IActionResult User([FromBody] UserPutDto data)
        {
            return HandleServiceResult(_userService.UpdateSelf(data));
        }

        [HttpPut]
        [NeedsPermission("UpdateUser", "Administrator")]
        public IActionResult UserUpdate([FromBody] UserPutDto data)
        {
            return HandleServiceResult(_userService.Update(data));
        }

        [HttpDelete("{id}")]
        [NeedsPermission("DeleteUser", "Administrator")]
        public IActionResult DeleteUser(Guid id)
        {
            return HandleServiceResult(_userService.Delete(id));
        }
    }
}
