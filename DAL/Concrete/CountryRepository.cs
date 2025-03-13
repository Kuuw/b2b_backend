using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class CountryRepository: GenericRepository<Country>, ICountryRepository
    {
    }
}
