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
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult CountryGet(Guid id)
        {
            return HandleServiceResult(_countryService.GetById(id));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CountryGetAll()
        {
            return HandleServiceResult(_countryService.GetAll());
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("InsertCountry", "Administrator")]
        public IActionResult CountryPost([FromBody] CountryPostDto data)
        {
            return HandleServiceResult(_countryService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdateCountry", "Administrator")]
        public IActionResult CountryPut([FromBody] CountryPutDto data)
        {
            return HandleServiceResult(_countryService.Update(data));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [NeedsPermission("DeleteCountry", "Administrator")]
        public IActionResult CountryDelete(Guid id)
        {
            return HandleServiceResult(_countryService.Delete(id));
        }
    }
}
