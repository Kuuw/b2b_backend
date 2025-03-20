using DAL.Abstract;

namespace DAL.Concrete
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
    }
}
