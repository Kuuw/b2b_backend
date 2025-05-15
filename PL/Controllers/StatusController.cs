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
    public class StatusController : BaseController
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult StatusGet(Guid id)
        {
            return HandleServiceResult(_statusService.GetById(id));
        }

        [HttpPost]
        [NeedsPermission("CreateStatus", "Administrator")]
        public IActionResult StatusPost([FromBody] StatusPostDto data)
        {
            return HandleServiceResult(_statusService.Insert(data));
        }

        [HttpPut]
        [NeedsPermission("UpdateStatus", "Administrator")]
        public IActionResult StatusPut([FromBody] StatusPutDto data)
        {
            return HandleServiceResult(_statusService.Update(data));
        }

        [HttpDelete("{id}")]
        [NeedsPermission("DeleteStatus", "Administrator")]
        public IActionResult StatusDelete(Guid id)
        {
            return HandleServiceResult(_statusService.Delete(id));
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public IActionResult GetStatuses()
        {
            return HandleServiceResult(_statusService.GetAll());
        }
    }
}
