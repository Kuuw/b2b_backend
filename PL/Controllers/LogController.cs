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
    public class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // TODO: Implement validation filters.  

        [HttpGet("{id}")]
        [NeedsPermission("ViewLog", "Administrator")]
        public IActionResult Log(Guid id)
        {
            return HandleServiceResult(_logService.GetById(id));
        }

        [HttpGet]
        [NeedsPermission("ViewLog", "Administrator")]
        public IActionResult Log(int page, int pageSize)
        {
            return HandleServiceResult(_logService.GetPaged(page, pageSize));
        }

        [HttpGet("Type/{id}")]
        [AllowAnonymous]
        public IActionResult LogType(Guid id)
        {
            return HandleServiceResult(_logService.GetTypeById(id));
        }

        [HttpGet("Type")]
        [AllowAnonymous]
        public IActionResult LogType(int page, int pageSize)
        {
            return HandleServiceResult(_logService.GetTypesPaged(page, pageSize));
        }

        [HttpPost("Type")]
        [NeedsPermission("CreateLogType", "Administrator")]
        public IActionResult LogType([FromBody] LogTypePostDto data)
        {
            return HandleServiceResult(_logService.InsertType(data));
        }

        [HttpPut("Type")]
        [NeedsPermission("UpdateLogType", "Administrator")]
        public IActionResult LogType([FromBody] LogTypePutDto data)
        {
            return HandleServiceResult(_logService.UpdateType(data));
        }

        [HttpDelete("Type/{id}")]
        [NeedsPermission("DeleteLogType", "Administrator")]
        public IActionResult LogTypeDelete(Guid id)
        {
            return HandleServiceResult(_logService.DeleteType(id));
        }

        [HttpDelete("{id}")]
        [NeedsPermission("DeleteLog", "Administrator")]
        public IActionResult LogDelete(Guid id)
        {
            return HandleServiceResult(_logService.Delete(id));
        }
    }
}
