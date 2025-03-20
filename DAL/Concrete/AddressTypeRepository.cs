using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class AddressTypeRepository : GenericRepository<AddressType>, IAddressTypeRepository
    {
    }
}
