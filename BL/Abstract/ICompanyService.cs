using Entities.DTO;
using Entities.Models;

namespace BL.Abstract
{
    public interface ICompanyService
    {
        public ServiceResult<bool> Insert(CompanyPostDto data);
        public ServiceResult<CompanyGetDto?> GetByEmail(string email);
        public ServiceResult<CompanyGetDto?> GetById(Guid id);
        public ServiceResult<bool> Update(CompanyPutDto data);
        public ServiceResult<bool> Delete(Guid guid);
    }
}
