using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface ICompanyService : IGenericService<Company, CompanyPostDto, CompanyGetDto, CompanyPutDto>
    {
        public ServiceResult<CompanyGetDto?> GetByEmail(string email);
    }
}
