using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface ICountryService : IGenericService<Country, CountryPostDto, CountryGetDto, CountryPutDto>
    {
    }
}
