using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.ActionFilters;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [NeedsPermission("ViewCountry", "Administrator")]
        public IActionResult CountryGetAll()
        {
            return HandleServiceResult(_countryService.GetAll());
        }

        [HttpPost]
        [NeedsPermission("InsertCountry", "Administrator")]
        public IActionResult CountryPost([FromBody] CountryPostDto data)
        {
            return HandleServiceResult(_countryService.Insert(data));
        }

        [HttpPut]
        [NeedsPermission("UpdateCountry", "Administrator")]
        public IActionResult CountryPut([FromBody] CountryPutDto data)
        {
            return HandleServiceResult(_countryService.Update(data));
        }

        [HttpDelete("{id}")]
        [NeedsPermission("DeleteCountry", "Administrator")]
        public IActionResult CountryDelete(Guid id)
        {
            return HandleServiceResult(_countryService.Delete(id));
        }
    }
}
