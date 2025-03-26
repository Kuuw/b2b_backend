using BL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService service)
        {
            _addressService = service;
        }

        [HttpGet("{id}")]
        public IActionResult Address(Guid id)
        {
            return HandleServiceResult(_addressService.GetById(id));
        }

        [HttpGet("CompanyId/{id}")]
        public IActionResult GetByCompany(Guid companyId)
        {
            return HandleServiceResult(_addressService.GetByCompanyId(companyId));
        }

        [HttpGet("UserId/{id}")]
        public IActionResult GetByUser(Guid userId)
        {
            return HandleServiceResult(_addressService.GetByUserId(userId));
        }

        [HttpPost]
        public IActionResult Address([FromBody] AddressPostDto data)
        {
            return HandleServiceResult(_addressService.Insert(data));
        }

        [HttpPut]
        public IActionResult Address([FromBody] AddressPutDto data)
        {
            return HandleServiceResult(_addressService.Update(data));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(Guid id)
        {
            return HandleServiceResult(_addressService.Delete(id));
        }
    }
}
