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
        [NeedsPermission("GetCompanyDetail")]
        public IActionResult Company(Guid id)
        {
            return HandleServiceResult(_companyService.GetById(id));
        }

        [HttpGet]
        [NeedsPermission("GetCompanyDetail")]
        public IActionResult Company(string email)
        {
            return HandleServiceResult(_companyService.GetByEmail(email));
        }

        [HttpPost]
        [NeedsPermission("InsertCompany")]
        public IActionResult Company([FromBody] CompanyPostDto data)
        {
            return HandleServiceResult(_companyService.Insert(data));
        }

        [HttpPut]
        [NeedsPermission("UpdateCompany")]
        public IActionResult Company([FromBody] CompanyPutDto data)
        {
            return HandleServiceResult(_companyService.Update(data));
        }

        [HttpDelete("{id}")]
        [NeedsPermission("DeleteCompany")]
        public IActionResult CompanyDelete(Guid id)
        {
            return HandleServiceResult(_companyService.Delete(id));
        }
    }
}
