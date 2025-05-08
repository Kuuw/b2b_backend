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
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService service)
        {
            _companyService = service;
        }

        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        [NeedsPermission("GetCompanyDetail", "Administrator")]
        public IActionResult Company(Guid id)
        {
            return HandleServiceResult(_companyService.GetById(id));
        }

        [HttpGet]
        [NeedsPermission("GetCompanyDetail", "Administrator")]
        public IActionResult Company(string email)
        {
            return HandleServiceResult(_companyService.GetByEmail(email));
        }

        [HttpPost]
        [NeedsPermission("InsertCompany", "Administrator")]
        public IActionResult Company([FromBody] CompanyPostDto data)
        {
            return HandleServiceResult(_companyService.Insert(data));
        }

        [HttpPut]
        [NeedsPermission("UpdateCompany", "Administrator")]
        public IActionResult Company([FromBody] CompanyPutDto data)
        {
            return HandleServiceResult(_companyService.Update(data));
        }

        [HttpDelete("{id}")]
        [NeedsPermission("DeleteCompany", "Administrator")]
        public IActionResult CompanyDelete(Guid id)
        {
            return HandleServiceResult(_companyService.Delete(id));
        }

        [HttpGet("GetAll")]
        [NeedsPermission("Administrator")]
        public IActionResult GetAll()
        {
            return HandleServiceResult(_companyService.GetAll());
        }

        [HttpGet("GetPaged")]
        [NeedsPermission("Administrator")]
        public IActionResult GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            return HandleServiceResult(_companyService.GetPaged(page, pageSize));
        }

        [HttpGet("GetReports")]
        [NeedsPermission("Administrator")]
        public IActionResult GetReports([FromQuery] int page, [FromQuery] int pageSize)
        {
            return HandleServiceResult(_companyService.GetReports(page, pageSize));
        }
    }
}
