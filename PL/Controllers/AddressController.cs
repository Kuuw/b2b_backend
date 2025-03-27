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
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService service)
        {
            _addressService = service;
        }

        [HttpGet("{id}")]
        [Authorize]
        [NeedsPermission("ViewAddressDetails")]
        public IActionResult Address(Guid id)
        {
            return HandleServiceResult(_addressService.GetById(id));
        }

        [HttpGet("CompanyId/{id}")]
        [Authorize]
        [NeedsPermission("ViewAddressDetails")]
        public IActionResult GetByCompany(Guid companyId)
        {
            return HandleServiceResult(_addressService.GetByCompanyId(companyId));
        }

        [HttpGet("UserId/{id}")]
        [Authorize]
        [NeedsPermission("ViewAddressDetails")]
        public IActionResult GetByUser(Guid userId)
        {
            return HandleServiceResult(_addressService.GetByUserId(userId));
        }

        [HttpPost]
        [Authorize]
        [NeedsPermission("InsertAddress")]
        public IActionResult Address([FromBody] AddressPostDto data)
        {
            return HandleServiceResult(_addressService.Insert(data));
        }

        [HttpPut]
        [Authorize]
        [NeedsPermission("UpdateAddress")]
        public IActionResult Address([FromBody] AddressPutDto data)
        {
            return HandleServiceResult(_addressService.Update(data));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [NeedsPermission("DeleteAddress")]
        public IActionResult DeleteAddress(Guid id)
        {
            return HandleServiceResult(_addressService.Delete(id));
        }
    }
}
