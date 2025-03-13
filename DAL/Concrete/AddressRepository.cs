using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
    }
}
