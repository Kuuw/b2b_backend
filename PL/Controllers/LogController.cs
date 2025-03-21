using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // TODO: Implement permission filters.
        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        public IActionResult Log(Guid id)
        {
            return HandleServiceResult(_logService.GetById(id));
        }

        [HttpGet]
        public IActionResult Log(int page, int pageSize)
        {
            return HandleServiceResult(_logService.GetPaged(page, pageSize));
        }

        [HttpGet("Type/{id}")]
        public IActionResult LogType(Guid id)
        {
            return HandleServiceResult(_logService.GetTypeById(id));
        }

        [HttpGet("Type")]
        public IActionResult LogType(int page, int pageSize)
        {
            return HandleServiceResult(_logService.GetTypesPaged(page, pageSize));
        }

        [HttpPost("Type")]
        public IActionResult LogType([FromBody] LogTypePostDto data)
        {
            return HandleServiceResult(_logService.InsertType(data));
        }

        [HttpPut("Type")]
        public IActionResult LogType([FromBody] LogTypePutDto data)
        {
            return HandleServiceResult(_logService.UpdateType(data));
        }

        [HttpDelete("Type/{id}")]
        public IActionResult LogTypeDelete(Guid id)
        {
            return HandleServiceResult(_logService.DeleteType(id));
        }

        [HttpDelete("{id}")]
        public IActionResult LogDelete(Guid id)
        {
            return HandleServiceResult(_logService.Delete(id));
        }
    }
}
