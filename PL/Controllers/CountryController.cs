using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

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

        // TODO: Implement permission filters.
        // TODO: Implement validation filters.

        [HttpGet("{id}")]
        public IActionResult CountryGet(Guid id)
        {
            return HandleServiceResult(_countryService.GetById(id));
        }

        [HttpPost]
        public IActionResult CountryPost([FromBody] CountryPostDto data)
        {
            return HandleServiceResult(_countryService.Insert(data));
        }

        [HttpPut]
        public IActionResult CountryPut([FromBody] CountryPutDto data)
        {
            return HandleServiceResult(_countryService.Update(data));
        }

        [HttpDelete("{id}")]
        public IActionResult CountryDelete(Guid id)
        {
            return HandleServiceResult(_countryService.Delete(id));
        }
    }
}
