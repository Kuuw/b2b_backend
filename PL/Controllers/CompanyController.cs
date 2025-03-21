using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService service)
        {
            _companyService = service;
        }

        // TODO: Implement permission filters.
        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        public IActionResult Company(Guid id)
        {
            return HandleServiceResult(_companyService.GetById(id));
        }

        [HttpGet]
        public IActionResult Company(string email)
        {
            return HandleServiceResult(_companyService.GetByEmail(email));
        }

        [HttpPost]
        public IActionResult Company([FromBody] CompanyPostDto data)
        {
            return HandleServiceResult(_companyService.Insert(data));
        }

        [HttpPut]
        public IActionResult Company([FromBody] CompanyPutDto data)
        {
            return HandleServiceResult(_companyService.Update(data));
        }

        [HttpDelete("{id}")]
        public IActionResult CompanyDelete(Guid id)
        {
            return HandleServiceResult(_companyService.Delete(id));
        }
    }
}
