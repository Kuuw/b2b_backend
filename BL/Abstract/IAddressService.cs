using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface IAddressService : IGenericService<Address, AddressPostDto, AddressGetDto, AddressPutDto>
    {
    }
}
