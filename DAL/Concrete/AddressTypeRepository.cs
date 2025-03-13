using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class AddressTypeRepository : GenericRepository<AddressType>, IAddressTypeRepository
    {
    }
}
