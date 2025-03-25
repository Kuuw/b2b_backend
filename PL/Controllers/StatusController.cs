using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class StatusController : BaseController
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // TODO: Implement permission filters.
        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        public IActionResult StatusGet(Guid id)
        {
            return HandleServiceResult(_statusService.GetById(id));
        }

        [HttpPost]
        public IActionResult StatusPost([FromBody] StatusPostDto data)
        {
            return HandleServiceResult(_statusService.Insert(data));
        }

        [HttpPut]
        public IActionResult StatusPut([FromBody] StatusPutDto data)
        {
            return HandleServiceResult(_statusService.Update(data));
        }

        [HttpDelete("{id}")]
        public IActionResult StatusDelete(Guid id)
        {
            return HandleServiceResult(_statusService.Delete(id));
        }
    }
}
